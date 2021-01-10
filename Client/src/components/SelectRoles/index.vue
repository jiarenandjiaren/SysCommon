<template>
  <div>
    <el-input @focus="selectDialog = true" v-model="names"></el-input>
    <el-dialog :destroy-on-close="true" class="dialog-mini custom-dialog user-dialog" width="850px" title="选择角色"
      :visible.sync="selectDialog">
      <selectUsersCom v-if="selectDialog" :selectUsers:sync="selectRoleList" :show.sync="selectDialog" :loginKey="'loginRole'" :users.sync="selectRoles" :userNames.sync="names"></selectUsersCom>
    </el-dialog>
  </div>
</template>

<script>
  import selectUsersCom from '@/components/SelectUsersCom/selectUserByIds'

  export default {
    name: 'select-users',
    components: {
      selectUsersCom
    },
    props: ['roles', 'userNames'],
    data() { // todo:兼容layui的样式、图标
      return {
        // selectRoles: this.roles,
        // names: this.userNames,
        selectDialog: false,
        selectRoleList: [],
        flag: false
      }
    },
    computed: {
      selectRoles:{
        get(){
          return this.roles
        },
        set(val){
          this.$emit('roles-change', 'roles', val)
        }
      },
      names:{
        get(){
          return this.userNames
        },
        set(val){
          this.$emit('roles-change', 'Texts', val)
        }
      }
    },
    watch: {
      userNames() {
        this.names = this.userNames
        this.groupList()
      },
      selectRoleList(val) {
        this.selectRoles = val && val.length > 0 && val.map(item => item.id) || []
        this.names = val && val.length > 0 && val.map(item => item.name || item.account).join(',') || ''
      }
    },
    mounted() {
      this.groupList()
    },
    methods: {
      groupList() {
        if (!this.userNames) {
          this.selectRoleList = []
          return
        }
        const nameArr = this.userNames && this.userNames.split(',')
        this.selectRoleList = this.selectRoles.map((item, index) => { return { id: item, name: nameArr[index] } })
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
