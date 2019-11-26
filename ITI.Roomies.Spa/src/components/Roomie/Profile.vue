<template>
  <div>
    <div>
      <el-card>
        <el-image style="width: 100px; height: 100px" :src="roomie.picturePath" fit="scale-down"></el-image>

        <el-popover placement="bottom" title="Upload a new profile picture" trigger="click">
          <el-button slot="reference" type="primary" icon="el-icon-edit" circle></el-button>
          <ImageUploader :id="this.roomieId" isRoomie="true" />
        </el-popover>

        <el-divider></el-divider>

        <el-form ref="name" :model="roomie" inline label-width="120px">
          <el-form-item label="Username">
            <el-input v-model="roomie.userName"></el-input>
          </el-form-item>

          <el-form-item label="Firstname">
            <el-input v-model="roomie.firstName"></el-input>
          </el-form-item>
          <el-form-item label="Lastname">
            <el-input v-model="roomie.lastName"></el-input>
          </el-form-item>
        </el-form>

        <el-form ref="form" :model="roomie">
          <el-form-item label="Email">
            <el-input v-model="roomie.email"></el-input>
          </el-form-item>
          <el-form-item label="Phone">
            <el-input v-model="roomie.phone"></el-input>
          </el-form-item>
          <el-form-item label="Description">
            <el-input type="textarea" v-model="roomie.description"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit($event)">Modify</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
  </div>
</template>

<script>
import ImageUploader from "../Utility/ImageUploader.vue";
import {
  updateRoomieAsync,
  getMyProfileAsync,
  getRoomieAsync
} from "../../api/RoomieApi";

export default {
  components: {
    ImageUploader
  },

  data() {
    return {
      roomieId: null,
      roomie: {},
      visible: false
    };
  },

  async mounted() {
    try {
      this.roomie = await getMyProfileAsync();

      this.roomieId = this.roomie.roomieId;
    } catch (error) {
      console.error(error);
    }
  }, //End mounted

  computed: {}, //End computed
  methods: {
    async refreshInfo() {
      try {
        this.roomie = await getMyProfileAsync();

        this.roomieId = this.roomie.roomieId;
      } catch (error) {
        console.error(error);
      }
    },

    async onSubmit(event) {
      event.preventDefault();

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
          console.error(e);
        } finally {
          this.refreshInfo();
        }
      }
    }
  } //end methods
};
</script>

<style lang="scss" scoped></style>
