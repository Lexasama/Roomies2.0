<template>
  <div>
    <div class="container p-3">
      <div class="text-center">
        <h1 class="my-4">Voulez-vous vous connecter en tant que :</h1>

        <b-card-group deck>
          <b-card class="chose" header="Roomie"></b-card>
          <b-card class="chose" header="Superviseur"></b-card>
        </b-card-group>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  }
};
</script>

<style lang="scss" scoped>
.chose:hover {
  box-shadow: 0 0 11px rgba(33, 33, 33, 0.2);
}
</style>