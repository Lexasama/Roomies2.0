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

          <el-form-item label="Due Date">
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

          <el-form-item>
            <el-button></el-button>
            <el-button type="primary" @click="onSubmit($event)">Submit</el-button>
          </el-form-item>
          {{task.taskDate}}
        </el-form>
      </div>
    </el-card>
  </div>
</template>

<script>
import { createTaskAsync } from "../../api/TaskApi";
import { getRoomiesAsync } from "../../api/RoomieApi";
import { DateTime } from "luxon";

export default {
  data() {
    return {
      date: "",
      time: "",
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
        },
        shortcuts: [
          {
            text: "Today",
            onClick(picker) {
              picker.$emit("pick", new Date());
            }
          },
          {
            text: "Tomorrow",
            onClick(picker) {
              const date = new Date();
              date.setTime(date.getTime() + 3600 * 1000 * 24);
              picker.$emit("pick", date);
            }
          },
          {
            text: "In a week",
            onClick(picker) {
              const date = new Date();
              date.setTime(date.getTime() + 3600 * 1000 * 24 * 7);
              picker.$emit("pick", date);
            }
          }
        ]
      },
      roomies: [
        // {
        //   roomieId: 1,
        //   firstName: "Axel"
        // },
        // {
        //   roomieId: 2,
        //   firstName: "Alex"
        // },
        // {
        //   roomieId: 3,
        //   firstName: "Al"
        // },
        // {
        //   roomieId: 4,
        //   firstName: "xel"
        // },
        // {
        //   roomieId: 5,
        //   firstName: "el"
        // },
        // {
        //   roomieId: 6,
        //   firstName: "Ax"
        // }
      ],
      assigned: []
    };
  },
  async mounted() {
    this.roomies = await getRoomiesAsync(this.$currentColoc.colocId);
    console.log("Roomies", this.roomies);
  }, //end mounted
  computed: {
    a: function() {
      console.log(this.assigned);
      return this.assigned;
    }
  }, //end computed
  methods: {
    async onSubmit(event) {
      event.preventDefault;

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
              this.show("Task created", "success");
            } else {
              this.show("Try again", "error");
            }
          } catch (error) {
            console.error(error);
          }
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
