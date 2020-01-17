<template>
  <div>
    <el-card>
      <div slot="header">
        <span>Active tasks</span>
      </div>
      <div>
        <template>
          <el-table :data="activeTaskList" height="250" style="width: 100%">
            <el-table-column prop="state" label="State" width="100">
              <template slot-scope="scope">
                <el-button round icon="el-icon-circle-check" @click="updateState(scope.row)"></el-button>
              </template>
            </el-table-column>
            <el-table-column prop="taskDate" label="date" width="180"></el-table-column>
            <el-table-column prop="taskName" label="Name"></el-table-column>
            <el-table-column label="Roomie(s)" :formatter="formatter"></el-table-column>
            <el-table-column prop="taskDes" label="Description"></el-table-column>
            <el-table-column label="Options">
              <template slot-scope="scope">
                <el-button-group>
                  <el-button size="mini" icon="el-icon-edit" @click="handleUpdate(scope.row)"></el-button>
                  <el-button size="mini" icon="el-icon-delete" @click="deleteTask(scope.row)"></el-button>
                </el-button-group>
              </template>
            </el-table-column>
          </el-table>
          <b-collapse v-model="visible">
            <el-card>
              <div slot="header">
                <span>Edit task</span>
              </div>
              <div>
                <taskUpdate :task="this.task" @task-updated="openClose()" />
              </div>
            </el-card>
          </b-collapse>
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
import {
  deleteTaskAsync,
  updateStateAsync,
  getColocFilteredTasksAsync
} from "@/api/TaskApi";
import taskUpdate from "./TaskUpdate.vue";
export default {
  components: {
    taskUpdate
  }, //end commponents
  data() {
    return {
      taskList: [],
      task: {},
      colocId: null,
      visible: false,
      activeNames: []
    };
  }, //en data

  async mounted() {
    this.colocId = this.$currentColoc.colocId;
    this.refreshList();
  }, //end mounted
  computed: {
    activeTaskList() {
      return this.taskList;
    }
  }, //end computed
  methods: {
    async refreshList() {
      this.taskList = await getColocFilteredTasksAsync(this.colocId, true);
      this.visible = false;
    },
    async deleteTask(task) {
      try {
        var r = await deleteTaskAsync(task.taskId);
        this.$emit("update-tasklist");
        this.refreshList();
      } catch (error) {
        console.error(error);
      }
    },
    async updateState(task) {
      try {
        var r = await updateStateAsync(task.taskId);
        this.$emit("update-tasklist");
        this.refreshList();
      } catch (e) {
        console.error(e);
      }
    },
    handleUpdate(task) {
      this.visible = !this.visible;
      this.task = task;
    },
    formatter(row, colunm) {
      var names = "";
      row.roomies.forEach(element => {
        names = names + " " + element.firstName;
      });
      return names;
    },
    openClose() {
      this.visible = !this.visible;
    }
  } //end methods
};
</script>