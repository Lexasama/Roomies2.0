<template>
  <div>
    <ImageUploader v-bind:id="1" v-bind:isRoomie="true"></ImageUploader>
  </div>
</template>

<script>
import ImageUploader from "../components/Utility/ImageUploader.vue";
export default {
  components: {
    ImageUploader
  },
  data() {
    return {};
  }
};
</script>