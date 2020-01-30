<template>
  <div>
    <div>
      <template>
        <el-table :data="itemList" style="width: 100%">
          <el-table-column prop="itemName" label="Name" width="180"></el-table-column>
          <el-table-column prop="unitPrice" label="Unit Price" width="180"></el-table-column>

          <el-table-column prop="itemAmount" label="Quantity" width="180"></el-table-column>

          <el-table-column label="Options">
            <template slot-scope="scope">
              <el-button-group>
                <el-button size="mini" @click="quantityDecrease(scope.row)">-</el-button>
                <el-button size="mini" @click="quantityIncrease(scope.row)">+</el-button>
              </el-button-group>
              <el-button
                style="margin-left:10px"
                size="mini"
                type="danger"
                @click="handleDelete(scope.$index, scope.row)"
              >Delete</el-button>
            </template>
          </el-table-column>
          <el-table-column></el-table-column>
        </el-table>
      </template>
    </div>
  </div>
</template>
   

<script>
import { getItemsAsync } from "./../../api/GroceryApi.js";
import {
  deleteItemAsync,
  decreaseQuantityAsync,
  increaseQuantityAsync
} from "../../api/ItemApi.js";
export default {
  component: {}, //end component
  data() {
    return {
      drawer: false,
      item: {},
      itemList: []
    };
  }, //end data
  props: {
    //* id of the groceryList
    id: {
      type: Number,
      required: true
    }
  }, //end props
  computed: {}, //end computed
  async mounted() {
    await this.refreshItemList();
  }, //end mounted
  methods: {
    handleEdit(index, row) {
      this.item = row;
      this.handleEditDrawer();
    },
    async handleDelete(index, row) {
      await deleteItemAsync(row.itemId);
      await this.refreshItemList();
    },
    handleEditDrawer() {
      this.drawer = true;
      console.log("editDrawer", this.drawer);
    },
    //* update the item list
    async refreshItemList() {
      this.itemList = await getItemsAsync(this.id);
    },
    async quantityDecrease(row) {
      console.log(row.itemId);
      try {
        if (row.itemAmount == 0) {
          await deleteItemAsync(row.itemId);
        } else {
          await decreaseQuantityAsync(row.itemId);
        }
      } catch (error) {
        console.error;
      } finally {
        await this.refreshItemList();
      }
    },
    async quantityIncrease(row) {
      console.log(row.itemId);
      await increaseQuantityAsync(row.itemId);
      await this.refreshItemList();
    }
  } //end methods
};
</script>

<style>
</style>