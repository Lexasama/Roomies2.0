<template>
  <div>
    <el-container>
      <el-asside>
        <navMenu />
      </el-asside>
      <el-container>
        <el-header>Settings</el-header>
        <el-main>
          <theme />
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import navMenu from "./NavMenu.vue";
import theme from "./Theme";
export default {
  components: {
    navMenu,
    theme
  }
};
</script>

<style>
</style>