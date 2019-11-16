<template>
  <div>
    <form enctype="multipart/form-data">
      <input type="file" name="image" accept="image/x-png, image/jpg,
      image/jpeg"/>
    </form>
    
    <el-button
      style="margin-left: 10px;"
      size="small"
      type="success"
      @click="submitUpload"
      >upload to server</el-button
    >
    <div class="el-upload__tip" slot="tip">
      jpg/png files with a size less than 500kb
    </div>
  </div>
</template>

//action="http://localhost:5000/api/picture/uploadColoc"
<script>
import axios from "axios";
import AuthService from "../../services/AuthService";
export default {
  data() {
    props: ["id", "isRoomie"];
    return {
      dialogImageUrl: "",
      dialogVisible: false,
      fileList: null,
      id: 1,
      isRoomie: true,
      file: null
    };
  },

  methods: {
    handleImageUpload(files) {
      console.log(files);
      this.file.append("file", files[0], files[0].name);
      console.log(this.file);
      this.uploadButtonDisabled = false;
    },

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
        formData.append("file", this.fileList),
        {
          headers: {
            "Content-type": "multipart/form-data",
            Authorization: `Bearer ${AuthService.accessToken}`
          },
          responseType: "application/json"
        }
      );
    }
  }
};
</script>

<style lang="scss" scoped></style>
