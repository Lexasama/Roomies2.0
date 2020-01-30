<template>
  <div>
    <radial-menu
      style="margin: auto; margin-top: 300px; background-color: white"
      :itemSize="65"
      :radius="130"
      :angle-restriction="180"
    >
      <radial-menu-item
        v-for="(item, index) in items"
        :key="index"
        style="background-color: white"
        @click="() => handleClick(item)"
      >
        <span>{{ item }}</span>
      </radial-menu-item>
    </radial-menu>
    <div style="color: rgba(0,0,0,0.6); margin-top: 16px;">{{ lastClicked }}</div>
  </div>
</template>

<script>
import { RadialMenu, RadialMenuItem } from "vue-radial-menu";

export default {
  components: {
    RadialMenu,
    RadialMenuItem
  }, //end components
  data() {
    return {
      items: ["Flat", "Tasks", "Groceries", "Profile"],
      lastClicked: "click on something!"
    };
  }, //end return
  methods: {
    handleClick(item) {
      console.log("item", item);
      this.lastClicked = item;

      if (item == "Tasks") {
        this.$router.push("/Tasks");
      }
      if (item == "Settings") {
        this.$router.push("/settings");
      }
      if (item == "Groceries") {
        this.$router.push("/Groceries");
      }
      if (item == "Profile") {
        this.$router.push("/profile");
      }
      if (item == "Flat") {
        this.$router.push("/coloc");
      }
    }
  } //end methods
};
</script>

<style>
</style>