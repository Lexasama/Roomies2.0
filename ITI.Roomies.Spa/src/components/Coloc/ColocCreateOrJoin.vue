<template>
  <div>
    <el-row>
      <el-col>
        <Create />
      </el-col>

      <el-col>
        <Join />
      </el-col>
    </el-row>
  </div>
</template>

<script>
import Join from "./ColocRejoin.vue";
import Create from "./ColocEdit.vue";
export default {
  components: {
    Join,
    Create
  }
};
</script>

<style>
</style>