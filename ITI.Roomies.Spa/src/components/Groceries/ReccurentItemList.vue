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
          <el-table-column type="selection" width="55"></el-table-column>
          <el-table-column property="itemName" label="Name"></el-table-column>
          <el-table-column property="unitPrice" label="Unit price"></el-table-column>
        </el-table>
        <div style="margin-top: 20px">
          <el-button @click="toggleSelection()">Clear selection</el-button>
          <el-button
            type="primary"
            v-if="multipleSelection.length == 0"
            disabled
          >Add to List X {{multipleSelection.length}}</el-button>
          <el-button
            type="primary"
            v-if="multipleSelection.length != 0"
            @click="addToList($event)"
          >Add to List X {{multipleSelection.length}}</el-button>
        </div>
      </template>
    </el-card>
  </div>
</template>

<script>
import { getAllItemAsync } from "../../api/ItemApi.js";
import { AddItemsAsync } from "../../api/GroceryApi.js";
export default {
  data() {
    return {
      items: [],
      multipleSelection: [],
      tableData: [],
      itemsToAdd: [],
      groceryListItems: {
        itemIdList: [],
        groceryListId: null
      }
    };
  }, //end data
  props: {
    groceryListId: {
      type: Number,
      required: true
    }
  }, //end props
  async mounted() {
    this.groceryListItems.groceryListId = this.groceryListId;
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
    async refresh() {
      this.items = await getAllItemAsync();
    },
    async addToList(event) {
      event.preventDefault();
      let items = [];

      this.multipleSelection.forEach(i => {
        items.push(i.itemId);
      });

      this.groceryListItems.itemIdList = items;
      console.log(this.groceryListItems);

      try {
        var p = await AddItemsAsync(this.groceryListItems);
        this.show("Item added", "success");
        this.$emit("item-added");
      } catch (error) {
        this.show("Try again", "error");
        console.error(error);
      }
    },

    show(text, type) {
      this.$message({
        showClose: true,
        message: text,
        type: type
      });
    }
  } //end methods
};
</script>

<style></style>
