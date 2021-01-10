<template>
  <div style="height: 100%;" class="select-users-wrap">
    <div class="flex-row" style="height: 100%;">
      <div class="part-box" v-if="loginKey === 'loginUser' && !orgId">
        <el-card shadow="never" class="body-small custom-card" style="height: 100%;">
          <div slot="header" class="clearfix">
            <el-button type="text" style="padding: 0 11px" @click="getAllUsers">全部用户>></el-button>
          </div>

          <el-tree :data="orgsTree" :expand-on-click-node="false" default-expand-all
            @node-click="handleNodeClick"></el-tree>
        </el-card>
      </div>
      <div class="flex-item table-box">
        <div class="flex-row" style="align-items: center;" v-if="loginKey === 'loginUser'" @keyup.13="handleSearchUser">
          <el-input
            size="mini"
            style="margin: 10px;width: 200px;"
            placeholder="请输入内容" v-model="searchKey">
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
          <el-button type="primary" icon="el-icon-search" size="mini" @click="handleSearchUser">查询</el-button>
          <div style="text-align: right;padding: 5px 10px;" :title="names" v-if="names" class="flex-item ellipsis">选中用户：{{names}}</div>
        </div>
        <el-table
          ref="multipleTable"
          height="calc(100% - 65px - 10px)"
          v-if="loginKey === 'loginUser'"
          :data="tableData.datas"
          tooltip-effect="dark"
          v-loading="tableData.loading"
          style="width: 100%;border-top: 1px solid #e4e4e4;"
          @row-click="rowClick"
          @select="handleSelectionUser"
          @select-all="handleSelectionUser">
          <el-table-column align="center" type="selection" width="55">
          </el-table-column>

          <el-table-column align="center" min-width="80px" :label="'账号'">
            <template slot-scope="scope">
              <span class="link-type">{{scope.row.account}}</span>
            </template>
          </el-table-column>

          <el-table-column align="center" min-width="80px" :label="'姓名'">
            <template slot-scope="scope">
              <span>{{scope.row.name}}</span>
            </template>
          </el-table-column>

          <el-table-column align="center" :label="'所属部门'">
            <template slot-scope="scope">
              <span>{{scope.row.organizations}}</span>
            </template>
          </el-table-column>

          <el-table-column align="center" class-name="status-col" :label="'状态'" width="100">
            <template slot-scope="scope">
              <span :class="scope.row.status | userStatusFilter">{{statusOptions.find(u =>u.key ==
                scope.row.status).display_name}}</span>
            </template>
          </el-table-column>
        </el-table>
        <el-table
          ref="multipleTable"
          height="calc(100%)"
          v-else
          :data="tableData.datas"
          tooltip-effect="dark"
          v-loading="tableData.loading"
          border
          style="width: 100%;"
          @row-click="rowClick"
          @select="handleSelectionUser"
          @select-all="handleSelectionUser">
          <el-table-column align="center" type="selection" width="55">
          </el-table-column>

          <el-table-column align="center" min-width="50px" :label="'角色名称'">
            <template slot-scope="scope">
              <span>{{scope.row.name}}</span>
            </template>
          </el-table-column>

          <!-- <el-table-column min-width="300px" :label="'用户列表'">
            <template slot-scope="scope">
              <role-users :role-id="scope.row.id"></role-users>
            </template>
          </el-table-column> -->

          <el-table-column align="center" class-name="status-col" :label="'状态'" width="100">
            <template slot-scope="scope">
              <span :class="scope.row.status | userStatusFilter">{{statusOptions.find(u =>u.key ==
                scope.row.status).display_name}}</span>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          :background="false"
          v-if="loginKey === 'loginUser'"
          layout="prev, pager, next"
          :total="tableData.total" :page-size="tableData.listQuery.limit" @current-change="handlePageSearch" style="margin-top: 0;text-align: right;">
        </el-pagination>
      </div>
    </div>
    <div style="text-align:right;margin-top: 10px;" v-if="!hiddenFooter">
      <el-button size="small" type="cancel" @click="handleClose">取消</el-button>
      <el-button size="small" type="primary" @click="handleSaveUsers">确定</el-button>
    </div>
  </div>
</template>
<script>
import {
  listToTreeSelect
} from '@/utils'
import * as login from '@/api/login'
import * as users from '@/api/users'
import * as roles from '@/api/roles'
export default {
  props: ['show', 'users', 'userNames', 'loginKey', 'orgId', 'hiddenFooter', 'selectUsers'],
  data() {
    return {
      orgsTree: [],
      // loginKey: '{loginUser}',
      searchKey: '',
      statusOptions: [{
        key: true,
        display_name: '停用'
      },
      {
        key: false,
        display_name: '正常'
      }],
      tableData: {
        datas: [],
        total: 0,
        selectUsers: [],
        selectUsersC: [],
        selectTexts: [],
        selectIds: [],
        selectTextsC: [],
        selectIdsC: [],
        loading: false,
        listQuery: { // 查询条件
          page: 1,
          limit: 15,
          orgId: '',
          key: undefined
        }
      }
    }
  },
  watch: {
    'tableData.selectUsersC'() {
      this.tableData.selectUsers = this.tableData.selectUsers.filter(user => !this.tableData.selectUsersC.some(x => x.id === user.id))
    }
  },
  computed: {
    names() {
      let names = ''
      if (this.tableData.selectUsers.length > 0 || this.tableData.selectUsersC.length > 0) {
        names = [...this.tableData.selectUsers, ...this.tableData.selectUsersC].map(item => item.name || item.account).join(',')
      }
      return names
    }
  },
  filters: {
    userStatusFilter(status) {
      var res = 'color-success'
      switch (status) {
        case 1:
          res = 'color-danger'
          break
        default:
          break
      }
      return res
    }
  },
  mounted() {
    this.tableData.selectUsers = [...this.selectUsers]
    this.loadData()
  },
  methods: {
    // 加载数据
    loadData(page) {
      this.tableData.listQuery.page = page || 1
      this.groupData()
      if (this.loginKey === 'loginUser') {
        (!this.partDatas || this.partDatas.length <= 0) && this.getPartData()
        this.getUserList()
        return
      }
      this.getRoleList()
    },
    // 通过部门获取用户
    handleNodeClick(data) {
      this.groupData()
      this.tableData.listQuery.orgId = data.id
      this.tableData.listQuery.page = 1
      this.getUserList()
    },
    // 搜索用户/角色
    handleSearchUser() {
      this.loadData()
    },
    // 获取全部用户
    getAllUsers() {
      this.tableData.listQuery.orgId = ''
      this.tableData.listQuery.page = 1
      this.getUserList()
    },
    // 分页查询用户/角色
    handlePageSearch(val) {
      this.loadData(val)
    },
    groupData() {
      this.tableData.selectUsers = [...this.tableData.selectUsers, ...this.tableData.selectUsersC]
    },
    // 获取用户
    getUserList() {
      this.tableData.loading = true
      this.tableData.listQuery.key = this.searchKey
      users.getList(this.tableData.listQuery).then(response => {
        this.tableData.datas = response.data
        this.tableData.total = response.count
        this.tableData.loading = false
        this.tableData.selectUsersC = [...this.tableData.datas].filter(x => this.tableData.selectUsers.some(item => item.id === x.id))
        this.setSelectTable()
      })
    },
    handleClose() {
      this.$emit('update:show', false)
    },
    // 默认选中
    setSelectTable() {
      this.$nextTick(() => {
        const rows = [...this.tableData.selectUsersC]
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row)
        })
      })
    },
    // 获取部门信息
    getPartData() {
      login.getOrgs().then(response => {
        this.partDatas = response.result.map(function(item) {
          return {
            id: item.id,
            label: item.name,
            parentId: item.parentId || null
          }
        })
        var orgstmp = JSON.parse(JSON.stringify(this.partDatas))
        this.orgsTree = listToTreeSelect(orgstmp)
      })
    },
    // 确认用户选择
    handleSaveUsers() {
      this.groupData()
      this.$emit('update:selectUsers', this.tableData.selectUsers)
      this.$emit('update:show', false)
    },
    // 选择用户
    handleSelectionUser(val) {
      this.tableData.selectUsersC = val
    },
    // 获取角色
    getRoleList() {
      this.tableData.loading = true
      this.tableData.listQuery.key = this.searchKey
      roles.getList(this.listQuery).then(response => {
        this.tableData.datas = response.result
        this.tableData.loading = false
        this.tableData.selectTextsC = [...this.tableData.datas].filter(x => this.tableData.selectTexts.indexOf(x.name || x.account) > -1).map(item => item.name || item.account)
        this.tableData.selectIdsC = [...this.tableData.datas].filter(x => this.tableData.selectIds.indexOf(x.id) > -1).map(item => item.id)
        this.tableData.selectTexts = [...this.tableData.selectTexts].filter(x => !this.tableData.selectTextsC.some(y => x === y))
        this.tableData.selectIds = [...this.tableData.selectIds].filter(x => !this.tableData.selectIdsC.some(y => x === y))
        this.setSelectTable()
      })
    },
    rowClick(row) {
      this.$refs.multipleTable.clearSelection()
      this.$refs.multipleTable.toggleRowSelection(row)
      this.tableData.selectUsersC = [row]
    }
  }
}
</script>
<style lang="scss">
  .select-users-wrap{
    .ellipsis{
      width: 100%;
      overflow:hidden;
      white-space:nowrap;
      text-overflow: ellipsis;
      display:inline-block;
    }
    .ruleSpan{
      cursor: pointer;
      color: #409eff;
    }
    .custom-card{
      height: 100%;
      .el-card__body{
        height:calc(100% - 34px);overflow: auto;
      }
    }
    .flex-row{
      width: 100%;
      display: flex;
      flex-direction: row;
      box-sizing: border-box;
    }
    .flex-column{
      display: flex;
      flex-direction: column;
      box-sizing: border-box;
    }
    .flex-item{
      overflow: hidden;
    }
    .VMB{
      &::before{
        content: "";
        display: inline-block;
        height: 100%;
        vertical-align: middle;
      }
      .VM{
        display: inline-block;
        vertical-align: middle;
      }
    }
  }
</style>

