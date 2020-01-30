<template>
  <div>
    <b-row>
      <b-col>
        <Create />
      </b-col>
      <b-col>
        <Join />
      </b-col>
    </b-row>
  </div>
</template>

<script>
import Join from "./ColocRejoin.vue";
import Create from "./ColocEdit.vue";
export default {
  components: {
    Join,
    Create
  }
};
</script>

<style>
</style>