<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span style="float: left;">Grocery List</span>
        <el-button style="float: right;">Add</el-button>
      </div>
      <div>
        <template>
          <el-table :data="getGroceries" style="width: 100%">
            <el-table-column type="expand">
              <template slot-scope="props">
                <p>props.row: {{ props.row }}</p>

                <Items :id="props.row.groceryListId" />
              </template>
            </el-table-column>
            <el-table-column prop="listName" label="Name"></el-table-column>
            <el-table-column prop="dueDate" label="Date" :formatter="dateFormatter"></el-table-column>
            <el-table-column>
              <el-button-group>
                <el-button>Add Item</el-button>
                <el-button>Update</el-button>
              </el-button-group>
            </el-table-column>
          </el-table>
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
import Items from "./GroceryListItems";
import { getGroceriesAsync } from "@/api/GroceryApi.js";
import { DateTime } from "luxon";

export default {
  components: {
    Items
  }, //end components
  data() {
    return {
      groceries: [],
      colocId: null
    };
  }, //end data
  async mounted() {
    this.colocId = this.$currentColoc.colocId;
    this.refresh();
  }, //end mounted
  computed: {
    getGroceries() {
      return this.groceries;
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
    }
  } //end methods
};
</script>

<style></style>
