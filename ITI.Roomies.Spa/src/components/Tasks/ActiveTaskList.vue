<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Active Task</span>
        <el-button @click="refreshList()">Refresh</el-button>
      </div>
      <div>
        <template>
          <el-table :data="activeTaskList" style="width: 100%">
            <el-table-column prop="state" label="State" width="180">
              <template slot="scope">
                <el-button round icon="el-icon-circle-check"></el-button>
              </template>
            </el-table-column>
            <el-table-column prop="taskDate" label="date" width="180"></el-table-column>
            <el-table-column prop="taskName" label="Name"></el-table-column>
            <el-table-column prop="firstName" label="Roomie(s)"></el-table-column>
            <el-table-column prop="taskDes" label="Description"></el-table-column>
            <el-table-column label="Options">
              <template slot-scope="scope">
                <el-button size="mini" icon="el-icon-edit"></el-button>
                <el-button size="mini" icon="el-icon-delete" @click="deleteTask(scope.row)">Delete</el-button>
              </template>
            </el-table-column>
          </el-table>
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
import { deleteTaskAsync } from "@/api/TaskApi";
export default {
  data() {
    return { taskList: [] };
  },
  props: {
    activeTasks: Array
  }, //end props
  async mounted() {
    this.refreshList();
  }, //end mounted
  computed: {
    activeTaskList() {
      return this.taskList;
    }
  }, //end computed
  methods: {
    async refreshList() {
      this.taskList = this.activeTasks;
      console.log("tasks");

      // const l = this.groupBy(this.taskList, t => t.taskId);
      // console.log(l);
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

    groupBy(list, keyGetter) {
      const map = new Map();

      list.forEach(i => {
        const key = keyGetter(i);
        const collection = map.get(key);
        if (!collection) {
          return map.set(key, [i]);
        } else {
          collection.push(i);
        }
      });
      return map;
    }
  } //end methods
};
</script>

<style>
</style>