<template>
  <div v-bind:style="actualTheme.appStyle">
    theme
    <el-button-group>
      <el-button
        v-for="(i, idx) in styles"
        :key="i.name"
        @click="setTheme(idx),menustyle()"
      >{{i.name}}</el-button>
    </el-button-group>
  </div>
</template>

<script>
import styles from "./Styles";

export default {
  data() {
    return {
      styles: styles,
      themeIdx: 0
    };
  }, //end data
  mounted() {
    console.log(this.styles);
    if (this.$cookies.get("themIdx") != undefined) {
      this.themeIdx = this.$cookies.get("themeIdx");
    } else {
      this.setTheme(0);
      this.themeIdx = this.$cookies.get("themeIdx");
    }
  }, //end mounted
  computed: {
    menustyle() {
      return this.styles[this.themeIdx];
    },
    actualTheme() {
      return this.styles[this.themeIdx];
    }
  }, //end computed
  methods: {
    setTheme(themeIdx) {
      this.$cookies.set("themeIdx", themeIdx);
      this.themeIdx = themeIdx;
      // this.$styles.setTheme(themeIdx);
    }
  } //end methods
};
</script>

<style>
</style>