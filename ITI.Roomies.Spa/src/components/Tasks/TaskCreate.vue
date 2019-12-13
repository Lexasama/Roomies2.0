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

          <el-form-item label="Due Date" prop="date">
            <el-date-picker
              type="date"
              v-model="task.taskDate"
              :picker-options="pickerOptions"
              placeholder="Pick a date"
            ></el-date-picker>
            <el-time-select v-model="task.taskTime" placeholder="Pick a time"></el-time-select>
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

          <el-form-item>
            <el-button></el-button>
            <el-button type="primary" @click="onSubmit($event)">Submit</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      rules: {
        taskName: [
          { required: true, message: "Please input task name", trigger: "blur" }
        ],
        date: [
          {
            type: "date",
            required: true,
            message: "Please pick a date",
            trigger: "change"
          }
        ],
        roomies: [
          {
            type: "array",
            required: true,
            message: "Please assign at least one roomie",
            trigger: "change"
          }
        ]
      },
      task: {
        taskName: "",
        taskDate: "",
        taskTime: "",
        description: "",
        colocId: null,
        roomies: []
      },
      pickerOptions: {
        disabledDate(time) {
          let date = new Date();
          date.setDate(date.getDate() - 1);
          return time.getTime() <= date;
        }
      },
      roomies: [
        {
          roomieId: 1,
          firstName: "Axel"
        },
        {
          roomieId: 2,
          firstName: "Alex"
        },
        {
          roomieId: 3,
          firstName: "Al"
        },
        {
          roomieId: 4,
          firstName: "xel"
        },
        {
          roomieId: 5,
          firstName: "el"
        },
        {
          roomieId: 6,
          firstName: "Ax"
        }
      ],
      assigned: []
    };
  },
  async mounted() {}, //end mounted
  computed: {
    a: function() {
      console.log(this.assigned);
      return this.assigned;
    }
  }, //end computed
  methods: {
    async onSubmit(event) {
      event.preventDefault;
      this.$refs["taskForm"].validate(valid => {
        if (valid) {
          var task = {
            taskName: ""
          };

          this.show("Task created", "success");
        } else {
          console.log("error submit!!");
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
