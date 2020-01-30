<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span style="float: left;">Grocery List</span>
        <el-button style="float: right;" @click="visible=!visible">Add</el-button>
      </div>
      <div>
        <b-collapse id="collapse" v-model="visible" class="mt-2">
          <GroceryListCreate @list-created="refresh()" />
        </b-collapse>
      </div>
      <div>
        <template>
          <el-table :data="getGroceries" style="width: 100%">
            <el-table-column type="expand">
              <template slot-scope="props">
                <Items :id="props.row.groceryListId" />
              </template>
            </el-table-column>
            <el-table-column prop="listName" label="Name"></el-table-column>
            <el-table-column prop="dueDate" label="Date" :formatter="dateFormatter"></el-table-column>
            <el-table-column>
              <template slot-scope="props">
              <el-button-group>
                <el-button @click="drawer=!drawer">Add Item</el-button>
                <el-button @click="deleteGroceryList(props.row.groceryListId)">Delete</el-button>
              </el-button-group>
        </template>
            </el-table-column>
          </el-table>
        </template>
      </div>

      <el-drawer :visible.sync="drawer" direction="rtl">
        <ItemCreate :groceryListId="groceryListId" />
      </el-drawer>
    </el-card>
  </div>
</template>

<script>
import Items from "./GroceryListItems";
import ItemCreate from "./../Items/ItemCreate.vue";
import GroceryListCreate from "./GroceryListCreate.vue";
import { getGroceriesAsync, deleteGroceryListAsync } from "@/api/GroceryApi.js";
import { DateTime } from "luxon";

export default {
  components: {
    Items,
    GroceryListCreate,
    ItemCreate
  }, //end components
  data() {
    return {
      visible:false,
      drawer: false,
      groceries: [],
      colocId: null,
      groceryListId: 0
    };
  }, //end data
  async mounted() {
    this.colocId = this.$currentColoc.colocId;
    this.refresh();
  }, //end mounted
  computed: {
    getGroceries() {
      return this.groceries;
    },
    getGroceryListId() {
      return this.groceryListId;
    }
  }, //end computed
  methods: {
    async refresh() {
      this.groceries = await getGroceriesAsync(this.colocId);
    },

    dateFormatter(row, colunm) {
      let d = DateTime.fromISO(row.dueDate).toISODate();
      let c = new Date(row.dueDate);
      let date = c.getDate() + "/" + (c.getMonth() + 1) + "/" + c.getFullYear();
      return date;
    },
    async deleteGroceryList(id){
      try{
        await deleteGroceryListAsync(id)
      }catch(e){
        console.error(e);
      }
    }
  } //end methods
};
</script>

<style></style>
