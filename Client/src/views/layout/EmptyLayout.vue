<template>
 <!-- <div class="app-wrapper" :class="classObj">
    <div v-if="device==='mobile'&&sidebar.opened" class="drawer-bg" @click="handleClickOutside"></div>
    <sidebar class="sidebar-container"></sidebar>
    <div class="main-container">
      <navbar></navbar>
      <tags-view></tags-view>
      <app-main></app-main>
    </div>
  </div> -->
	<div class="app-wrapper" :class="classObj">
		<el-container class="flex-column">
			<el-container class="flex-row flex-item">
				<!-- <sidebar class="sidebar-container"></sidebar>
				<div class="main-container flex-item">
					<tags-view class="custom-tags-view"></tags-view> -->
					<app-main></app-main>
				<!-- </div> -->
			</el-container>
		</el-container>
	</div>
</template>

<script>
import AppMain from './components/AppMain'
// import ResizeMixin from './mixin/ResizeHandler'

export default {
  name: 'layout',
  components: {
    // Navbar,
    // Sidebar,
    AppMain
    // TagsView
  },
  // mixins: [ResizeMixin],
  computed: {
    sidebar() {
      return this.$store.state.app.sidebar
    },
    // device() {
    //   return this.$store.state.app.device
    // },
    classObj() {
      return {
        hideSidebar: !this.sidebar.opened,
        openSidebar: this.sidebar.opened,
        withoutAnimation: this.sidebar.withoutAnimation,
        mobile: this.device === 'mobile'
      }
    }
  },
  mounted() {
    console.log('route', this.$router)
  },
  methods: {
    // handleClickOutside() {
    //   this.$store.dispatch('CloseSideBar', { withoutAnimation: false })
    // }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
@import 'src/styles/mixin.scss';
/deep/ .app-main{
  height: 100%;
}
.app-wrapper {
    @include clearfix;
    position: relative;
    height: 100%;
    width: 100%;
    &.mobile.openSidebar {
        position: fixed;
        top: 0;
    }
}
.drawer-bg {
    background: #000;
    opacity: 0.3;
    width: 100%;
    top: 0;
    height: 100%;
    position: absolute;
    z-index: 999;
}
.el-header {
    padding: 0;
    line-height: 44px;
    background-color: #333;
    // position: fixed;// me
    // top: 0;// me
    width: 100%;
    z-index: 100;
}
.el-container .sidebar-container {
    // top: 44px !important;
    height: auto !important;

    // position: relative !important;// me
}


.el-container .main-container{
  margin-left: 0 !important;
  overflow: hidden;
}
.custom-tags-view{
  margin-top: 0 !important;
}
</style>
