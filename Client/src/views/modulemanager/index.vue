<template>
  <div class="flex-column">
    <sticky :className="'sub-navbar'">
      <div class="filter-container">
        <el-input @keyup.enter.native="handleFilter" size="mini" prefix-icon="el-icon-search" style="width: 200px;margin-bottom: 0;" class="filter-item" :placeholder="'关键字'"
          v-model="listQuery.key">
        </el-input>

        <!-- <el-button class="filter-item" type="success" v-waves icon="el-icon-search" @click="handleFilter">搜索</el-button> -->
        <permission-btn moduleName='modulemanager' :size="'mini'" v-on:btn-event="onBtnClicked"></permission-btn>

        <el-checkbox size="mini" style='margin-left:15px;' @change='tableKey=tableKey+1' v-model="showDescription">Id/描述</el-checkbox>
      </div>
    </sticky>
    <div class="app-container flex-item">
      <el-row class="fh">
        <el-col :span="10" class="fh ls-border">
          <el-card shadow="never" class="card-body-none fh" style="overflow-y: auto">
            <div slot="header" class="clearfix">
              <el-button type="text" style="padding: 0 11px" @click="getAllMenus">所有菜单>></el-button>
            </div>
						<tree-table highlight-current-row :data="modulesTree" :columns="columns" @row-click="treeClick" border></tree-table>
          </el-card>
        </el-col>
        <el-col :span="14" class="fh">
			<div class="bg-white fh">
				<el-table ref="mainTable" :key='tableKey' :data="currentPageMenus" v-loading="listLoading" border fit
					highlight-current-row style="width: 100%;" height="calc(100% - 52px)" @row-click="rowClick" @selection-change="handleSelectionChange">
					<el-table-column type="selection" align="center" width="55">
					</el-table-column>

					<el-table-column :label="'Id'" v-if="showDescription" min-width="120px">
						<template slot-scope="scope">
							<span>{{scope.row.id}}</span>
						</template>
					</el-table-column>

					<el-table-column min-width="80px" :label="'DOM ID'">
						<template slot-scope="scope">
							<span class="link-type" @click="handleEditMenu(scope.row)">{{scope.row.domId}}</span>
						</template>
					</el-table-column>

					<el-table-column min-width="50px" :label="'名称'">
						<template slot-scope="scope">
							<span>{{scope.row.name}}</span>
						</template>
					</el-table-column>

          <el-table-column min-width="30px" :label="'排序'">
						<template slot-scope="scope">
							<span>{{scope.row.sort}}</span>
						</template>
					</el-table-column>

					<el-table-column min-width="80px" :label="'样式'">
						<template slot-scope="scope">
							<span>{{scope.row.class}}</span>
						</template>
					</el-table-column>
					<el-table-column min-width="80px" :label="'ICON'">
						<template slot-scope="scope">
							<span>{{scope.row.icon}}</span>
						</template>
					</el-table-column>

					<el-table-column :label="'描述'" v-if="showDescription" min-width="120px">
						<template slot-scope="scope">
							<span>{{scope.row.remark}}</span>
						</template>
					</el-table-column>
				</el-table>
					<!-- <el-pagination background @size-change="handleSizeChange" @current-change="handleCurrentChange"
						:current-page="listQuery.page" :page-sizes="[10,20,30, 50]" :page-size="listQuery.limit" layout="total, sizes, prev, pager, next, jumper"
						:total="total">
					</el-pagination> -->
				<pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="handleCurrentChange" />
			</div>
        </el-col>
      </el-row>


      <!--模块编辑对话框-->
      <el-dialog v-el-drag-dialog  class="dialog-mini" width="500px" :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
        <el-form :rules="rules" ref="dataForm" :model="temp" label-position="right" label-width="100px">
          <el-form-item size="small" :label="'Id'" prop="id" v-show="dialogStatus=='update'">
            <span>{{temp.id}}</span>
          </el-form-item>
          <el-form-item size="small" :label="'层级ID'" v-show="dialogStatus=='update'">
            <span>{{temp.cascadeId}}</span>
          </el-form-item>
          <el-form-item size="small" :label="'名称'" prop="name">
            <el-input v-model="temp.name"></el-input>
          </el-form-item>
           <el-form-item size="small" :label="'排序'">
             <el-input-number v-model="temp.sortNo" :min="1" :max="20" ></el-input-number>
          </el-form-item>
           <el-form-item size="small" :label="'是否系统'" prop="isSys">
             <el-switch
              v-model="temp.isSys"
              active-color="#13ce66"
              inactive-color="#ff4949">
            </el-switch>
          </el-form-item>
          <el-form-item size="small" :label="'模块标识'">
            <el-input v-model="temp.code"></el-input>
          </el-form-item>
          <el-form-item size="small" :label="'图标'">
            <el-popover
              placement="top-start"
              width="400"
              trigger="click"
              content="这是一段内容,这是一段内容,这是一段内容,这是一段内容。">
              <el-input slot="reference" v-model="temp.iconName"></el-input>
              <el-row class="selectIcon-box">
                <el-col :class="{'active': temp.iconName === item.font_class}" :span="3" v-for="(item,index) in iconData.glyphs" :key="index">
                  <i :class="`${iconData.font_family} ${iconData.css_prefix_text}${item.font_class}`" @click="handleChangeTempIcon(item)"></i>
                </el-col>
              </el-row>
            </el-popover>
          </el-form-item>
          <el-form-item size="small" :label="'url'">
            <el-input v-model="temp.url"></el-input>
          </el-form-item>

          <el-form-item size="mini" :label="'上级机构'">

            <treeselect ref="modulesTree" :normalizer="normalizer" :disabled="treeDisabled" :options="modulesTreeRoot"
              :default-expand-level="3" :multiple="false" :open-on-click="true" :open-on-focus="true" :clear-on-select="true"
              v-model="dpSelectModule"></treeselect>
            <!-- <el-checkbox v-model="isRoot">根节点</el-checkbox> -->
          </el-form-item>
        </el-form>
        <div slot="footer">
          <el-button size="mini" @click="dialogFormVisible = false">取消</el-button>
          <el-button size="mini" v-if="dialogStatus=='create'" type="primary" @click="createData">确认</el-button>
          <el-button size="mini" v-else type="primary" @click="updateData">确认</el-button>
        </div>
      </el-dialog>
      <!--菜单编辑对话框-->
      <el-dialog v-el-drag-dialog class="dialog-mini" width="500px" :title="textMap[dialogMenuStatus]" :visible.sync="dialogMenuVisible">
        <el-form :rules="rules" ref="menuForm" :model="menuTemp" label-position="right" label-width="100px">
          <el-form-item size="small" :label="'Id'" prop="id" v-show="dialogMenuStatus=='update'">
            <span>{{menuTemp.id}}</span>
          </el-form-item>

          <el-form-item size="small" :label="'名称'" prop="name">
            <el-input v-model="menuTemp.name"></el-input>
          </el-form-item>
          <el-form-item size="small" :label="'DOM ID'">
            <el-input v-model="menuTemp.domId"></el-input>
          </el-form-item>
          <el-form-item size="small" :label="'样式'">
            <el-input v-model="menuTemp.class"></el-input>
          </el-form-item>
          <el-form-item size="small" :label="'图标'">
            <!-- <el-input v-model="menuTemp.icon"></el-input> -->
            <el-popover
              placement="top-start"
              width="400"
              trigger="click"
              content="这是一段内容,这是一段内容,这是一段内容,这是一段内容。">
              <el-input slot="reference" v-model="menuTemp.icon"></el-input>
              <el-row class="selectIcon-box">
                <el-col :class="{'active': menuTemp.icon === item.font_class}" :span="3" v-for="(item,index) in iconData.glyphs" :key="index">
                  <i :class="`${iconData.font_family} ${iconData.css_prefix_text}${item.font_class}`" @click="menuTemp.icon = item.font_class"></i>
                </el-col>
              </el-row>
            </el-popover>
          </el-form-item>
           <el-form-item size="small" :label="'排序'">
             <el-input-number v-model="menuTemp.sort" :min="0" :max="10" ></el-input-number>
          </el-form-item>
          <el-form-item size="small" :label="'备注'">
            <el-input v-model="menuTemp.remark"></el-input>
          </el-form-item>
          <el-form-item size="small" :label="'所属模块'">

            <treeselect ref="menuModulesTree" :normalizer="normalizer" :options="modulesTree" :default-expand-level="3"
              :multiple="false" :open-on-click="true" :open-on-focus="true" :clear-on-select="true" v-model="menuTemp.moduleId"></treeselect>
          </el-form-item>
        </el-form>
        <div slot="footer">
          <el-button size="mini" @click="dialogMenuVisible = false">取消</el-button>
          <el-button size="mini" v-if="dialogMenuStatus=='create'" type="primary" @click="addMenu">确认</el-button>
          <el-button  size="mini" v-else type="primary" @click="updateMenu">确认</el-button>
        </div>
      </el-dialog>
    </div>
  </div>

</template>

<script>
import treeTable from '@/components/TreeTable'
import Pagination from '@/components/Pagination'
import { listToTreeSelect } from '@/utils'
import * as modules from '@/api/modules'
import * as login from '@/api/login'
import Treeselect from '@riophae/vue-treeselect'
import '@riophae/vue-treeselect/dist/vue-treeselect.css'
import waves from '@/directive/waves' // 水波纹指令
import Sticky from '@/components/Sticky'
import permissionBtn from '@/components/PermissionBtn'
import elDragDialog from '@/directive/el-dragDialog'
import iconData from '@/assets/public/css/comIconfont/iconfont/iconfont.json'
export default {
  name: 'modulemanager',
  components: {
    Sticky,
    permissionBtn,
    Treeselect,
    treeTable,
    Pagination
  },
  directives: {
    waves,
    elDragDialog
  },
  data() {
    return {
      iconData: iconData,
      normalizer(node) {
        // treeselect定义字段
        return {
          label: node.name,
          id: node.id,
          children: node.children
        }
      },
      columns: [
        // treetable的列名
        {
          text: '模块名称',
          value: 'name'
        },
        {
          text: '模块标识',
          value: 'code'
        },
        {
          text: 'URL',
          value: 'url'
        }
      ],
      selectMenus: [], // 菜单列表选中的值
      tableKey: 0,
      list: [], // 菜单列表
      total: 0,
      currentModule: null, // 左边模块treetable当前选中的项
      listLoading: true,
      listQuery: {
        // 查询条件
        page: 1,
        limit: 20,
        orgId: '',
        key: undefined
      },
      apps: [],

      showDescription: false,
      modules: [], // 用户可访问到的模块列表
      modulesTree: [], // 用户可访问到的所有模块组成的树
      temp: {
        // 模块临时值
        id: undefined,
        cascadeId: '',
        url: '',
        code: '',
        sortNo: 0,
        iconName: '',
        parentId: null,
        name: '',
        status: 0,
        isSys: false
      },
      menuTemp: {
        // 菜单临时值
        id: undefined,
        url: '',
        code: '',
        moduleId: null,
        name: '',
        status: 0,
        sort: 0
      },
      dialogFormVisible: false, // 模块编辑框
      dialogStatus: '',
      dialogMenuVisible: false, // 菜单编辑框
      dialogMenuStatus: '',

      chkRoot: false, // 根节点是否选中
      treeDisabled: false, // 树选择框时候可用
      textMap: {
        update: '编辑',
        create: '添加'
      },
      rules: {
        name: [
          {
            required: true,
            message: '名称不能为空',
            trigger: 'blur'
          }
        ]
      },
      downloadLoading: false
    }
  },
  computed: {
    isRoot: {
      get() {
        return this.chkRoot
      },
      set(v) {
        this.chkRoot = v
        if (v) {
          this.temp.parentName = '根节点'
          this.temp.parentId = null
        }
        // this.treeDisabled = v
      }
    },
    modulesTreeRoot() {
      const root = [{
        name: '根节点',
        parentId: '',
        id: ''
      }]
      return root.concat(this.modulesTree)
    },
    currentPageMenus: {
      get() {
        var start = (this.listQuery.page - 1) * this.listQuery.limit
        var end = (this.listQuery.page * this.listQuery.limit)
        var result = this.list.slice(start, end)
        console.log(result)
        return result
      }
    },
    dpSelectModule: {
      // 模块编辑框下拉选中的模块
      get: function() {
        if (this.dialogStatus === 'update') {
          return this.temp.parentId || ''
        } else {
          return ''
        }
      },
      set: function(v) {
        console.log('set org:' + v)
        if (v === undefined || v === null || !v) {
          // 如果是根节点
          this.temp.parentName = '根节点'
          this.temp.parentId = null
          // this.isRoot = true
          return
        }
        // this.isRoot = false
        this.temp.parentId = v
        var parentname = this.modules.find(val => {
          return v === val.id
        }).name
        this.temp.parentName = parentname
      }
    }
  },
  filters: {
    statusFilter(status) {
      const statusMap = {
        0: 'info',
        1: 'danger'
      }
      return statusMap[status]
    }
  },
  created() {
    this.getList()
    console.log('iconData>>>>>>>', this.iconData)
  },
  mounted() {
    this.getModulesTree()
  },
  methods: {
    handleChangeTempIcon(item){
      this.temp.iconName = item.font_class
      console.log(item, this.temp,this.temp.iconName === item.font_class,this.temp.iconName,  item.font_class)
    },
    rowClick(row) {
      this.$refs.mainTable.clearSelection()
      this.$refs.mainTable.toggleRowSelection(row)
    },
    treeClick(data) {
      // 左侧treetable点击事件
      this.currentModule = data
      this.currentModule.parent = null
      this.getList()
    },
    getAllMenus() {
      this.currentModule = null
      this.getList()
    },
    handleSelectionChange(val) {
      this.selectMenus = val
    },
    onBtnClicked: function(domId) {
      console.log('you click:' + domId)
      switch (domId) {
        case 'btnAdd':
          this.handleCreate()
          break
        case 'btnEdit':
          if (this.currentModule === null) {
            this.$message({
              message: '只能选中一个进行编辑',
              type: 'error'
            })
            return
          }
          this.handleUpdate(this.currentModule)
          break
        case 'btnDel':
          if (this.currentModule === null) {
            this.$message({
              message: '至少删除一个',
              type: 'error'
            })
            return
          }
          this.handleDelete(this.currentModule)
          break
        case 'btnAddMenu':
          this.handleAddMenu()
          break
        case 'btnEditMenu':
          if (this.selectMenus.length !== 1) {
            this.$message({
              message: '只能选中一个进行编辑',
              type: 'error'
            })
            return
          }
          this.handleEditMenu(this.selectMenus[0])
          break
        case 'btnDelMenu':
          if (this.selectMenus.length < 1) {
            this.$message({
              message: '至少删除一个',
              type: 'error'
            })
            return
          }
          this.handleDelMenus(this.selectMenus)
          break
        default:
          break
      }
    },
    getList() {
      this.listLoading = true
      var moduleId = this.currentModule === null ? null : this.currentModule.id
      modules.loadMenus(moduleId).then(response => {
        this.list = response.result
        // this.total = response.count
        this.total = response.result.length
        this.listLoading = false
      })
    },
    getModulesTree() {
      var _this = this // 记录vuecomponent
      login.getModules().then(response => {
        _this.modules = response.result.map(function(item) {
          return {
            sortNo: item.sortNo,
            id: item.id,
            name: item.name,
            iconName: item.iconName,
            parentId: item.parentId || null,
            code: item.code,
            url: item.url,
            cascadeId: item.cascadeId,
            isSys: item.isSys
          }
        })
        var modulestmp = JSON.parse(JSON.stringify(_this.modules))
        _this.modulesTree = listToTreeSelect(modulestmp)
        console.log(_this.modulesTree)
      })
    },
    handleFilter() {
      this.listQuery.page = 1
      this.getList()
    },
    handleSizeChange(val) {
      this.listQuery.limit = val
      this.getList()
    },
    handleCurrentChange(val) {
      console.log(val)
      this.listQuery.page = val.page
      this.listQuery.limit = val.limit
      this.getList()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        cascadeId: '',
        url: '',
        iconName: '',
        code: '',
        parentId: null,
        name: '',
        status: 0
      }
    },
    resetMenuTemp() {
      this.menuTemp = {
        id: undefined,
        cascadeId: '',
        url: '',
        code: '',
        moduleId: this.currentModule ? this.currentModule.id : null,
        name: '',
        status: 0,
        sort: 0
      }
    },

    // #region 模块管理
    handleCreate() {
      // 弹出添加框
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.dpSelectModule = null
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      // 保存提交
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          modules.add(this.temp).then(response => {
            // 需要回填数据库生成的数据
            this.temp.id = response.result.id
            this.temp.cascadeId = response.result.cascadeId
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '创建成功',
              type: 'success',
              duration: 2000
            })
            this.getModulesTree()
          })
        }
      })
    },
    handleUpdate(row) {
      // 弹出编辑框
      console.log('row', row)
      this.temp = Object.assign({}, row) // copy obj
      if (this.temp.children) { // 点击含有子节点树结构时，带有的children会造成提交的时候json死循环
        this.temp.children = null
      }

      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.dpSelectModule = this.temp.parentId
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      // 更新提交
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          modules.update(tempData).then(() => {
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '更新成功',
              type: 'success',
              duration: 2000
            })

            this.getModulesTree()
            for (const v of this.list) {
              if (v.id === this.temp.id) {
                const index = this.list.indexOf(v)
                this.list.splice(index, 1, this.temp)
                break
              }
            }
          })
        }
      })
    },
    handleDelete(rows) {
      // 多行删除
      modules.del([rows.id]).then(() => {
        this.$notify({
          title: '成功',
          message: '删除成功',
          type: 'success',
          duration: 2000
        })
        this.getModulesTree()
      })
    },
    // #end region

    // #region 菜单管理
    handleAddMenu() {
      // 弹出添加框
      this.resetMenuTemp()
      this.dialogMenuStatus = 'create'
      this.dialogMenuVisible = true
      this.$nextTick(() => {
        this.$refs['menuForm'].clearValidate()
      })
    },
    addMenu() {
      // 保存提交
      this.$refs['menuForm'].validate(valid => {
        if (valid) {
          modules.addMenu(this.menuTemp).then(response => {
            // 需要回填数据库生成的数据
            this.menuTemp.id = response.result.id
            this.list.unshift(this.menuTemp)
            this.dialogMenuVisible = false
            this.$notify({
              title: '成功',
              message: '创建成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleEditMenu(row) {
      // 弹出编辑框
      this.menuTemp = Object.assign({}, row) // copy obj
      this.dialogMenuStatus = 'update'
      this.dialogMenuVisible = true
      this.$nextTick(() => {
        this.$refs['menuForm'].clearValidate()
      })
    },
    updateMenu() {
      // 更新提交
      this.$refs['menuForm'].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.menuTemp)
          modules.updateMenu(tempData).then(() => {
            this.dialogMenuVisible = false
            this.$notify({
              title: '成功',
              message: '更新成功',
              type: 'success',
              duration: 2000
            })

            for (const v of this.list) {
              if (v.id === this.menuTemp.id) {
                const index = this.list.indexOf(v)
                this.list.splice(index, 1, this.menuTemp)
                break
              }
            }
          })
        }
      })
    },
    handleDelMenus(rows) {
      // 多行删除
      modules.delMenu(rows.map(u => u.id)).then(() => {
        this.$notify({
          title: '成功',
          message: '删除成功',
          type: 'success',
          duration: 2000
        })
        rows.forEach(row => {
          const index = this.list.indexOf(row)
          this.list.splice(index, 1)
        })
      })
    }
    // #end region
  }
}
</script>

<style lang="scss">
.text {
    font-size: 14px;
}

.item {
    margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
    display: table;
    content: '';
}

.clearfix:after {
    clear: both;
}

.el-card__header {
    padding: 12px 20px;
}
.selectIcon-box{
  text-align: center;
  border: 1px solid #eeeeee;
  border-right: 0;
  border-bottom: 0;
  .el-col{
    padding: 10px 0;
    border-right: 1px solid #eeeeee;
    border-bottom: 1px solid #eeeeee;
    &.active{
      .iconfont{
        color: #409EFF;
      }
    }
  }
  .iconfont{
    cursor: pointer;
    font-size: 20px;
  }
}
</style>
