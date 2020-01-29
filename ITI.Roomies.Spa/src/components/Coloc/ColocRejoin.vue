<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Rejoin</span>
      </div>
      <div class="alert alert-danger" v-if="errors.length !== 0">
        <b>Invalid field :</b>
        <ul>
          <li v-for="e of errors" :key="e">{{e}}</li>
        </ul>
      </div>
      <el-form :inline="true">
        <el-form-item>
          <el-input placeholder="Enter your invite code" v-model="code"></el-input>
        </el-form-item>
        <el-button @click="submit($event)">Add</el-button>
      </el-form>
      <el-divider></el-divider>
    </el-card>
  </div>
</template>

<script>
import { invitedAsync } from "../../api/RoomieApi.js";
export default {
  data() {
    return { code: "", errors: [] };
  }, //end data
  methods: {
    async submit(event) {
      event.preventDefault();
      var errors = [];
      if (!this.code) errors.push("Enter your invite code");

      this.errors = errors;
      if (errors.length == 0) {
        try {
          var p = await invitedAsync(this.code);
          console.log(p);
          if (p !== "undefined") {
            this.show("You have been added succesfully", "success");
            this.$router.push("/");
          }
        } catch (e) {
          console.error(e);
          this.show("Invalid invite code", "error");
        }
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