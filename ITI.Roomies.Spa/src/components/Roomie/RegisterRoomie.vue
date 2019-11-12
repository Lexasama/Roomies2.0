<template>
  <div>
    <div>
      <h1 v-if="mode == 'create'">Enregistrer un Roomie</h1>
      <h1 v-else>Editer son Profil</h1>
    </div>
    <el-steps :active="active" finish-status="success">
      <el-step title="Step 1"></el-step>
      <el-step title="Step 2"></el-step>
      <el-step title="Step 3"></el-step>
    </el-steps>

    <b-form @submit="onSubmit($event)">
      <div class="alert alert-danger" v-if="errors.length > 0 ">
        <b>Les champs suivant sont invalides:</b>
        <ul>
          <li v-for="e of errors">{{e}}</li>
        </ul>
      </div>

      <div v-if="active == 0">
        <b-form-group id="sex" label="Vous êtes :" label-cols-sm="4" label-cols-lg="3">
          <b-form-radio-group>
            <b-form-radio v-model="roomie.sex" value="1">Monsieur</b-form-radio>
            <b-form-radio v-model="roomie.sex" value="2">Madame</b-form-radio>
            <b-form-radio v-model="roomie.sex" value="3">Autre</b-form-radio>
          </b-form-radio-group>
        </b-form-group>
        <b-form-group
          id="username"
          label="username"
          label-for="username"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input
            id="username"
            place
            holder="Entrez vote nom d'utilisateur"
            v-model="roomie.username"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group
          id="nom"
          label-for="nom"
          label="Vôtre nom:"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input v-model="roomie.lastName" placeholder="Entrez votre nom" required></b-form-input>
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
            placeholder="Entrez vôtre nom"
            v-model="roomie.firstName"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group
          id="birthdate"
          label="Date de naissance"
          label-for="birthdate"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input
            id="birthdate"
            v-model="roomie.birthDate"
            placeholder="entrer votre date de naissance"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group
          id="phone"
          label="téléphone"
          label-for="phone"
          label-cols-sm="4"
          label-cols-lg="3"
        >
          <b-form-input
            id="phone"
            v-model="roomie.phone"
            placeholder="Entrez vore numéro de télephone"
            required
          ></b-form-input>
        </b-form-group>
      </div>

      <div v-if="active == 1">
        <b-container fluid>
          <label for="description">Description:</label>
          <b-form-textarea
            id="description"
            v-model="roomie.description"
            placeholder="Décrivez vous en quelque mots ... "
          ></b-form-textarea>
        </b-container>
      </div>

      <div v-if="active == 2">
        <b-form-file
          v-model="file"
          accept="image/*"
          :state="Boolean(file)"
          placeholder="choisissez ou déposez ici..."
          drop-placeholder="Déposer ici..."
        ></b-form-file>
        <div class="mt-3">Selected file: {{ file ? file.name : '' }}</div>
      </div>
    </b-form>

    <el-button-group>
      <el-button v-if="active>=1" icon="el-icon-arrow-left" @click="previousStep">Previous step</el-button>
      <el-button @click="nextStep">
        Next step
        <i class="el-icon-arrow-right el-icon-right"></i>
      </el-button>
    </el-button-group>
  </div>
</template>

<script>
import {
  createRoomieAsync,
  updateRoomieAsync,
  getRoomieAsync
} from "../../api/RoomieApi";
import { DateTime } from "luxon";

export default {
  data() {
    return {
      roomie: {},
      mode: "create",
      id: null,
      errors: [],
      active: 0,
      file: null
    };
  },
  async mounted() {
    this.mode = this.$route.params.mode;
    this.id = this.$route.params.id;

    if (this.mode == "edit") {
      try {
        const roomie = getRoomieAsync(this.id);

        roomie.birthDate = DateTime.fromISO(roomie.birthDate).toISODate();

        this.roomie = roomie;

        this.errors.push(this.roomie.errorMessage);
      } catch (e) {
        console.error(e);
      }
    }
  },
  methods: {
    nextStep() {
      if (this.active++ > 2) this.active = 0;
      if (this.active == 2) onSubmit();
    },
    previousStep() {
      if (this.active-- <= 0) this.active = 0;
    },

    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      if (!this.roomie.lastName) errors.push("Nom");
      if (!this.roomie.firstName) errors.push("Prénom");
      if (!this.roomie.birthDate) errors.push("BirthDate");
      if (!this.roomie.sex) errors.push("sexe");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          if (this.mode == "create") {
            await createRoomieAsync(this.roomie);
          } else {
            await updateRoomieAsync(this.roomie);
          }
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>