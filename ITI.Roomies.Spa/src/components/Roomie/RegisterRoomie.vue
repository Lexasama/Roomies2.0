<template>
  <div>
    <div>
      <h1>Register a Roomie</h1>
    </div>

    <div>
      <el-steps :space="200" :active="active" finish-status="success" simple>
        <el-step title="Step 1" icon="el-icon-user-solid"></el-step>
        <el-step title="Step 2" icon="el-icon-edit"></el-step>
        <el-step title="Step 3" icon="el-icon-picture"></el-step>
      </el-steps>
    </div>

    <el-divider></el-divider>

    <b-form @submit="onSubmit($event)">
      <div class="alert alert-danger" v-if="errors.length > 0 ">
        <b>The following fields are invalid:</b>
        <ul>
          <li v-for="e of errors" :key="e">{{e}}</li>
        </ul>
      </div>

      <div v-if="active == 1">
        <b-form-group id="sex" label="You are :" label-cols-sm="4" label-cols-lg="3">
          <b-form-radio-group>
            <b-form-radio v-model="roomie.sex" value="1">Male</b-form-radio>
            <b-form-radio v-model="roomie.sex" value="2">Female</b-form-radio>
            <b-form-radio v-model="roomie.sex" value="3">Other</b-form-radio>
          </b-form-radio-group>
        </b-form-group>
        <b-form-group
          id="username"
          label="Username"
          label-for="username"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input
            id="username"
            placeholder="Entrez your Username"
            v-model="roomie.userName"
            :value="roomie.userName"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group
          id="nom"
          label-for="nom"
          label="Lastname:"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-input v-model="roomie.lastName" placeholder="Entrez your Lastname" required></b-input>
        </b-form-group>

        <b-form-group
          id="prenom"
          label="Votre prénom:"
          label-for="prenom"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input
            id="prenom"
            placeholder="Entrer your Firstname"
            v-model="roomie.firstName"
            :value="roomie.firstName"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group
          id="birthdate"
          label="Birthdate:"
          label-for="birthdate"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <input
            id="birthdate"
            class="form-control"
            type="date"
            v-model="roomie.birthDate"
            required
          />
        </b-form-group>
        {{roomie.birthDate}}
        <b-form-group
          id="phone"
          label="Phone"
          label-for="phone"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input
            id="phone"
            v-model="roomie.phone"
            placeholder="Entrer your phone number"
            required
          ></b-form-input>
        </b-form-group>
      </div>

      <div v-if="active == 2">
        <b-container fluid>
          <label for="description">Description:</label>
          <b-form-textarea
            id="description"
            v-model="roomie.description"
            placeholder="Describe yourself... "
          ></b-form-textarea>
        </b-container>
      </div>

      <div v-if="active == 3">
        <imageUploader :id="this.userId" isRoomie="true" />
      </div>
    </b-form>

    <el-divider></el-divider>

    <el-button-group>
      <el-button v-if="active>=2" icon="el-icon-arrow-left" @click="previousStep">Previous step</el-button>
      <el-button @click="nextStep" v-if="active<=2">
        Next step
        <i class="el-icon-arrow-right el-icon-right"></i>
      </el-button>
      <el-button v-if="active==3" @click="onSubmit($event)">Submit</el-button>
    </el-button-group>

    <el-divider></el-divider>
  </div>
</template>

<script>
import { DateTime } from "luxon";
import ImageUploader from "../Utility/ImageUploader";
import { findUserByEmailAsync, createRoomieAsync } from "../../api/RoomieApi";

export default {
  components: {
    ImageUploader
  },
  data() {
    return {
      roomie: {},
      test: {
        firstName: "a",
        lastName: "b",
        userName: "lexa",
        birtdate: new Date("1999,10,10"),
        phone: "0123456783",
        description: "j'aime les patates",
        sex: 1
      },
      id: null,
      userId: null,
      errors: [],
      active: 1,
      user: {}
    };
  },
  async mounted() {
    // this.mode = this.$route.params.mode;
    // this.id = this.$route.params.id;

    try {
      this.user = await findUserByEmailAsync();
      this.userId = this.user.userId;

      console.log(this.user);
      console.log(this.userId);

      this.roomie = this.test;
    } catch (error) {
      console.error(error);
    }
  },
  methods: {
    async nextStep() {
      if (this.active <= 3) {
        try {
          var errors = [];

          if (!this.roomie.userName) errors.push("Username");
          if (!this.roomie.lastName) errors.push("Lastname");
          if (!this.roomie.firstName) errors.push("Firstname");
          if (!this.roomie.birthDate) errors.push("Birthdate");
          if (!this.roomie.phone) errors.push("Phone");
          if (!this.roomie.sex) errors.push("Sexe");
          this.errors = errors;
          if (errors.length == 0) {
            this.active++;
          }
        } catch (e) {
          console.error(e);
        } finally {
        }
      }
    },
    previousStep() {
      if (this.active <= 3) {
        this.active--;
      }
    },

    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      if (!this.roomie.userName) errors.push("Username");
      if (!this.roomie.lastName) errors.push("Lastname");
      if (!this.roomie.firstName) errors.push("Firstname");
      if (!this.roomie.birthDate) errors.push("BirthDate");
      if (!this.roomie.phone) errors.push("Phone");
      if (!this.roomie.sex) errors.push("Sexe");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          await createRoomieAsync(this.roomie);
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>