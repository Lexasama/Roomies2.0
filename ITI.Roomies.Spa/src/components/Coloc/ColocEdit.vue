<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Create</span>
      </div>
      <el-form :inline="true" :model="coloc">
        <el-form-item label="Enter name:" required>
          <el-input v-model="coloc.colocName"></el-input>
        </el-form-item>
        <el-button type="primary" @click="onSubmit($event)">Create {{coloc.colocName}}</el-button>
      </el-form>

      <el-divider></el-divider>
    </el-card>
  </div>
</template>

<script>
import { createColocAsync, getColocAsync } from "../../api/ColocApi";

export default {
  data() {
    return {
      coloc: {
        colocName: ""
      },
      errors: []
    };
  },

  async mounted() {},
  methods: {
    async onSubmit(event) {
      event.preventDefault();
      var errors = [];
      if (!this.coloc.colocName) errors.push("Name");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          let colocId = await createColocAsync(this.coloc);
          if (this.$currentColoc.colocId == -1) {
            await this.setColocs(colocId);
            var colocs = [this.$currentColoc];
            this.$colocs.setList(colocs);
          }
          this.show();
          this.$router.replace(`/colocProfile/${colocId}`);
        } catch (e) {
          console.error(e);
        }
      }
    },
    show() {
      this.$message({
        showClose: true,
        message: "Success",
        type: "success"
      });
    },
    async setColocs(colocId) {
      var coloc = await getColocAsync(colocId);
      this.$currentColoc.setCurrentColoc(coloc);
    }
  }
};
</script>

<style lang="scss" scoped></style>
