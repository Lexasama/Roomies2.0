<template>
  <div>
    {{task}}
    <el-form :model="updatedTask" :rules="rules" ref="taskUpdate">
      <el-form-item label="Task name">
        <el-input v-model="task.taskName" style="width: 75%;" required></el-input>
      </el-form-item>

      <el-form-item label="Due Date" required>
        <el-date-picker
          type="datetime"
          v-model="task.taskDate"
          :picker-options="pickerOptions"
          format="dd/MM/yyyy HH:mm"
        ></el-date-picker>
      </el-form-item>
      <el-form-item label="Assign to: " prop="roomies">
        <el-checkbox-group v-model="updatedTask.roomies">
          <el-checkbox
            v-for="r in roomies"
            :key="r.roomieId"
            :label="r.roomieId"
            name="roomie"
          >{{r.firstName}}</el-checkbox>
        </el-checkbox-group>
      </el-form-item>
      <el-form-item label="Description">
        <el-input type="textarea" placeholder="Describe your task" v-model="task.taskDes"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit($event)">Submit</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { updateTaskAsync } from "../../api/TaskApi";
import { getRoomiesAsync } from "../../api/RoomieApi";

import rules from "../Tasks/rules";
import pickerOptions from "../Tasks/PickerOptions";
export default {
  data() {
    return {
      roomies: [],
      rules: rules,
      pickerOptions: pickerOptions,
      updatedTask: { roomies: [] }
    };
  }, //end data
  props: {
    task: {
      type: Object,
      required: true
    }
  }, //end props
  async mounted() {
    this.roomies = await getRoomiesAsync(this.$currentColoc.colocId);
    this.assigned();
    console.log("rules", this.rules);
  }, // end mounted
  computed: {}, //end computed
  methods: {
    assigned() {
      this.task.roomies.forEach(element => {
        this.updatedTask.roomies.push(element.roomieId);
      });
      return updatedTask.roomies;
    },
    dateFormat(date) {
      var d = Date.parse(date.toString());
      return new Date(d).toISOString();
    },
    async onSubmit(event) {
      event.preventDefault();

      this.task.taskDate = this.dateFormat(this.task.taskDate);
      var task = {
        taskId: this.task.taskId,
        taskName: this.task.taskName,
        taskDes: this.task.taskDes,
        taskDate: this.task.taskDate,
        roomies: this.updatedTask.roomies,
        colocId: this.$currentColoc.colocId
      };

      var errors = [];

      if (!this.task.taskName) errors.push("Task name");
      if (!this.task.taskDate) errors.push("Date");

      this.errors = errors;

      if (this.errors.length == 0) {
        try {
          debugger;
          await updateTaskAsync(task);
          this.show("Task updated", "success");
          this.$emit("task-updated");
        } catch (e) {
          console.error(e);
          this.show("Try again", "error");
        }
      } else {
        this.show("Some fields are invalid", "error");
      }
    },
    show(text, type) {
      this.$message({
        showClose: true,
        message: text,
        type: type
      });
    }
  } //end methods
};
</script>

<style>
</style>