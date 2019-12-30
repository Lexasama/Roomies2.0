<template>
  <div>
    <el-button @click="refreshList()">Refresh</el-button>
    <el-row>
      <el-col>
        <activeTasks :activeTasks="activeTaskList" @update-tasklist="evented" />
      </el-col>
    </el-row>
    <el-row>
      <doneTasks />
    </el-row>
  </div>
</template>

<script>
import doneTasks from "./DoneTasks";
import activeTasks from "./ActiveTaskList";

import { getColocTaskList } from "@/api/TaskApi";

export default {
  components: {
    activeTasks,
    doneTasks
  }, //end components
  data() {
    return {
      taskList: [],
      colocId: null
    };
  }, //end data

  async mounted() {
    this.colocId = this.$currentColoc.colocId;
  }, //end mounted
  computed: {
    activeTaskList() {
      let list = [];
      list = this.taskList.filter(this.isActive);
      return list;
    },
    doneTaskList() {
      let list = [];
      list = this.taskList.filter(this.isDone);
      return list;
    }
  }, //end computed

  methods: {
    evented() {
      console.log("THE EVENT §§§ ");
    },
    async refreshList() {
      console.log("colocId", this.colocId);
      this.taskList = await getColocTaskList(this.colocId);
      console.log("TasksList", this.taskList);
    },
    isDone(task) {
      if (task.state == true) {
        return true;
      }
    },
    isActive(task) {
      if (task.state == false) {
        return true;
      }
    }
  } //end methods
};
</script>

<style>
</style>