<template>
  <el-scrollbar wrapClass="scrollbar-wrapper">
    <el-menu
      mode="vertical"
      :show-timeout="200"
      :default-active="$route.path"
      :collapse="isCollapse"
      background-color="#304156"
      text-color="#bfcbd9"
      active-text-color="#409EFF"
    >
      <sidebar-item v-for="route in routes" :key="route.name" :item="route" :base-path="route.path"></sidebar-item>
    </el-menu>
  </el-scrollbar>
</template>

<script>
import { mapGetters } from 'vuex'
import SidebarItem from './SidebarItem'

export default {
  components: { SidebarItem },
  data() {
    return {
      routes: []
    }
  },
  computed: {
    ...mapGetters(['sidebar', 'permission_routers']),
    // routes() {
    //   return this.permission_routers.sort((a, b) => a.meta.sortNo - b.meta.sortNo)
    // },
    isCollapse() {
      return !this.sidebar.opened
    }
  },
  watch: {
    item() {
      this.routes = this.permission_routers.length > 0 && this.permission_routers.sort((a, b) => a.meta.sortNo - b.meta.sortNo)
    }
  },
  created() {
    this.routes = this.permission_routers.length > 0 && this.permission_routers.sort((a, b) => a.meta.sortNo - b.meta.sortNo)
  },
}
</script>
