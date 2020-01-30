<template>
  <div>
    <b-row>
      <b-col>
        <GroceryList />
      </b-col>
    </b-row>
  </div>
</template>

<script>
import GroceryList from "./GroceryList.vue";
export default {
  components: {
    GroceryList
  }
};
</script>

<style></style>
