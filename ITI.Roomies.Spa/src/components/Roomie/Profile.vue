<template>
  <div>
    <div>
      <b-form @submit="onSubmit($event)">
        <el-card shadow="hover">
          <b-row>
            <b-col>
              <b-media>
                <template>
                  <b-img :src="roomie.PicPath" rounded width="180"></b-img>
                </template>
                <div>
                  <b-button v-b-toggle.collapse-1 variant="primary">Changer de photo</b-button>
                  <b-collapse id="collapse-1" class="mt-2">
                    <b-card>
                      <ImageUploader :id="this.roomieId" isRoomie="true" />
                    </b-card>
                  </b-collapse>
                </div>
                <H1>SEX</H1>
              </b-media>
            </b-col>
          </b-row>
        </el-card>

        <!-- NAME EMAIL PHONE -->

        <el-card shadow="hover">
          <b-row>
            <b-col>
              <b-form-group id="name" label-cols-md="1" label="Prénom">
                <b-form-input v-model="roomie.FirstName" :value="roomie.FirstName"></b-form-input>
              </b-form-group>
            </b-col>
            <b-col>
              <b-form-group id="lastname" label-cols-md="1" label="Nom">
                <b-form-input v-model="roomie.LastName" :value="roomie.LastName"></b-form-input>
              </b-form-group>
            </b-col>
          </b-row>

          <b-form-group id="email" label="Email" label-for="email" label-cols="1" label-cols-lg="2">
            <b-row>
              <b-col>
                <b-form-input id="email" v-model="roomie.Email" :value="roomie.Email"></b-form-input>
              </b-col>
            </b-row>
          </b-form-group>
          <b-row>
            <b-col>
              <b-form-group label-cols="4" label-cols-lg="2" label="Phone" label-for="phone">
                <b-form-input id="phone" v-model="roomie.Phone" :value="roomie.Phone"></b-form-input>
              </b-form-group>
            </b-col>
          </b-row>
        </el-card>

        <!-- DESCRIPTION -->
        <el-card shadow="hover">
          <b-form-group label="Description" label-for="desc">
            <b-form-textarea label="desc" :value="roomie.Description" size="lg" rows="8"></b-form-textarea>
          </b-form-group>
        </el-card>
      </b-form>
    </div>
  </div>
</template>

<script>
import Roomie from "../../components/Roomie/Roomie.js";
import ImageUploader from "../Utility/ImageUploader.vue";
import {
  updateRoomieAsync,
  getRoomieProfileAsync,
  getRoomieAsync
} from "../../api/RoomieApi";
import { Server } from "http";
export default {
  components: {
    ImageUploader
  },
  data() {
    return {
      roomie: {},
      roomieId: null
    };
  },

  async mounted() {
    this.refreshInfo();
  },
  methods: {
    async refreshInfo() {
      try {
        this.roomie = await getRoomieProfileAsync();
        this.roomieId = this.roomie.id;
      } catch (error) {
        console.error(error);
      }
    },

    async onSubmit(event) {
      envent.preventDefault();

      var errors = [];

      if (!this.roomie.lastName) errors.push("Nom");
      if (!this.roomie.firstName) errors.push("Prénom");
      if (!this.roomie.birthDate) errors.push("BirthDate");
      if (!this.roomie.phone) errors.push("Phone");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          await updateRoomieAsync(this.roomie);
        } catch (e) {
          console.log(e);
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
</style>