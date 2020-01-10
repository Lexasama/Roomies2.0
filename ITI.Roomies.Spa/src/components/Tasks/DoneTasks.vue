<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Done tasks</span>
      </div>
      <div>
        <template>
          <el-table :data="doneTaskList" style="width: 100%">
            <el-table-column prop="state" label="State" width="180">
              <template slot-scope="scope">
                <el-button round icon="el-icon-circle-check" @click="updateState(scope.row)"></el-button>
              </template>
            </el-table-column>
            <el-table-column prop="taskDate" label="Date" :formatter="dateFormatter"></el-table-column>
            <el-table-column prop="taskName" label="Name"></el-table-column>
            <el-table-column label="Roomie(s)" :formatter="nameFormatter"></el-table-column>
            <el-table-column prop="taskDes" label="Description"></el-table-column>
          </el-table>
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
import { getColocFilteredTasksAsync, updateStateAsync } from "@/api/TaskApi";
import { DateTime } from "luxon";
export default {
  data() {
    return {
      taskList: [],
      colocId: null
    };
  }, //end data
  async mounted() {
    this.colocId = this.$currentColoc.colocId;
    this.refreshList();
  }, //end mounted
  computed: {
    doneTaskList() {
      return this.taskList;
    }
  }, //end computed
  methods: {
    async refreshList() {
      this.taskList = await getColocFilteredTasksAsync(this.colocId, false);
    },
    nameFormatter(row, colunm) {
      let names = "";
      row.roomies.forEach(element => {
        names = names + " " + element.firstName;
      });
      return names;
    },
    dateFormatter(row, colunm) {
      let d = DateTime.fromISO(row.taskDate).toISODate();
      let c = new Date(row.taskDate);
      let date = c.getDate() + "/" + (c.getMonth() + 1) + "/" + c.getFullYear();
      return date;
    },
    async updateState(task) {
      try {
        var r = await updateStateAsync(task.taskId);
        this.$emit("update-tasklist");
        this.refreshList();
      } catch (e) {
        console.error(e);
      }
    }
  } //end methods
};
</script>
