<template>
  <div>
    <el-row>
      <el-col>
        <activeTasks ref="activeTasksComp" @update-tasklist="refreshDoneTasks()" />
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
    async refreshDoneTasks() {
      await this.$refs.doneTasksComp.refreshList();
    },
    async refreshActiveTasks() {
      await this.$refs.activeTasksComp.refreshList();
    }
  } //end methods
};
</script>