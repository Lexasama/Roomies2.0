<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Create a grocery List</span>
      </div>

      <el-form ref="form" :model="groceryList" label-width="120px">
        <el-form-item label="Name" required>
          <el-input v-model="groceryList.name"></el-input>
        </el-form-item>

        <el-form-item label="Date" required>
          <el-date-picker
            type="date"
            placeholder="Pick a date"
            v-model="groceryList.dueDate"
            format="dd/MM/yyyy"
            style="width: 100%;"
          ></el-date-picker>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="onSubmit($event)">Create</el-button>
          <el-button>Cancel</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { createGroceryListAsync } from "./../../api/GroceryApi.js";
export default {
  data() {
    return {
      errors: [],
      groceryList: {
        colocId: null,
        roomieId: null,
        name: "",
        dueDate: ""
      }
    };
  }, //end data
  mounted() {
    this.groceryList.colocId = this.$currentColoc.colocId;
    this.groceryList.roomieId = this.$user.roomieId;
  }, //end mounted
  methods: {
    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      if (!this.groceryList.name) errors.push("Name");
      if (!this.groceryList.dueDate) errors.push("Date");

      this.errors = errors;

      if (this.errors.length == 0) {
        try {
          var p = await createGroceryListAsync(this.groceryList);
          this.show("Grocery list created", "success");
          this.$emit("list-created");
        } catch (e) {
          console.error(e);
          this.show("Try again", "error");
        }
      } else {
        this.show("Invalid field(s)", "error");
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

<style>
</style>