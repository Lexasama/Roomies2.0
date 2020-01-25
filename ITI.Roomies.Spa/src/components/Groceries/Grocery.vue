<template>
  <div>
    <b-row>
      <b-col>
        <GroceryList />
      </b-col>
      <!-- <b-col>
        <ReccurentItemList />
      </b-col>-->
    </b-row>
  </div>
</template>

<script>
import GroceryList from "./GroceryList.vue";
import ReccurentItemList from "./ReccurentItemList.vue";
export default {
  components: {
    GroceryList,
    ReccurentItemList
  }
};
</script>

<style></style>
