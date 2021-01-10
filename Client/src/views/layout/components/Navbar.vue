<template>
  <el-menu class="navbar" mode="horizontal">
		<div class="logo">
			<img class="user-avatar" :src="logo">OpenAuth.Pro
		</div>
    <hamburger class="hamburger-container" :toggleClick="toggleSideBar" :isActive="sidebar.opened"></hamburger>
    <!-- <breadcrumb></breadcrumb> -->
    <el-dropdown class="avatar-container" trigger="click">
      <div class="avatar-wrapper">
        <!-- <img class="user-avatar" :src="logo"> -->
				欢迎您，{{name}}
        <i class="el-icon-caret-bottom"></i>
      </div>
      <el-dropdown-menu class="user-dropdown" slot="dropdown">
        <!-- <router-link class="inlineBlock" to="/">
          <el-dropdown-item>
            首页
          </el-dropdown-item>
        </router-link> -->
        <el-dropdown-item>
					<a href="/#/profile">个人中心</a>
				</el-dropdown-item>
        <el-dropdown-item divided>
          <span @click="logout" style="display:block;">退出</span>
        </el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </el-menu>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
// import Breadcrumb from '@/components/Breadcrumb'
import Hamburger from '@/components/Hamburger'
import logo from '@/assets/logo.png?imageView2/1/w/80/h/80'

export default {
  data: function() {
    return {
      logo: logo
    }
  },
  components: {
    // Breadcrumb,
    Hamburger
  },
  computed: {
    ...mapGetters(['sidebar', 'isIdentityAuth', 'name'])
  },
  methods: {
    ...mapActions([
      'signOutOidc'
    ]),
    toggleSideBar() {
      this.$store.dispatch('ToggleSideBar')
    },
    logout() {
      if (this.isIdentityAuth) {
        this.signOutOidc()
      } else {
        this.$store.dispatch('LogOut').then(() => {
          location.reload() // 为了重新实例化vue-router对象 避免bug
        })
      }
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.navbar {
    border-bottom: 0 !important;
    background-color: #333;
    .hamburger-container {
        line-height: 44px;
        height: 44px;
        float: left;
        padding: 0 10px;
        color: white;
    }
    .screenfull {
        position: absolute;
        right: 90px;
        top: 16px;
        color: red;
    }
    .avatar-container {
        height: 44px;
        display: inline-block;
        position: absolute;
        line-height: 44px;
        right: 35px;
        color: white;
        .avatar-wrapper {
            cursor: pointer;
            position: relative;
            .el-icon-caret-bottom {
                position: absolute;
                right: -22px;
                top: 12px;
                font-size: 16px;
            }
        }
    }
    .logo {
        width: 180px;
        text-align: center;
        float: left;
        color: white;
        .user-avatar {
            width: 30px;
            height: 30px;
            vertical-align: middle;
            margin-right: 5px;
        }
    }
}
</style>
