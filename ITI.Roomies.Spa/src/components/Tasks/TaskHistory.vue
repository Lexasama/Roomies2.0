<template>
  <div>
    <el-row>
      <el-col>
        <activeTasks />
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
export default {
  components: {
    activeTasks,
    doneTasks
  }
};
</script>

<style>
</style>