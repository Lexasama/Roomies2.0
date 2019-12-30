<template>
  <div>
    <b-row :gutter="20">
      <b-col>
        <tasksCreate />
      </b-col>
      <b-col>
        <history />
      </b-col>
    </b-row>
  </div>
</template>

<script>
import tasksCreate from "./TaskCreate";
import history from "./TaskHistory";

export default {
  components: {
    tasksCreate,
    history
  },
  data() {
    return {};
  }
};
</script>

<style>
</style>