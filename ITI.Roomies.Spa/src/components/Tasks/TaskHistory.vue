<template>
  <div>
    <el-button @click="print()">Refresh</el-button>
    <el-row>
      <el-col>
        <activeTasks ref="activeTaskComp" @update-tasklist="refreshDoneTasks()" />
      </el-col>
    </el-row>
    <el-row>
      <doneTasks ref="doneTasksComp" @update-tasklist="refreshActiveTasks()" />
    </el-row>
  </div>
</template>

<script>
import doneTasks from "./DoneTasks";
import activeTasks from "./ActiveTaskList";

export default {
  components: {
    activeTasks,
    doneTasks
  }, //end components
  methods: {
    async refreshDoneTasks(){
      console.log("Refresh Done Tasks")
      await this.$refs.doneTasksComp.refresh()
    },
    async refreshActiveTasks(){
      console.log("Refresh Active Tasks")
      await this.$refs.activeTasksComp.refresh()
    },
    async refresh() {
      await this.$refs.doneTasksComp.refreshList();
      this.$refs.activeTasksComp.refreshList();
    }
  } //end methods
};
</script>

<style>
</style>