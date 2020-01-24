<template>
  <div>
    <el-form v-model="item">
      <el-input v-model="item.itemName"></el-input>
      <el-input-number v-model="item.itemQuantity" @change="handleChange" :min="1"></el-input-number>
    </el-form>
  </div>
</template>

<script>
export default {
  props: {
    item: {
      type: Object,
      required: true
    }
  } //end props
};
</script>

<style>
</style>