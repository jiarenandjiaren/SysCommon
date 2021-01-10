<template>
  <section class="app-main" ref="appMain">
    <transition name="fade-transform" mode="out-in">
      <keep-alive :include="cachedViews">
        <router-view :key="key"></router-view>
      </keep-alive>
    </transition>
  </section>
</template>

<script>
export default {
  name: 'AppMain',
  computed: {
    cachedViews() {
      return this.$store.state.tagsView.cachedViews
    },
    key() {
      return this.$route.fullPath
    }
  },
  watch: {
    $route() {
      this.$refs.appMain.scrollTop = 0
    }
  }
}
</script>

<style scoped>
.app-main {
    width: 100%;
    /*84 = navbar + tags-view = 50 +34 */
    /* min-height: calc(100vh - 84px); *//* me */
    height: calc(100% - 35px);/* me */
    position: relative;
    overflow: auto;
		background-color: #efefef;
    box-sizing: border-box;
}
</style>
