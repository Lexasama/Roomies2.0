<template>
  <div>
    <div>
      <form action="POST" enctype="multipart/form-data">
        <input
          type="file"
          name="file"
          id="file"
          accept="image/x-png, image/jpg, image/jpeg"
          @change="handleFileUpload($event.target.files)"
        />
        <el-button type="button" @click="submitFile()">Submit picture</el-button>
      </form>
    </div>
  </div>
</template>

//action="http://localhost:5000/api/picture/uploadColoc"
<script>
import axios from "axios";
import AuthService from "../../services/AuthService";
export default {
  props: {
    id: {
      type: Number,
      required: true,
      default: 0
    },
    isRoomie: {
      type: Boolean,
      required: true
    }
  },
  data() {
    return {
      files: new FormData(),
      model: {},
      env: process.env.VUE_APP_BACKEND,
      sizeMax: 5000000,
      file: new FormData(),
      env: process.env.VUE_APP_BACKEND
    };
  },

  async mounted() {
    console.log(this.id);
    console.log(this.isRoomie);
  },

  methods: {
    async submitFile() {
      event.preventDefault();
      const endpoint = process.env.VUE_APP_BACKEND + "/api/picture";
      const file = this.file;
      file.append("id", parseInt(this.id));
      file.append("isRoomie", this.isRoomie);

      var id = this.roomieId;
      var isRoomie = this.isRoomie;

      let data = await axios.post(`${endpoint}/uploadColoc`, this.file, {
        headers: {
          "Content-type": "multipart/form-data",
          Authorization: `Bearer ${AuthService.accessToken}`
        },
        responseType: "application/json"
      });
    },

    async handleFileUpload(files) {
      this.file.append("file", files[0], files[0].name);
    }
  }
};
</script>

<style lang="scss" scoped></style>
