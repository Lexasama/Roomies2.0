<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Active tasks</span>
      </div>
      <div>
        <template>
          <el-table :data="activeTaskList" style="width: 100%">
            <el-table-column prop="state" label="State" width="180">
              <template slot-scope="scope">
                <el-button round icon="el-icon-circle-check" @click="updateState(scope.row)"></el-button>
              </template>
            </el-table-column>
            <el-table-column prop="taskDate" label="date" width="180"></el-table-column>
            <el-table-column prop="taskName" label="Name"></el-table-column>
            <el-table-column label="Roomie(s)" :formatter="formatter"></el-table-column>
            <el-table-column prop="taskDes" label="Description"></el-table-column>
            <el-table-column label="Options">
              <template slot-scope="scope">
                <el-button size="mini" icon="el-icon-edit"></el-button>
                <el-button size="mini" icon="el-icon-delete" @click="deleteTask(scope.row)"></el-button>
              </template>
            </el-table-column>
          </el-table>
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
import {
  deleteTaskAsync,
  updateStateAsync,
  updateTaskAsync,
  getColocFilteredTasksAsync
} from "@/api/TaskApi";
export default {
  data() {
    return { taskList: [], colocId: null };
  },

  async mounted() {
    this.colocId = this.$currentColoc.colocId;
    this.refreshList();
  }, //end mounted
  computed: {
    activeTaskList() {
      return this.taskList;
    }
  }, //end computed
  methods: {
    async refreshList() {
      this.taskList = await getColocFilteredTasksAsync(this.colocId, true);
    },
    async deleteTask(task) {
      try {
        var r = await deleteTaskAsync(task.taskId);
        this.$emit("update-tasklist");
        this.refreshList();
      } catch (error) {
        console.error(error);
      }
    },
    async updateState(task) {
      try {
        var r = await updateStateAsync(task.taskId);
        this.$emit("update-tasklist");
        this.refreshList();
      } catch (e) {
        console.error(e);
      }
    },
    formatter(row, colunm) {
      var names = "";
      row.roomies.forEach(element => {
        names = names + " " + element.firstName;
      });
      return names;
    }
  } //end methods
};
</script>