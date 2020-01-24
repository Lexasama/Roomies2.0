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
        <el-button type="button" @click="submitFile($event)">Submit</el-button>
      </form>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import AuthService from "../../services/AuthService";
import { getPicAsync } from "../../api/RoomieApi";
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
      file: new FormData()
    };
  },

  async mounted() {
    console.log("#ImageUploader id:", this.id);
    console.log("#ImageUploader isRoomie:", this.isRoomie);
  },

  methods: {
    async submitFile(event) {
      event.preventDefault();
      const endpoint = process.env.VUE_APP_BACKEND + "/api/picture";
      const file = this.file;
      file.append("id", parseInt(this.id));
      file.append("isRoomie", this.isRoomie);

      var id = this.roomieId;
      var isRoomie = this.isRoomie;
      try {
        let data = await axios.post(`${endpoint}/uploadImage`, this.file, {
          headers: {
            "Content-type": "multipart/form-data",
            Authorization: `Bearer ${AuthService.accessToken}`
          },
          responseType: "application/json"
        });
        console.log("data", data);

        if (data.status == 200) {
          this.show("Succes", "succes");
        }
      } catch (error) {
        console.error(error);
        this.show("Try again", "error");
      }
    },
    async handleFileUpload(files) {
      this.file.append("file", files[0], files[0].name);
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

<style lang="scss" scoped></style>
