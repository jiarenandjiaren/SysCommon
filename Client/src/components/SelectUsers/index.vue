<template>
  <div>
    <el-input @focus="selectDialog = true" v-model="names"></el-input>
    <el-dialog :destroy-on-close="true" class="dialog-mini custom-dialog user-dialog" width="850px" title="选择用户"
      :visible.sync="selectDialog">
      <!-- 使用v-if的原因：dialog在关闭的时候会执行组件里面的mounted，所以需要关闭dialog时销毁子组件 -->
      <selectUsersCom ref="selectUser" :hiddenFooter="true" v-if="selectDialog" :selectUsers.sync="selectUserList" :orgId="orgId" :show.sync="selectDialog" :users.sync="selectUsers" :loginKey="'loginUser'" :userNames.sync="names"></selectUsersCom>
      <div slot="footer" style="text-align:right;">
        <el-button size="small" type="cancel" @click="selectDialog = false">取消</el-button>
        <el-button size="small" type="primary" @click="handleSaveUsers">确定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  // import selectUsersCom from '@/components/SelectUsersCom/selectUserByIds'
  import selectUsersCom from '@/components/SelectUsersCom'

  export default {
    name: 'select-users',
    components: {
      selectUsersCom
    },
    props: ['users', 'userNames', 'orgId'],
    data() { // todo:兼容layui的样式、图标
      return {
        // selectUsers: this.users,
        // userNames: '',
        // names: this.userNames,
        defaultSelectUsers: this.users,
        orgUsers: [],
        orgs: [],

        selectDialog: false,
        selectUserList: []
      }
    },
    computed: {
      selectUsers:{
        get(){
          return this.users
        },
        set(val){
          this.$emit('users-change', 'users', val)
        }
      },
      names:{
        get(){
          return this.userNames
        },
        set(val){
          this.$emit('users-change', 'Texts', val)
        }
      }
    },
    watch: {
      // selectUsers() {
      //   this.$emit('users-change', this.selectUsers, this.names)
      // },
      userNames() {
        this.names = this.userNames
        this.groupList()
      },
      selectUserList(val) {
        this.selectUsers = val && val.length > 0 && val.map(item => item.id) || []
        this.names = val && val.length > 0 && val.map(item => item.name || item.account).join(',') || ''
      }
    },
    mounted() {
      this.groupList()
    },
    methods: {
      groupList() {
        if (!this.userNames) {
          this.selectUserList = []
          return
        }
        const nameArr = this.userNames && this.userNames.split(',')
        this.selectUserList = this.selectUsers.map((item, index) => { return { id: item, name: nameArr[index] } })
      },
      handleSaveUsers() {
        this.$refs.selectUser.handleSaveUsers()
      }
    }
  }

</script>

<style lang="scss">
 .el-transfer{
   margin-top:10px;
 }
 .user-dialog{
  &.custom-dialog{
    .el-dialog{
      height: 70%;
      .el-icon-close{
        padding-top: 10px;
      }
      .el-dialog__body{
        height: calc(100% - 35px - 40px);
      }
      .el-dialog__headerbtn {
        top: 0;
      }
    }
  }
 }
 
</style>
