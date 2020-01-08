<template>
  <div>
    <b-row :gutter="20">
      <b-col>
        <tasksCreate @update-tasklist="refreshList()" />
      </b-col>
      <b-col>
        <b-col>
          <taskHistory ref="taskHistoryComp" />
        </b-col>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import tasksCreate from "./TaskCreate";
import TaskHistory from "./TaskHistory.vue";

export default {
  components: {
    tasksCreate,
    TaskHistory
  }, //end component
  methods: {
    async refreshList() {
      await this.$refs.taskHistoryComp.refreshActiveTasks();
    }
  }
};
</script>