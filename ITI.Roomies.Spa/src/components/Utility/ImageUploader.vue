<template>
  <div>
    <!-- <form enctype="multipart/form-data">
      <input type="file" name="image" accept="image/x-png, image/jpg, image/jpeg" />
    </form>-->

    <!-- <b-form-file
      accept="image/*"
      v-model="fileList"
      :state="Boolean(fileList[0])"
      placeholder="Choose a file or drop it here..."
      drop-placeholder="Drop file here..."
    ></b-form-file>

    <el-button
      style="margin-left: 10px;"
      size="small"
      type="success"
      @click="submitUpload"
    >upload to server</el-button>
    <div class="el-upload__tip" slot="tip">jpg/png files with a size less than 500kb</div>-->
    <div>
      <form action="POST" enctype="multipart/form-data">
        <input
          type="file"
          name="file"
          id="file"
          accept="image/x-png, image/jpg, image/jpeg"
          @change="handleFileUpload($event.target.files)"
        />>
        <el-button type="button" @click="submitFile()">Importer une photo</el-button>
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
      required: true
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
    submitUpload: async function() {
      event.preventDefault();

      const endpoint = process.env.VUE_APP_BACKEND + "/api/picture";

      // if (this.isRoomie == true) {
      //   file.append("roomieId", parseInt(this.id));
      // } else {
      //   file.append("colocId", parseInt(this.id));
      //}

      console.log(this.fileList);
      var formData = new FormData();

      console.log(this.isRoomie);
      console.log(this.id);

      let data = await axios.post(
        `${endpoint}/uploadColoc/${this.id}/${this.isRoomie}`,
        formData.append("file", this.fileList[0]),
        {
          headers: {
            "Content-type": "multipart/form-data",
            Authorization: `Bearer ${AuthService.accessToken}`
          },
          responseType: "application/json"
        }
      );
    },

    async submitFile() {
      event.preventDefault();
      const endpoint = process.env.VUE_APP_BACKEND + "/api/picture";
      const file = this.file;
      file.append("roomieId", parseInt(this.roomieId));

      var id = this.roomieId;
      var isRoomie = this.isRoomie;
      console.log(this.isRoomie);
      console.log(this.id);
      let data = await axios.post(
        `${endpoint}/uploadColoc/${this.id}/${this.isRoomie}`,
        this.file,
        {
          headers: {
            "Content-type": "multipart/form-data",
            Authorization: `Bearer ${AuthService.accessToken}`
          },
          responseType: "application/json"
        }
      );
    },
    async handleFileUpload(files) {
      console.log(files);
      this.file.append("file", files[0], files[0].name);
      console.log(this.file);
    }
  }
};
</script>

<style lang="scss" scoped></style>
