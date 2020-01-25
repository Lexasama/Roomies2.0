<template>
  <div>
    <el-card class="box-card">
      <div slot="header">
        <span>Add Task</span>
      </div>
      <div>
        <el-form :model="task" :rules="rules" ref="taskForm">
          <el-form-item label="Task name" prop="taskName">
            <el-input v-model="task.taskName" style="width: 75%;"></el-input>
          </el-form-item>

          <el-form-item label="Due Date" required>
            <el-date-picker
              type="datetime"
              v-model="task.taskDate"
              :picker-options="pickerOptions"
              default-time="12:00:00"
              placeholder="Pick a date"
              format="dd/MM/yyyy HH:mm"
            ></el-date-picker>
          </el-form-item>

          <el-form-item label="Assign to: " prop="roomies">
            <el-checkbox-group v-model="task.roomies">
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
    </el-card>
  </div>
</template>

<script>
import { createTaskAsync } from "../../api/TaskApi";
import { getRoomiesAsync } from "../../api/RoomieApi";

import rules from "../Tasks/rules";
import pickerOptions from "../Tasks/PickerOptions";

export default {
  data() {
    return {
      date: "",
      time: "",
      rules: rules,
      pickerOptions: pickerOptions,
      task: {
        taskName: "",
        taskDate: "",
        taskDes: "",
        colocId: null,
        roomies: []
      },
      roomies: []
    };
  },
  async mounted() {
    this.roomies = await getRoomiesAsync(this.$currentColoc.colocId);
  }, //end mounted
  methods: {
    async onSubmit(event) {
      event.preventDefault();

      this.$refs["taskForm"].validate(async valid => {
        if (valid) {
          var d = Date.parse(this.task.taskDate.toString());
          var i = new Date(d).toISOString();

          this.task.taskDate = i;
          var task = {
            taskName: this.task.taskName,
            taskDes: this.task.taskDes,
            taskDate: this.task.taskDate,
            roomies: this.task.roomies,
            colocId: this.$currentColoc.colocId
          };
          try {
            var i = await createTaskAsync(task);
            if (i != null) {
              this.$emit("update-tasklist");
              this.show("Task created", "success");
              this.$refs["taskForm"].resetFields();
            } else {
              this.show("Try again", "error");
            }
          } catch (error) {
            console.error(error);
          }
        } else {
          this.show("Some fields are invalid", "error");
          return false;
        }
      });
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
