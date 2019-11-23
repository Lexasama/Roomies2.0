<template>
  <div>
    <el-form ref="form" :model="coloc">
      <el-form-item label="Name" label-for="colocName">
        <el-input id="colocName" v-model="coloc.name" size="lg"></el-input>
      </el-form-item>
    </el-form>

    <div>
      <imageUploader :id="id" isRoomie="false" />
    </div>
  </div>
</template>

<script>
import ImageUploader from "../Utility/ImageUploader.vue";

export default {
  components: {
    ImageUploader
  },
  data() {
    return {
      coloc: {},
      mode: null,
      errors: [],
      id: 1,
      fileList: null
    };
  },

  async mounted() {
    this.mode = this.$route.params.mode;
    this.id = this.$route.params.id;

    if (this.mode == "edit") {
      try {
        const coloc = await getColocAsync(this.id);

        this.coloc = coloc;

        this.errors.push(this.coloc.errorMessage);
      } catch (errors) {
        console.error(errors);
        this.$router.replace("/coloc");
      }
    }
  },
  methods: {
    refresh() {},

    submitUpload() {
      this.$refs.upload.submit();
    },

    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      if (!this.coloc.Name) errors.push("Name");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          if (this.mode == "create") {
            await createColocAsync(this.coloc);
          } else {
            await updateColocAsync(this.coloc);
          }
        } catch (e) {
          console.error(e);
        } finally {
          this.refresh();
        }
      }
    }
  }
};
</script>

<style lang="scss" scoped></style>
