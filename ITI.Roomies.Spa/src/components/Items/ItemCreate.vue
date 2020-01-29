<template>
  <div>{{groceryListId}}</div>
</template>

<script>
export default {
  props: {
    groceryListId: {
      type: Number,
      required: true
    }
  }
};
</script>

<style>
</style>