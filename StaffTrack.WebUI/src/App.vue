<template>
  <v-app>
    <div>
      <v-app-bar v-if="isAuthenticated" color="deep-purple accent-4" dense dark class="mb-4">
        <v-toolbar-title class="mr-3">Staff Track</v-toolbar-title>
        <v-btn text to="/"> Anasayfa</v-btn>
        <v-btn text to="/users"> Personeller</v-btn>
        <v-btn text to="/admins"> Yöneticiler</v-btn>
        <v-spacer></v-spacer>
        <v-toolbar-title class="mr-3" v-if="isAuthenticated">{{ getAdminCredentials.name +' '+ getAdminCredentials.lastName }}</v-toolbar-title>
        <v-btn v-if="isAuthenticated" text @click="logout"> Çıkış</v-btn>
      </v-app-bar>
    </div>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import {store} from "@/store";

export default {
  data() {
    return {
    }
  },
  methods: {
    logout() {
      store.dispatch("logout");
    },
  },
  computed: {
    isAuthenticated() {
      return store.getters.isAuthenticated;
    },
    getAdminCredentials() {
      return store.getters.getAdminCredentials;
    }
  },
  mounted() {
    store.dispatch("initialize");
  }
};
</script>

<style>
</style>