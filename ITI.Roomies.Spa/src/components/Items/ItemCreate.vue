<template>

  <div>

      <div slot="header" >
        <span>Add or create an Item</span>
      <RecurrentList/>
      </div>
  </div>
</template>

<script>
import RecurrentList from "../../components/Groceries/ReccurentItemList"
  export default {
  components:{
    RecurrentList
  }, //end components
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