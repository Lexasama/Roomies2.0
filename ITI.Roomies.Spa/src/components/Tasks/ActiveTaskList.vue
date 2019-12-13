<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Active Task</span>
      </div>
      <div>
        <template>
          <el-table :data="taskList" style="width: 100%">
            <el-table-column prop="state" label="State" width="180">
              <template slot="scope">
                <el-button round icon="el-icon-circle-check"></el-button>
              </template>
            </el-table-column>
            <el-table-column prop="taskDate" label="date" width="180"></el-table-column>
            <el-table-column prop="taskName" label="Name"></el-table-column>
            <el-table-column prop="roomie" label="Roomie"></el-table-column>
            <el-table-column prop="taskDes" label="Description"></el-table-column>
            <el-table-column label="Options">
              <template slot="scope">
                <el-button size="mini" icon="el-icon-edit" ></el-button>
                <el-button size="mini" icon="el-icon-delete"></el-button>
              </template>
            </el-table-column>
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