<template>
  <div>
    <el-card>
      <el-image style="width: 100px; height: 100px" :src="roomie.picturePath" fit="scale-down"></el-image>

      <el-divider></el-divider>

      <el-form ref="name" :model="roomie" inline label-width="120px">
        <el-form-item label="Firstname" disabled>
          <el-input v-model="roomie.firstName" :disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="Lastname">
          <el-input v-model="roomie.lastName" :disabled="true"></el-input>
        </el-form-item>
      </el-form>

      <el-form ref="form" :model="roomie">
        <el-form-item label="Email">
          <el-input v-model="roomie.email" :disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="Phone">
          <el-input v-model="roomie.phone" :disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="Description">
          <el-input type="textarea" v-model="roomie.description" :disabled="true"></el-input>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>
<script>
export default {
  props: {
    roomie: {
      type: Object,
      required: false,
      default: () => {}
    }
  }, //end props
  data() {
    return {};
  }, //End data
  async mounted() {
    if (this.roomie == null) {
      this.roomie = this.$user;
    }
    console.log("#roomiesProfile", this.roomie);
  } //end mounted
}; // END EXPORT DEFAULT
</script>