<template>
  <div class="flex-column">
    <sticky :className="'sub-navbar'">
      <div class="filter-container">
        <el-input @keyup.enter.native="handleFilter" size="mini" style="width: 200px;" class="filter-item"
          :placeholder="'名称'" v-model="firstQuery.key">
        </el-input>
        <el-button class="filter-item" size="mini" v-waves icon="el-icon-search" @click="handleFilter">搜索</el-button>
        <permission-btn moduleName='wmsInboundOrderTbls' size="mini" v-on:btn-event="onBtnClicked"></permission-btn>
      </div>
    </sticky>
    <div class="app-container flex-item flex-column">
      <div class="flex-item">
        <el-card shadow="nerver" class="demo-card fh">
          <el-table ref="multipleTable" id="firstCard" :key='tableKey' :data="mainList" v-loading="listLoading" border
            tooltip-effect="dark" style="width: 100%;" height="calc(100% - 52px)" @row-click="rowClickFirstTable">
            <el-table-column width="55" align="center">
              <template slot-scope="scope">
                <el-radio :label="scope.row.id" v-model="radio">{{''}}</el-radio>
              </template>
            </el-table-column>
            <!--使用服务器返回的表头，如果复杂的表头和格式解析，请自己定义表格格式-->
            <template v-for="(headerItem,index) in firstHeaderList">
              <!-- 如果需要单独处理的格式化显示，可以在这里单独处理 -->
              <el-table-column v-if="headerItem.key=='orderType'" :key="index"  :label="headerItem.description">
                <template slot-scope="scope">
                  <select-type :isEdit="false" typeId="SYS_INBOUNDTYPE" v-model="scope.row[headerItem.key]"></select-type>
                </template>
              </el-table-column>
              <el-table-column v-else-if="headerItem.key=='goodsType'" :key="index"  :label="headerItem.description">
                <template slot-scope="scope">
                  <select-type :isEdit="false" typeId="SYS_GOODSTYPE" v-model="scope.row[headerItem.key]"></select-type>
                </template>
              </el-table-column>
              <el-table-column v-else-if="headerItem.key=='transferType'" :key="index"  :label="headerItem.description">
                <template slot-scope="scope">
                  <select-type :isEdit="false" typeId="SYS_SHIPTYPE" v-model="scope.row[headerItem.key]"></select-type>
                </template>
              </el-table-column>
              <el-table-column v-else-if="headerItem.type=='String' ||headerItem.type=='DateTime'" :key="index"
                :prop="headerItem.key" :label="headerItem.description" show-overflow-tooltip>
              </el-table-column>
              <el-table-column v-else :label="headerItem.description" min-width="100px" :key="index">
                <template slot-scope="scope">
                  <span>{{scope.row[headerItem.key]|filterInt}}</span>
                </template>
              </el-table-column>
            </template>
          </el-table>
          <pagination v-show="firstTotal>0" :total="firstTotal" :page.sync="firstQuery.page"
            :limit.sync="firstQuery.limit" @pagination="handleCurrentChange" />
        </el-card>
      </div>
      <el-row class="flex-item">
        <el-col :span="showTitleDialog ? 12 : 0" class="fh form-card">
          <el-card shadow="nerver" class="demo-card fh">
            <div slot="header">
              <span v-if="radio == ''">头信息</span>
              <span v-else>{{radio}}头信息</span>
            </div>
            <el-form :rules="mainRules" ref="dataForm" :model="firstTemp" label-position="right" label-width="100px" style="height:calc(100% - 34px);overflow: auto;">
              <el-row>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'入库通知单号'" prop="id">
                    <el-input v-model="firstTemp.id" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'相关单据号'" prop="externalNo">
                    <el-input v-model="firstTemp.externalNo" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'单据类型'" prop="externalType">
                    <el-input v-model="firstTemp.externalType" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'状态'">
                    <el-select class="filter-item" :disabled="!editModel" v-model="firstTemp.status" placeholder="Please select">
                      <el-option v-for="item in  statusOptions" :key="item.key" :label="item.display_name"
                        :value="item.key">
                      </el-option>
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'入库类型'" prop="orderType">
                    <select-type :isEdit="true" :disabled="!editModel" typeId="SYS_INBOUNDTYPE" v-model="firstTemp.orderType"></select-type>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'商品类别'" prop="goodsType">
                    <select-type :isEdit="true" :disabled="!editModel" typeId="SYS_GOODSTYPE" v-model="firstTemp.goodsType"></select-type>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'采购单号'" prop="purchaseNo">
                    <el-input v-model="firstTemp.purchaseNo" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'仓库编号'" prop="stockId">
                    <el-input v-model="firstTemp.stockId" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'货主编号'" prop="ownerId">
                    <el-input v-model="firstTemp.ownerId" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8"></el-col>
                <el-col :span="8"></el-col>
                <el-col :span="8"></el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'承运人编号'" prop="shipperId">
                    <el-input v-model="firstTemp.shipperId" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'供应商编号'" prop="supplierId">
                    <el-input v-model="firstTemp.supplierId" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'承运方式'" prop="transferType">
                     <select-type :isEdit="true" :disabled="!editModel" typeId="SYS_SHIPTYPE" v-model="firstTemp.transferType"></select-type>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'备注'" prop="remark">
                    <el-input v-model="firstTemp.remark" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'有效标志'">
                    <el-switch v-model="firstTemp.enable" :active-value="true" :disabled="!editModel" :inactive-value="false">
                    </el-switch>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item size="mini" :label="'是否入保税库'">
                    <el-switch v-model="firstTemp.inBondedArea" :disabled="!editModel" :active-value="true" :inactive-value="false">
                    </el-switch>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="12">
                  <el-form-item size="mini" :label="'预定时间'" prop="scheduledInboundTime">
                    <!-- <el-input v-model="firstTemp.scheduledInboundTime" :disabled="!editModel"></el-input> -->
                    <el-date-picker
                      v-model="firstTemp.scheduledInboundTime"
                      type="datetime"
                      style="width: 185px;"
                      size="mini"
                      :disabled="!editModel">
                    </el-date-picker>
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item size="mini" :label="'销退箱数'" prop="returnBoxNum">
                    <el-input v-model="firstTemp.returnBoxNum" :disabled="!editModel"></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </el-card>
        </el-col>

        <!-- 第二部分多选 -->
        <el-col :span="!showTitleDialog ? 24 : 12" class="fh detail-card">
          <el-card shadow="nerver" class="demo-card fh" id="secondCard">
            <div slot="header">
              <!-- <el-button class="filter-item show-title-button" size="mini" v-waves :icon="showTitleDialog ? 'el-icon-d-arrow-left' : 'el-icon-d-arrow-right'" @click="showTitleDialog=!showTitleDialog"></el-button> -->
              <i class="show-title-button" :class="showTitleDialog ? 'el-icon-d-arrow-left' : 'el-icon-d-arrow-right'" :title="showTitleDialog ? '展开' : '收缩'" @click="showTitleDialog=!showTitleDialog"></i>
              <span v-if="radio == ''">订单明细</span>
              <span v-else>{{radio}}订单明细</span>
              <el-button v-if="editModel" class="filter-item edit-button" size="mini" v-waves icon="el-icon-plus" @click="onBtnClicked('btnAddDetail')">添加</el-button>
              <el-button v-if="editModel" class="filter-item edit-button delete-button" size="mini" v-waves icon="el-icon-delete" @click="onBtnClicked('btnDelDetail')">删除</el-button>
            </div>
            <el-table ref="secondTable" border :data="secondList" tooltip-effect="dark" style="width: 100%" height="calc(100% - 44px)"
              @row-click="rowClickSecondTable" @selection-change="selChangeSecondTable">
              <el-table-column width="55" type="selection" align="center" prop="custid"> </el-table-column>

              <!--使用服务器返回的表头，如果复杂的表头和格式解析，请自己定义表格格式-->
              <template v-for="(headerItem,index) in secondHeaderList">
                <el-table-column v-if="headerItem.type=='String'||headerItem.type=='Decimal' ||headerItem.type=='DateTime'"
                  :key="index" :prop="headerItem.key" :label="headerItem.description" show-overflow-tooltip :min-width="(headerItem.key ==='prodDate' || headerItem.key ==='expireDate') ? '150px' : '100px'">
                  <template slot-scope="scope">
                    <div v-if="editModel && isEdit(headerItem.key)">
                      <el-date-picker
                        v-model="scope.row[headerItem.key]"
                        type="date"
                        :placeholder="scope.row[headerItem.description]"
                        v-if="headerItem.key ==='prodDate' || headerItem.key ==='expireDate'"
                        style="width: 130px;"
                        value-format="yyyy-MM-dd"
                        size="mini">
                      </el-date-picker>
                      <el-input v-else type="text" size="mini" v-model="scope.row[headerItem.key]" :placeholder="scope.row[headerItem.description]"></el-input>
                    </div>
                    <span v-else>{{scope.row[headerItem.key]}}</span>
                  </template>
                </el-table-column>

                <el-table-column v-else :label="headerItem.description" align="center" min-width="120px" :key="index">
                  <template slot-scope="scope">
                    <div v-if="editModel">
                      <el-select v-if="headerItem.key === 'asnStatus'" size="mini" v-model="scope.row[headerItem.key]">
                        <el-option :value="1" :label="'状态1'"></el-option>
                      </el-select>
                      <el-switch v-else size="mini" v-model="scope.row[headerItem.key]" :active-value="true" :inactive-value="false">
                      </el-switch>
                    </div>
                    <span v-else>{{scope.row[headerItem.key]|filterInt}}</span>
                  </template>
                </el-table-column>

              </template>
            </el-table>
            <!-- 分页 -->
            <pagination v-show="secondTotal>0" :total="secondTotal" :page.sync="secondQuery.page"
              :limit.sync="secondQuery.limit" @pagination="handleSecondPage" />
          </el-card>
        </el-col>
      </el-row>
      <el-card shadow="nerver" v-if="editModel" style="text-align: right;padding-bottom: 10px;"> 
        <el-row>
          <el-col :span="24"><el-button size="mini" @click="editModel = false">取消</el-button>
          <el-button size="mini" v-if="dialogStatus=='create'" type="primary" @click="createData">确认</el-button>
          <el-button size="mini" v-else type="primary" @click="updateData">确认</el-button></el-col>
        </el-row>
      </el-card>
    </div>
  </div>

</template>

<script>
  import * as wmsInboundOrderTbls from '@/api/wmsinboundordertbls'
  import * as wmsInboundOrderDtbls from '@/api/wmsinboundorderdtbls'
  import waves from '@/directive/waves' // 水波纹指令
  import Sticky from '@/components/Sticky'
  import permissionBtn from '@/components/PermissionBtn'
  import Pagination from '@/components/Pagination'
  import elDragDialog from '@/directive/el-dragDialog'
  import { parseTime } from '@/utils/index'
  import SelectType from '@/components/SelectType'
  import { mapGetters } from 'vuex'
  export default {
    name: 'wmsInboundOrderTbls',
    components: {
      Sticky,
      permissionBtn,
      Pagination,
      SelectType
    },
    directives: {
      waves,
      elDragDialog
    },
    data() {
      return {
        // ------------------------主列表数据（头）-----------------------------
        firstHeaderList: [], // 主列表列定义
        radio: '', // 主列表选中项
        firstQuery: { // 主列表查询条件
          page: 1,
          limit: 20,
          key: undefined,
          appId: undefined
        },
        mainList: null, // 主列表值
        firstTotal: 0, // 主列表总条数
        listLoading: true, // 主列表记录总数
        tableKey: 0,
        showTitleDialog: true,

        editModel: false, // 是否为编辑模式
        editType: 'edit', // 编辑类型
        dialogStatus: '', // 主修改对话框状态create/update
        selectRow: {},
        firstTempDefaultVal: { // 头信息默认值
          id: '', // 入库通知单号
          externalNo: '', // 相关单据号
          externalType: '', // 相关单据类型
          status: '', // 入库通知单状态(SYS_INSTCINFORMSTATUS)
          orderType: '', // 入库类型(SYS_INSTCTYPE)
          goodsType: '', // 商品类别
          purchaseNo: '', // 采购单号
          stockId: '', // 仓库编号
          ownerId: '', // 货主编号(固定值CQM)
          shipperId: '', // 承运人编号
          supplierId: '', // 供应商编号
          scheduledInboundTime: '', // 预定入库时间
          remark: '', // 备注
          enable: false, // 有效标志
          transferType: '', // 承运方式
          inBondedArea: false, // 是否入保税库(0:否,1:是)
          returnBoxNum: '', // 销退箱数
          extendInfo: '' // 其他信息,防止自动生成代码最后加逗号，可以删除
        },
        firstTemp: {}, // 当前选中的头信息
        mainRules: {
          appId: [{
            required: true,
            message: '必须选择一个应用',
            trigger: 'change'
          }],
          name: [{
            required: true,
            message: '名称不能为空',
            trigger: 'blur'
          }]
        },
        // ------------------------明细列表数据-------------------------------------
        secondHeaderList: [], // 明细列表列定义
        secondList: [], // 明细列表值
        secondQuery: {}, // 明细列表的过滤条件，如页码、每页条数，搜索关键字等
        secondTotal: 0, // 明细列表总条数
        multipleSelection: [], // 明细列表checkbox选中的值,

        // ------------------------通用数据项-------------------------------------
        statusOptions: [{
          key: 1,
          display_name: '停用'
        },
        {
          key: 0,
          display_name: '正常'
        }
        ],

        textMap: {
          update: '编辑',
          create: '添加'
        }
      }
    },
    computed: {
      ...mapGetters(['defaultorgid'])
    },
    filters: {
      filterInt(val) {
        switch (val) {
          case null:
            return ''
          case 1:
            return '状态1'
          case 2:
            return '状态2'
          default:
            return '默认状态'
        }
      },
      statusFilter(disable) {
        const statusMap = {
          false: 'color-success',
          true: 'color-danger'
        }
        return statusMap[disable]
      }
    },
    created() {
      this.getList()
    },
    methods: {
      isEdit(key) {
        switch (key) {
          case 'updateUserName':
          case 'updateTime':
          case 'createUserName':
          case 'createTime':
            return false
          default:
            return true
        }
      },
      // ------------------------通用处理函数-------------------------------------
      onBtnClicked: function(domId) {
        console.log('you click:' + domId)
        switch (domId) {
          case 'btnAdd': // 添加新记录
            this.resetFirstTemp()
            this.secondList = []
            this.dialogStatus = 'create'
            this.editModel = true
            this.editType = 'add'
            this.$nextTick(() => {
              this.$refs['dataForm'].clearValidate()
            })
            break
          case 'btnEdit': // 编辑头
            this.firstTemp = Object.assign({}, this.selectRow)
            if (this.firstTemp.id === '') {
              this.editModel = false
              this.$message({
                message: '请选择要修改的项',
                type: 'error'
              })
              return
            }
            this.dialogStatus = 'update'
            this.editModel = true
            this.editType = 'edit'
            this.$nextTick(() => {
              this.$refs['dataForm'].clearValidate()
            })
            break
          case 'btnDel': // 删除主表
            if (this.firstTemp.id === '') {
              this.$message({
                message: '请选择要删除的项',
                type: 'error'
              })
              return
            }
            this.handleFirstDel(this.firstTemp)
            break
          case 'btnAddDetail': // 添加明细行
            this.handleAddOrderDetail()
            break
          case 'btnDelDetail': // 删除明细行
            if (this.multipleSelection.length < 1) {
              this.$message({
                message: '至少删除一个',
                type: 'error'
              })
              return
            }
            this.handleSecondDel(this.multipleSelection)
            break
          default:
            break
        }
      },
      // ------------------------主数据列表处理------------------------------------
      getList() {
        this.listLoading = true
        wmsInboundOrderTbls.getList(this.firstQuery).then(response => {
          response.columnHeaders.forEach((item) => { // 首字母小写
            item.key = item.key.substring(0, 1).toLowerCase() + item.key.substring(1)
          })
          this.firstHeaderList = response.columnHeaders.filter(u => u.browsable)
          this.mainList = response.data
          this.firstTotal = response.count

          if (this.firstTotal > 0) {
            this.rowClickFirstTable(this.mainList[0])
          }

          this.listLoading = false
        })
      },
      rowClickFirstTable(row) { // 点击行
        this.radio = row.id
        this.secondQuery.page = 1
        this.secondQuery.limit = 10
        this.querySecondList(this.radio)
        this.showTitleDetail(row)
      },
      handleFilter() {
        this.firstQuery.page = 1
        this.getList()
      },
      handleSizeChange(val) {
        this.firstQuery.limit = val
        this.getList()
      },
      handleCurrentChange(val) {
        this.firstQuery.page = val.page
        this.firstQuery.limit = val.limit
        this.getList()
      },
      resetFirstTemp() {
        this.firstTemp = Object.assign({}, this.firstTempDefaultVal)
      },
      createData() { // 保存提交
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            let tempData = Object.assign({}, this.firstTemp)
            tempData = this.setDetails(tempData)
            tempData.OrgId = this.defaultorgid
            wmsInboundOrderTbls.add(tempData).then(() => {
              this.mainList.unshift(this.firstTemp)
              this.editModel = false
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
      showTitleDetail(row) { // 弹出编辑框
        this.selectRow = Object.assign({}, row) // 新增订单时保存当前选中行
        this.firstTemp = Object.assign({}, row) // copy obj
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      setDetails(tempData) { // 处理明细
        tempData.wmsInboundOrderDtblReqs = []
        tempData.scheduledInboundTime = tempData.scheduledInboundTime && parseTime(tempData.scheduledInboundTime, '{y}-{m}-{d} {h}:{i}:{s}')
        tempData.returnBoxNum = tempData.returnBoxNum && parseInt(tempData.returnBoxNum) || 0
        this.secondList.length > 0 && this.secondList.forEach(item => {
          // item.prodDate = item.prodDate && parseTime(item.prodDate, '{y}-{m}-{d}')
          // item.expireDate = item.expireDate && parseTime(item.expireDate, '{y}-{m}-{d}')
          const obj = {
            id: item.id || '',
            orderId: item.orderId || '',
            price: item.price && parseFloat(item.price) || 0,
            priceNoTax: item.priceNoTax && parseFloat(item.priceNoTax) || 0,
            inStockStatus: item.inStockStatus || false,
            asnStatus: item.asnStatus && parseInt(item.asnStatus) || 0,
            goodsId: item.goodsId || '',
            goodsBatch: item.goodsBatch || '',
            qualityFlg: item.qualityFlg || '',
            orderNum:	item.orderNum && parseFloat(item.orderNum) || 0,
            inNum:	item.inNum && parseFloat(item.inNum) || 0,
            leaveNum:	item.leaveNum && parseFloat(item.leaveNum) || 0,
            holdNum:	item.holdNum && parseFloat(item.holdNum) || 0,
            prodDate: item.prodDate,
            expireDate: item.expireDate,
            taxRate:	item.taxRate && parseFloat(item.taxRate) || 0,
            ownerId: item.ownerId || '',
            remark: item.remark || ''
          }
          tempData.wmsInboundOrderDtblReqs.push(obj)
        })
        return tempData
      },
      updateData() { // 更新提交
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            let tempData = Object.assign({}, this.firstTemp)
            tempData = this.setDetails(tempData)
            wmsInboundOrderTbls.update(tempData).then(() => {
              for (const v of this.mainList) {
                if (v.id === this.firstTemp.id) {
                  const index = this.mainList.indexOf(v)
                  this.mainList.splice(index, 1, this.firstTemp)
                  break
                }
              }

              this.editModel = false
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              })
            })
          }
        })
      },
      handleFirstDel(row) { // 删除头
        wmsInboundOrderTbls.del([row.id]).then(() => {
          this.$notify({
            title: '成功',
            message: '删除成功',
            type: 'success',
            duration: 2000
          })
          const index = this.mainList.indexOf(row)
          this.mainList.splice(index, 1)
        })
      },

      // ------------------------明细列表处理-------------------------------------
      handleSecondPage(e) {
        this.secondQuery = e
        this.querySecondList(this.radio)
      },
      querySecondList(id) {
        wmsInboundOrderDtbls.getList({
          'inboundorderid': id,
          'page': this.secondQuery.page,
          'limit': this.secondQuery.limit,
          'key': this.secondQuery.customerKey
        }).then((res) => {
          res.columnHeaders.forEach((item) => { // 首字母小写
            item.key = item.key.substring(0, 1).toLowerCase() + item.key.substring(1)
          })
          this.secondHeaderList = res.columnHeaders.filter(u => u.browsable)
          this.secondHeaderListC = res.columnHeaders || []
          this.secondTotal = res.count
          this.secondList = res.data
        })
      },
      rowClickSecondTable(row) { // 行点击事件
        this.$refs.multipleTable.clearSelection()
        this.$refs.multipleTable.toggleRowSelection(row)
      },
      handleSecondDel(rows) { // 删除明细时,只删除前端
        rows.forEach(row => {
          const index = this.secondList.indexOf(row)
          this.secondList.splice(index, 1)
        })
      },
      selChangeSecondTable(val) { // 明细选中事件
        this.multipleSelection = val
      },
      handleAddOrderDetail() { // 添加明细
        const obj = {}
        this.secondHeaderListC.forEach(item => {
          obj[item.key] = ''
        })
        obj.orderId = this.firstTemp.id

        this.secondList.push(Object.assign({}, obj))
      }
    }
  }

</script>
<style lang="scss">
  .nomal-form label {
    font-weight: 500 !important;
  }
  .detail-card {
    .el-table__body-wrapper {
      height: calc(100% - 32px) !important;
    }
  }

  .demo-card .el-card__header {
    padding: 10px !important;
    line-height: 1;
    position: relative;
  }

  .el-card__body {
    padding: 10px 10px 0 10px !important;
    height: 100%;
  }
  .form-card .el-card__body{
    padding-right: 0 !important;
  }
  .form-card .el-form{
    padding-right: 5px;
  }

  .dialog-small .el-dialog__header {
    padding: 10px;
    border-bottom: 1px solid #efefef;
  }

  .dialog-small .el-dialog__headerbtn {
    top: 15px;
  }

  .dialog-small .el-dialog__body {
    padding: 10px;
  }

  .el-form-item__error {
    padding-top: 0;
  }

</style>

<style scoped>
  .el-form-item {
    margin-bottom: 15px;
  }

  .m-t-lg {
    margin-top: 20px;
  }

  .pagination-container {
    border: 0;
    padding-left: 0 !important;
  }

  .times .el-date-editor.el-input,
  .times .el-date-editor.el-input__inner {
    width: 120px;
  }

  .times .el-select {
    width: 120px;
  }

  .box-flex .label {
    width: 50px;
  }

  .m-b-sm {
    margin-bottom: 5px;
  }

  .p-l-m {
    padding-left: 10px;
  }

  .p-r-m {
    padding-right: 10px;
  }
  .edit-button{
    position: absolute !important;
    top: 2px;
    right: 5px;
  }
  .delete-button{
    right: 90px;
  }
  .show-title-button{
    cursor: pointer;
    padding: 0 10px 0 0;
  }
</style>
