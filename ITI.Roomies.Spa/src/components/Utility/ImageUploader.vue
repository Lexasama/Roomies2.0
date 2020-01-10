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
import { getPicAsync } from "../../api/RoomieApi";
import { getColocAsync } from "../../api/ColocApi";
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
      env: process.env.VUE_APP_BACKEND,
      file: new FormData(),
      env: process.env.VUE_APP_BACKEND
    };
  },

  async mounted() {
    console.log("#ImageUploader id:", this.id);
    console.log("#ImageUploader isRoomie:", this.isRoomie);
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

      let data = await axios.post(`${endpoint}/uploadImage`, this.file, {
        headers: {
          "Content-type": "multipart/form-data",
          Authorization: `Bearer ${AuthService.accessToken}`
        },
        responseType: "application/json"
      });
      console.log("data", data);
      if (data.status == 200) {
        this.show();
        this.setVariables();
      }
    },

    async handleFileUpload(files) {
      this.file.append("file", files[0], files[0].name);
    },
    show() {
      this.$message({
        showClose: true,
        message: "Success",
        type: "success"
      });
    },
    async setVariables() {
      if (this.isRoomie == true) {
        var path = await getPicAsync(this.id);
        this.$user.setPicPath(path.picturePath);
      }
      if (this.isRoomie == false && this.$currentColoc.colocId == -1) {
        var coloc = await getColocAsync(this.id);
        console.log("coloc", coloc);
        this.$currentColoc.setCurrentColoc(coloc);
      }
    }
  }
};
</script>

<style lang="scss" scoped></style>
