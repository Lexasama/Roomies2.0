<template>
  <div>
    <el-card class="box-card">
      <div slot="header">
        <span>Item List</span>
      </div>
      <template>
        <el-table
          ref="multipleTable"
          :data="items"
          style="width: 100%"
          @selection-change="handleSelectionChange"
        >
          <el-table-column type="selection" width="55"> </el-table-column>
          <el-table-column property="itemName" label="Name">
          </el-table-column>
          <el-table-column property="unitPrice" label="Unit price">
          </el-table-column>

        </el-table>
        <div style="margin-top: 20px">
          <el-button
            @click="toggleSelection()"
            >Clear selection</el-button
          >
          <el-button @click="AddToList($event)">Add to List X {{multipleSelection.length}}</el-button>
        </div>
      </template>
    </el-card>
  </div>
</template>

<script>
  import {getAllItemAsync} from "../../api/ItemApi.js"
export default {
  data() {
    return {
      items: [],
      multipleSelection: [],
      tableData: []
    };
  }, //end data
  async mounted(){
    await this.refresh();
  }, //end mounted
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
    },
    async refresh(){
      this.items = await getAllItemAsync();
},
    async addToList(event){
event.preventDefault();
    }
  }, //end methods
};
</script>

<style></style>
