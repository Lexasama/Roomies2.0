<template>
  <div>
    <h1>This is the task update component</h1>

    <el-form :inline="true" :model="formInline" class="demo-form-inline">
      <el-form-item label="Approved by">
        <el-input v-model="formInline.user" placeholder="Approved by"></el-input>
      </el-form-item>
      <el-form-item label="Activity zone">
        <el-select v-model="formInline.region" placeholder="Activity zone">
          <el-option label="Zone one" value="shanghai"></el-option>
          <el-option label="Zone two" value="beijing"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">Query</el-button>
      </el-form-item>
    </el-form>
    <!-- <el-form :model="task">
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
    </el-form>-->
  </div>
</template>

<script>
export default {
  data() {
    return {};
  }, //end data
  props: {
    task: {
      type: Object,
      required: false
    }
  } //end props
};
</script>

<style>
</style>