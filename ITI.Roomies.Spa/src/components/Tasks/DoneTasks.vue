<template>
  <div>
    <el-card>
      <div slot="header">
        <span>DONE Tasks</span>
      </div>
      <div>
        <template>
          <el-table :data="taskList" style="width: 100%">
            <el-table-column prop="date" label="Date" width="180"></el-table-column>
            <el-table-column prop="name" label="Name" width="180"></el-table-column>
            <el-table-column prop="address" label="Description"></el-table-column>
          </el-table>
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      taskList: []
    };
  }
};
</script>

<style>
</style>