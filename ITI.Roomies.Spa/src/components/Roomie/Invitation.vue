<template>
  <div>
    <el-card>
      <div slot="header" class="clearfix">
        <span>Invitation</span>
        <div class="alert alert-danger" v-if="errors.length !== 0">
          <b>Invalid field :</b>
          <ul>
            <li v-for="e of errors" :key="e">{{e}}</li>
          </ul>
        </div>
        <el-form :inline="true">
          <el-form-item>
            <el-input placeholder="Enter your invite code" v-model="code"></el-input>
            <el-button @click="submit($event)">Add</el-button>
          </el-form-item>
        </el-form>
      </div>
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
          // this.$router.push("/");
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>

<style>
</style>