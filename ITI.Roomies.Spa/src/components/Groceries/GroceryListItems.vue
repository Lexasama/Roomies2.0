<template>
  <div>
    <div>
      <el-drawer title="Edit item" :visible.sync="drawer" direction="rtl">
        <itemEdit :item="this.item" />
      </el-drawer>
    </div>
    <div>
      <template>
        <el-table :data="itemList" style="width: 100%">
          <el-table-column prop="itemName" label="Name" width="180"></el-table-column>
          <el-table-column prop="itemPrice" label="Price" width="180"></el-table-column>
          <el-table-column label="Options">
            <template slot-scope="scope">
              <el-button-group>
                <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                <el-button
                  size="mini"
                  type="danger"
                  @click="handleDelete(scope.$index, scope.row)"
                >Delete</el-button>
              </el-button-group>
            </template>
          </el-table-column>
        </el-table>
      </template>
    </div>
  </div>
</template>
   

<script>
import itemEdit from "../Items/ItemEdit.vue";
export default {
  component: {
    itemEdit
  }, //end component
  data() {
    return {
      drawer: false,
      item: {},
      itemList: [
        { itemId: 1, itemName: "ItemName", itemPrice: "100" },
        { itemId: 2, itemName: "ItemName1", itemPrice: "30" },
        { itemId: 3, itemName: "ItemName2", itemPrice: "10" }
      ]
    };
  }, //end data
  props: {
    //* id of the groceryList
    id: {
      type: Number,
      required: true
    }
  }, //end props
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
      //this.itemList = await getItemsAsync(this.id);
    }
  } //end methods
};
</script>

<style>
</style>