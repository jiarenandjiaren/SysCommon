<template>
  <div>
    <sticky :className="'sub-navbar '">
      <el-button v-if="active>0" size="mini" style="margin-left: 10px;" type="primary" @click="pre">上一步</el-button>
      <el-button v-if="active<1" size="mini" style="margin-left: 10px;" @click="next">下一步</el-button>
      <el-button v-loading="loading" size="mini" style="margin-left: 10px;" type="success" @click="submitForm">保存
      </el-button>
    </sticky>
    <div class="createPost-container">
      <el-form class="form-container" :model="postObj" :rules="rules" ref="postObj">
        <el-steps class="steps" :active="active" finish-status="success">
          <el-step title="选择模板"></el-step>
          <el-step title="基础信息"></el-step>
        </el-steps>
        <!--选择模板-->
        <div class="createPost-main-container" v-show="active==0">
          <el-form-item label=" ">
            <el-select style="width: 400px;" v-model="currentSchemeId" size="small" placeholder="请选择">
              <el-option v-for="item in flowschemes" :key="item.id" :label="item.schemeName" :value="item.id"></el-option>
             </el-select>
          </el-form-item>
          <div class="flow-form-content">
            
            <el-card class="box-card" v-if="postObj.frmType == 1">
             <component ref="frmData" v-bind:is="postObj.dbName +'Add'"></component>
            </el-card>
            <template v-else-if="postObj.frmType === 2">
              <CreatedForm insite="true" ref="createdForm" v-if="createdFormData.list && createdFormData.list.length > 0" :data="createdFormData" :value="{}">
              </CreatedForm>
            </template>
            <form ref="frmData" v-else>
              <p v-html="frmPreview"></p>
            </form>
          </div>
          <div class="flow-form-content" style="height: 500px;">
            <CreatedFlow ref="createdFlow" :form-template="currentForm" :isEdit="true" :isShowContent="isShowContent" :scheme-content="currentScheme.schemeContent"></CreatedFlow>
          </div>
        </div>

        <!--编辑基础信息-->
        <div class="createPost-main-container" v-show="active==1">
          <el-row>
            <el-col :span="12">
              <el-form-item label="标题" label-width="100px" prop="schemeNameRule">
                <el-input name="name" v-model="postObj.customName" required style="max-width: 200px;">
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label-width="100px" label="实例编号:">
                <el-input type="text" v-model="postObj.code" style="max-width: 200px;">
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label-width="100px" label="发布时间:" class="postInfo-container-item">
                <el-date-picker v-model="postObj.createDate" type="datetime" format="yyyy-MM-dd HH:mm:ss" placeholder="选择日期时间">
                </el-date-picker>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label-width="100px" label="重要性(占位):" class="postInfo-container-item">
                <el-rate style="margin-top:8px;" :max='3' :colors="['#99A9BF', '#F7BA2A', '#FF9900']" :low-threshold="1"
                  :high-threshold="3">
                </el-rate>
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item label-width="100px" label="摘要:">
                <el-input type="textarea" :rows="3" placeholder="请输入内容" v-model="postObj.description">
                </el-input>
                <span class="word-counter" v-if="contentShortLength">{{contentShortLength}}字</span>
              </el-form-item>
            </el-col>
          </el-row>
        </div>

      </el-form>
    </div>
  </div>
</template>

<script>
  import {
    formatTime
  } from '@/utils/index'
  import MDinput from '@/components/MDinput'
  import Sticky from '@/components/Sticky' // 粘性header组件
  import * as apiFlowschemes from '@/api/flowschemes'
  import * as apiFlowinstances from '@/api/flowinstances'
  import * as apiForms from '@/api/forms'
  import CreatedForm from '@/components/Formcreated/components/CreatedForm'
  import CreatedFlow from '@/components/CreatedFlow'
  import { mapGetters } from 'vuex'

  export default {
    name: 'flowinstance-add',
    components: {
      MDinput,
      Sticky,
      CreatedForm,
      CreatedFlow
    },
    data() {
      const validateRequire = (rule, value, callback) => {
        if (value === '') {
          this.$message({
            message: rule.field + '为必传项',
            type: 'error'
          })
          callback(null)
        } else {
          callback()
        }
      }
      return {
        isShowContent: true,
        currentForm: null,
        postObj: {
          id: undefined,
          code: new Date().getTime(),
          dbName: '', // 关联的数据库名
          frmType: 0, // 表单类型 0：默认动态表单，1：用户自定义页面
          description: '',
          customName: '',
          frmData: '',
          schemeId: ''
        },
        loading: false,
        flowschemes: [], // 选择的流程模板列表
        createdFormData: {
          list: [],
          config: {}
        },
        fields: 0, // 表单数据元素个数
        currentSchemeId: '',
        currentScheme: {
          schemeContent: ''
        }, // 当前选中的流程模板
        frmPreview: '',
        active: 0,
        rules: {
          schemeNameRule: [{
            validator: validateRequire
          }],
          frmRule: [{
            validator: validateRequire
          }]
        }
      }
    },
    watch: {
      currentSchemeId(id) {
        // this.isShowContent = false
        this.currentScheme.schemeContent = ''
        this.onSchemeChange(id)
      }
    },
    computed: {
      ...mapGetters(['defaultorgid']),
      contentShortLength() {
        return this.postObj.description ? this.postObj.description.length : 0
      }
    },
    created() {
      apiFlowschemes.getList().then(response => {
        // 获取模板列表
        this.flowschemes = response.data
        // 初始化值
        this.currentSchemeId = this.flowschemes[0].id
        // this.onSchemeChange(this.flowschemes[0].id)
      })
    },
    methods: {
      next() {
        if (this.active++ > 1) this.active = 0
      },
      pre() {
        if (this.active-- < 0) this.active = 0
      },
      onSchemeChange(schemeId) {
        // 预览表单和流程
        var _this = this
        this.currentScheme = Object.assign({}, this.flowschemes.find(u => u.id === schemeId))
        this.postObj.customName = this.currentScheme.schemeName + formatTime('yyyy-MM-dd hh:mm:ss')
        this.postObj.schemeId = schemeId
        apiForms
          .get({
            id: this.currentScheme.frmId
          })
          .then(response => {
            if (response.result.frmType === 1) { // 用户自定义的页面
              _this.postObj.frmType = 1
              _this.postObj.dbName = response.result.webId
            } else if (response.result.frmType === 2) {
              _this.postObj.frmType = 2
              _this.postObj.dbName = ''
              response.result.html = response.result.content // 暂无用content替代一下html
              //  _this.frmPreview = response.result.content
              _this.createdFormData = JSON.parse(response.result.contentData)
              _this.frmPreview = response.result.html // 表单预览的数据
              _this.fields = response.result.fields // 表单属性的个数
            } else {
              _this.postObj.frmType = 0
              _this.postObj.dbName = ''
              _this.frmPreview = response.result.content
              _this.frmPreview = response.result.html // 表单预览的数据
              _this.fields = response.result.fields // 表单属性的个数
            }
          })
      },
      handleAdd() {
        const _this = this
        _this.postObj.OrgId = this.defaultorgid

        apiFlowinstances.add(_this.postObj).then(() => {
          _this.loading = false
          _this.$notify({
            title: '成功',
            message: '创建成功',
            type: 'success',
            duration: 2000
          })
        })
        _this.loading = false
      },
      submitForm() {
        var _this = this
        _this.$refs.postObj.validate(valid => {
          if (valid) {
            _this.loading = true
            if (_this.postObj.schemeContent === '') {
              _this.$message({
                message: '请选择一个流程模板',
                type: 'error'
              })
              _this.loading = false
              return
            }
            console.log(_this.postObj.frmType)
            if (_this.postObj.frmType === 1) { // 使用的是自定义的页面提交
              _this.postObj.frmData = JSON.stringify(_this.$refs.frmData.getData())
            } else if (_this.postObj.frmType === 0) { // 解析表单中提交的数据
              const frmdata = {}
              for (let i = 0; i < _this.$refs.frmData.elements.length; i++) {
                frmdata[ _this.$refs.frmData[i].name] = _this.$refs.frmData[i].value
              }
              _this.postObj.frmData = JSON.stringify(frmdata)
            } else {
              _this.$refs.createdForm.getData().then(res => {
                _this.postObj.frmData = JSON.stringify(res)
                _this.handleAdd()
              }).catch(() => {
                _this.loading = false
              })
              return
            }
            _this.handleAdd()
          } else {
            console.log('error submit!!')
            return false
          }
        })
      }
    }
  }

</script>

<style rel="stylesheet/scss" lang="scss">
  @import 'src/styles/mixin.scss';
   iframe {
     border: 0;
   }
  .createPost-container {
    margin: 10px;
    padding: 10px;
    background-color: white;
    min-height: 100%;

    .createPost-main-container {
      max-width: 1000px;
      margin: 0 auto;
      margin-top: 40px;
    }
  }

  .steps {
    max-width: 1000px;
    margin: 0 auto;

    .el-step__main {
      margin-left: -21px;
    }
  }

  .edit-btns {
    padding: 20px;
  }

  .el-tabs--border-card {
    border: 1px solid #dcdfe6;
    -webkit-box-shadow: 0 0px 0px 0 rgba(0, 0, 0, 0.12), 0 0 0px 0 rgba(0, 0, 0, 0.04);
    box-shadow: 0 0px 0px 0 rgba(0, 0, 0, 0.12), 0 0 0px 0 rgba(0, 0, 0, 0.04);
  }

  .el-tabs--border-card.border-b-none {
    border-bottom: 0 !important;
  }

  .editor {
    padding: 15px;
    border: 1px solid #dcdfe6;
    border-top: 0;
  }
  
  .flow-form-content{
    margin-top: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    padding: 10px;
  }
</style>
