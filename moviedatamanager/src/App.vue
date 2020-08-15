<template>
  <v-app>
    <v-main>
    <MovieTable />
    </v-main>
  </v-app>
</template>

<script>
import MovieTable from "./components/MovieTable.vue";

export default {
  name: "App",
  components: {
    MovieTable,
  },
};
</script>

<style>
</style>
