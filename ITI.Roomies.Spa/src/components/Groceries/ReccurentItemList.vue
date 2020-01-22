<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>Item List</span>
        <el-button style="float: right; padding: 3px 0" type="text"
          >Operation button</el-button
        >
      </div>
      <template>
        <el-table
          ref="multipleTable"
          :data="tableData"
          style="width: 100%"
          @selection-change="handleSelectionChange"
        >
          <el-table-column type="selection" width="55"> </el-table-column>
          <el-table-column label="Date" width="120">
            <template slot-scope="scope">{{ scope.row.date }}</template>
          </el-table-column>
          <el-table-column property="name" label="Name" width="120">
          </el-table-column>
          <el-table-column
            property="address"
            label="Address"
            show-overflow-tooltip
          >
          </el-table-column>
        </el-table>
        <div style="margin-top: 20px">
          <el-button
            style="float: right; padding: 3px 0"
            @click="toggleSelection()"
            >Clear selection</el-button
          >
        </div>
      </template>
    </el-card>
    <div></div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      items: [],
      multipleSelection: [],
      tableData: []
    };
  }, //end data
  methods: {
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    }
  }
};
</script>

<style></style>
