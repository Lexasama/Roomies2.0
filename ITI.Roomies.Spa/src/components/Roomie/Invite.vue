<template>
  <div>
    <el-card>
      <H1>Invite to {{ colocName }}</H1>

      <el-divider></el-divider>
      <el-form ref="emailList" :model="emailList">
        <el-form-item
          v-for="(email, index) in emailList.emails"
          :label="'Email' + index"
          :key="email.key"
          :prop="'emails.' + index + '.value'"
          :rules="emailRules"
        >
          <el-row>
            <el-col :span="11">
              <el-input v-model="email.value"></el-input>
            </el-col>
            <el-col :span="3">
              <el-button @click.prevent="removeEmail(email)">Delete</el-button>
            </el-col>
          </el-row>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="submitForm('emailList')">Submit</el-button>
          <el-button @click="addEmail">New Email</el-button>
          <el-button @click="resetForm('emailList')">Reset</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>
<script>
import { inviteAsync } from "../../api/RoomieApi";
export default {
  data() {
    return {
      mailingModel: {
        emails: [],
        colocId: null
      },
      colocName: "",
      emailList: {
        emails: [
          {
            key: null,
            value: ""
          }
        ]
      },
      list: [],
      emailRules: [
        {
          required: true,
          message: "Please input email address",
          trigger: "blur"
        },
        {
          type: "email",
          message: "Please input correct email address",
          trigger: ["blur", "change"]
        }
      ]
    };
  },
  async mounted() {
    this.colocName = this.$currentColoc.colocName;
  }, //end mounted
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate(async valid => {
        if (valid) {
          console.log("emailList", this.emailList);

          this.emailList.emails.forEach(e => {
            this.list.push(e.value);
          });

          this.mailingModel.emails = this.list;
          this.mailingModel.colocId = this.$currentColoc.colocId;

          await inviteAsync(this.mailingModel);
          this.show("Invitation(s) sent", "success");
          this.list = [];
        } else {
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
    removeEmail(item) {
      var index = this.emailList.emails.indexOf(item);
      if (index !== -1) {
        this.emailList.emails.splice(index, 1);
      }
    },
    addEmail() {
      this.emailList.emails.push({
        key: Date.now(),
        value: ""
      });
    },
    show(text, type) {
      this.$message({
        showClose: true,
        message: text,
        type: type
      });
    }
  } //end  methods
};
</script>
