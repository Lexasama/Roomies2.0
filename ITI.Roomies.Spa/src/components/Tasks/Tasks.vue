<template>
  <div>
    <taskHistory />
    <!-- <b-row :gutter="20">
      <b-col>
        <tasksCreate @update-tasklist="refreshList()" />
      </b-col>
      <b-col>
        <b-col>
          <activeTasks @update-tasklist="refreshList()" />
        </b-col>
        <b-col>
          <doneTasks />
        </b-col>
      </b-col>
    </b-row>-->
  </div>
</template>

<script>
import tasksCreate from "./TaskCreate";
import activeTasks from "./ActiveTaskList";
import doneTasks from "./DoneTasks";
import TaskHistory from "./TaskHistory.vue";

export default {
  components: {
    tasksCreate,
    activeTasks,
    doneTasks,
    TaskHistory
  },
  data() {
    return {
      colocId: null
    };
  }, //end data
  async mounted() {
    this.colocId = this.$currentColoc.colocId;
    this.refreshList();
  }, //end mounted
  computed: {}, //end computed
  methods: {
    async refreshList() {
      ///should trigger refresch mzthods of the child components
    }
  } //end methods
};
</script>

<style>
</style>