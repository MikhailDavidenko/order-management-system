<template>
  <div id="app">
    <HeaderMenu />
    <router-view />
  </div>
</template>

<script>
import HeaderMenu from './components/HeaderMenu.vue';

export default {
  components: {
    HeaderMenu,
  }
};
</script>

<style>
</style>
