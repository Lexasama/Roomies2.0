<template>
  <div>
    <el-card class="box-card">
      <div slot="header">
        <span>Create</span>
      </div>
      <el-form ref="form" :model="item" label-width="120px">
        <el-form-item label="Name" required>
          <el-input v-model="item.itemName"></el-input>
        </el-form-item>

        <el-form-item label="Unit price" required>
          <el-input-number v-model="item.unitPrice" :step="1"></el-input-number>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="onSubmit($event)">Create and Add</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { createItemAsync } from "./../../api/ItemApi.js";
export default {
  components: {}, //end components
  props: {
    groceryListId: {
      type: Number,
      required: true
    }
  }, //end props
  data() {
    return {
      errors: [],
      item: {}
    };
  }, //end data
  methods: {
    async onSubmit(event) {
      event.preventDefault();
      var errors = [];

      if (!this.item.itemName) errors.push("Name");
      if (!this.item.unitPrice) errors.push("Price");
      this.errors = errors;
      if (this.errors.length == 0) {
        try {
          var p = await createItemAsync(this.item);
          this.show("Grocery list created", "success");
          this.$emit("item-created");
        } catch (error) {
          console.error(error);
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
  }
};
</script>

<style>
</style>