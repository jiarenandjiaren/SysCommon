<template>
  <div v-if="!item.hidden&&item.children" class="menu-wrapper">

      <router-link v-if="hasOneShowingChildren(item.children) && !item.children[0].children&&!item.alwaysShow" :to="resolvePath(item.children[0].path)">
        <el-menu-item :index="resolvePath(item.children[0].path)" :class="{'submenu-title-noDropdown':!isNest}">
          <i :class="`iconfont icon-${item.children[0].meta.icon}`"></i>
          <span v-if="item.children[0].meta&&item.children[0].meta.title" slot="title">{{item.children[0].meta.title}}</span>
        </el-menu-item>
      </router-link>

      <el-submenu v-else :index="item.name||item.path">
        <template slot="title">
          <i :class="`iconfont icon-${item.meta.icon}`"></i>
          <span v-if="item.meta&&item.meta.title" slot="title">{{item.meta.title}}</span>
        </template>

        <template v-for="child in routes">
          <template v-if="!child.hidden">
            <sidebar-item :is-nest="true" class="nest-menu" v-if="child.children&&child.children.length>0" :item="child" :key="child.path" :base-path="resolvePath(child.path)"></sidebar-item>

            <router-link v-else :to="resolvePath(child.path)" :key="child.name">
              <el-menu-item :index="resolvePath(child.path)">
                <i :class="`iconfont icon-${child.meta.icon}`"></i>
                <span v-if="child.meta&&child.meta.title" slot="title">{{child.meta.title}}</span>
              </el-menu-item>
            </router-link>
          </template>
        </template>
      </el-submenu>

  </div>
</template>

<script>
import path from 'path'

export default {
  name: 'SidebarItem',
  props: {
    // route配置json
    item: {
      type: Object,
      required: true
    },
    isNest: {
      type: Boolean,
      default: false
    },
    basePath: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      routes: []
    }
  },
  watch: {
    item() {
      this.routes = this.item.children && this.item.children.length > 0 && this.item.children.sort((a, b) => a.meta.sortNo - b.meta.sortNo)
    }
  },
  created() {
    this.routes = this.item.children && this.item.children.length > 0 && this.item.children.sort((a, b) => a.meta.sortNo - b.meta.sortNo)
  },
  // computed: {
  //   routes() {
  //     return this.item.children.sort((a, b) => a.meta.sortNo - b.meta.sortNo)
  //   }
  // },
  methods: {
    hasOneShowingChildren(children) {
      // 启用如果只有一个子菜单自动提升为顶级独立菜单
      const showingChildren = children.filter(item => {
        return !item.hidden
      })
      if (showingChildren.length === 1) {
        if (showingChildren[0].name === 'dashboard') { // 如果是首页，则直接调整为顶级独立菜单
          return true
        }
        // if (showingChildren[0].name === 'swagger') { // 如果是swagger，则直接调整为顶级独立菜单
        //   return true
        // }
      }
      return false
    },
    resolvePath(...paths) {
      return path.resolve(this.basePath, ...paths)
    }
  }
}
</script>
<style lang="scss">
  .menu-wrapper .iconfont{
    margin-right: 5px;
    font-size: 16px;
    vertical-align: middle;
  }
</style>


