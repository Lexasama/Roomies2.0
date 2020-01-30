<template>
  <div>
    <div>
      <template>
        <el-table :data="itemList" style="width: 100%">
          <el-table-column prop="itemName" label="Name" width="180"></el-table-column>
          <el-table-column prop="unitPrice" label="Unit Price" width="180"></el-table-column>

          <el-table-column prop="itemAmount" label="Quantity" width="180"></el-table-column>

          <el-table-column  prop="totalPrice" label="Total Price" width="180"></el-table-column>
          <el-table-column label="Options">
            <template slot-scope="scope">
                <el-button-group>
                <el-button size="mini">-</el-button>
                <el-button size="mini">+</el-button>
              </el-button-group>
              <el-button style="margin-left:10px"
                      size="mini"
                      type="danger"
                      @click="handleDelete(scope.$index, scope.row)"
              >Delete</el-button>
            </template>
          </el-table-column>
          <el-table-column >
           </el-table-column>
        </el-table>
      </template>
    </div>
  </div>
</template>
   

<script>
import { getItemsAsync } from "./../../api/GroceryApi.js";
import itemEdit from "../Items/ItemEdit.vue";
export default {
  component: {
    itemEdit
  }, //end component
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
  computed:{

  }, //end computed
  async mounted() {
    await this.refreshItemList();
  }, //end mounted
  methods: {
    handleEdit(index, row) {
      this.item = row;
      this.handleEditDrawer();
    },
    handleDelete(index, row) {
      console.log(index, row);
    },
    handleEditDrawer() {
      this.drawer = true;
      console.log("editDrawer", this.drawer);
    },
    //* update the item list
    async refreshItemList() {
      this.itemList = await getItemsAsync(this.id);
      this.itemList =  this.itemList.forEach(i => {
        console.log(i);
        i.totalPrice = (i.unitPrice * i.itemAmount)
      });
      console.log(this.itemList);
    }
  } //end methods
};
</script>

<style>
</style>