<template>
  <div>
    <el-card>
      <b-form-group
        id="name-form"
        label-cols-sm="4"
        label-cols-lg="3"
        label="Enter name"
        description="The name of your flatsharing"
        label-for="name"
      >
        <b-form-input id="name" v-model="coloc.colocName"></b-form-input>
      </b-form-group>
      <el-divider></el-divider>
      <b-button block variant="primary" @click="onSubmit($event)">Create {{coloc.colocName}}</b-button>

      <el-divider></el-divider>
    </el-card>
  </div>
</template>

<script>
import { createColocAsync } from "../../api/ColocApi";

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
          console.log("SUBMITED");

          console.log(a);
          debugger;
          this.$router.replace(`/colocProfile/${colocId}`);
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>

<style lang="scss" scoped></style>
