<template>
  <div>
    <div>
      <radial-menu
        style="margin: auto; margin-top: 300px; background-color: white"
        :itemSize="50"
        :radius="130"
        :angle-restriction="360"
      >
        <radial-menu-item
          v-for="(item, index) in items"
          :key="index"
          style="background-color: white"
          @click="() => handleClick(item)"
        >
          <span>{{item}}</span>
        </radial-menu-item>
      </radial-menu>
      <div style="color: rgba(0,0,0,0.6); margin-top: 16px;">{{ lastClicked }}</div>
    </div>
  </div>
</template>

<script>
import { RadialMenu, RadialMenuItem } from "vue-radial-menu";

export default {
  components: {
    RadialMenu,
    RadialMenuItem
  },
  data() {
    return {
      items: [
        "Taks",
        "Calendar",
        "Goceries",
        "Budget",
        "Settings",
        "more",
        "Roomie1",
        "Roomie2",
        "Roomie3",
        "Roomie4",
        "Roomie5"
      ],
      lastClicked: "click on something!"
    };
  },
  methods: {
    handleClick(item) {
      this.lastClicked = item;
    }
  }
};
</script>

<style lang="scss">
</style>