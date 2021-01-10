<template>
  <div class="createPost-container" style="height: 100%;overflow:hidden;">
    <el-container style="height: 100%;">
      <el-header style="padding: 0;height: 42px;">
        <sticky :className="'sub-navbar '">
          <div class="filter-container">
            <el-button v-loading="loading" size="mini" class="filter-item" style="margin-left: 10px;" @click="submitForm">保存
            </el-button>
          </div>
        </sticky>
      </el-header>
      <el-main style="padding:0;height: calc(100% - 42px);">
        <el-container style="height: 100%;">
          <el-aside style="background: #fff;">
            <el-form class="form-container" :model="postForm" :rules="rules" ref="postForm" label-position="top">
              <div class="createPost-main-container" style="margin: 0;height: 100%;">
                <el-form-item prop="name">
                  <MDinput name="name" v-model="postForm.name" required :maxlength="100">
                    标题
                  </MDinput>
                </el-form-item>

                <el-form-item label="摘要" label-position="top">
                  <el-input type="textarea" :rows="6" placeholder="请输入内容" v-model="postForm.description">
                  </el-input>
                  <span class="word-counter" v-show="contentShortLength">{{contentShortLength}}字</span>
                </el-form-item>

                <el-form-item label="表单类型" label-position="top">
                  <el-select v-model="postForm.frmType" placeholder="" style="width: 100%;">
                    <el-option label="可拖拽动态表单" :value=2></el-option>
                    <el-option label="动态表单" :value=0></el-option>
                    <el-option label="自定义开发页面" :value=1></el-option>
                    <!-- 动态element-ui在1.5版本中发布 -->
                    <!-- <el-option label="自定义element-ui" :value=2></el-option> -->
                  </el-select>

                  <el-select style="margin-top: 10px;width: 100%;" v-model="postForm.webId" v-if="postForm.frmType == 1" placeholder="请选择系统内置的页面">
                    <el-option label="请假条" value='FrmLeaveReq'></el-option>
                  </el-select>
                </el-form-item>
              </div>
            </el-form>
          </el-aside>
          <el-main style="padding:0;">
            <div class="editor-container" style="height: 100%;" v-if="postForm.frmType === 0 || postForm.frmType === 2">
              <FormContainer ref="contentDataForm" :edit-info="editInfo" v-if="postForm.frmType === 2"></FormContainer>
              <template v-if="postForm.frmType === 0">
                <Ueditor ref="ue" v-bind:content="postForm.content" :formType="postForm.frmType" v-bind:fileds="postForm.fields" @ready="ueReady"></Ueditor>
              </template>
            </div>

            <el-card class="box-card" v-if="postForm.frmType == 1">
              <component v-bind:is="postForm.webId != '' && postForm.webId +'Add'"></component>
            </el-card>
          </el-main>
        </el-container>
      </el-main>
    </el-container>
  </div>
</template>

<script>
  import MDinput from '@/components/MDinput'
  import Ueditor from '@/components/Ueditor'
  import Sticky from '@/components/Sticky' // 粘性header组件
  import FormContainer from '@/components/Formcreated/index'
  import * as forms from '@/api/forms'
  import { mapGetters } from 'vuex'

  const defaultForm = {
    id: undefined,
    description: '',
    webId: '', // 用户开发的页面ID
    frmType: 2, // 表单类型，默认为0动态表单，1为用户开发的页面
    name: '',
    fields: 0, // 字段个数
    delete: 0, // 删除标记
    disabled: 0,
    contentData: '', // 表单中的控件属性描述
    contentParse: '', // 表单控件位置模板
    content: '' // 未处理的HTML
  }

  export default {
    name: 'form-detail',
    components: {
      MDinput,
      Sticky,
      Ueditor,
      FormContainer
    },
    props: {
      isEdit: {
        type: Boolean,
        default: false
      }
    },
    data() {
      const validateRequire = (rule, value, callback) => {
        if (value === '') {
          this.$message({
            message: rule.field + '为必传项',
            type: 'error'
          })
        } else {
          callback()
        }
      }
      return {
        postForm: Object.assign({}, defaultForm),
        loading: false,
        rules: {
          name: [{
            validator: validateRequire, trigger: 'blur'
          }]
        }
      }
    },
    computed: {
      ...mapGetters(['defaultorgid']),
      contentShortLength() {
        return this.postForm && this.postForm.description ? this.postForm.description.length : 0
      },
      editInfo() {
        return this.postForm.contentData && JSON.parse(this.postForm.contentData) || {}
      }
    },
    mounted() {
      this.postForm.frmType = this.$route.query['frmType'] !== undefined ? this.$route.query['frmType'] : 2
      if (!this.isEdit) {
        this.postForm = Object.assign({}, defaultForm)
      } else {
        // ueditor需要准备好了调用数据，frmtype为2时，不调用ueditor
        if (Number(this.postForm.frmType) !== 0) {
          const id = this.$route.params && this.$route.params.id
          this.fetchData({
            id: id
          })
        }
      }
    },

    methods: {
      ueReady() { // ueditor准备好了，来数据吧
        if (this.isEdit && !this.postForm.contentData) {
          const id = this.$route.params && this.$route.params.id
          this.fetchData({
            id: id
          })
        }
      },
      fetchData(id) {
        forms.get(id).then(response => {
          this.postForm = response.result
        }).catch(err => {
          console.log(err)
        })
      },
      submitForm() {
        if (this.postForm.frmType === 0) { // 动态表单需要获取ue中的值
          this.postForm = Object.assign(this.postForm, this.$refs.ue.getObj())
        } else if (this.postForm.frmType === 2) {
          this.postForm.contentData = JSON.stringify(this.$refs.contentDataForm.handleGenerateJson())
        }
        this.$refs.postForm.validate(valid => {
          if (valid) {
            this.loading = true
            this.postForm.OrgId = this.defaultorgid
            if (this.isEdit) {
              this.postForm.id = this.$route.params && this.$route.params.id
              forms.update(this.postForm).then(() => {
                this.loading = false
                this.$notify({
                  title: '成功',
                  message: '修改成功',
                  type: 'success',
                  duration: 2000
                })
              })
            } else {
              forms.add(this.postForm).then(() => {
                this.loading = false
                this.$notify({
                  title: '成功',
                  message: '创建成功',
                  type: 'success',
                  duration: 2000
                })
              })
            }
          } else {
            console.log('error submit!!')
            return false
          }
        })
      }
    }
  }

</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  @import "src/styles/mixin.scss";

  .createPost-container {
    position: relative;

    .createPost-main-container {
      margin: 10px;
      background-color: white;
      padding: 10px;
      padding-top: 0;

      .postInfo-container {
        position: relative;
        @include clearfix;
        margin-bottom: 10px;

        .postInfo-container-item {
          float: left;
        }
      }

      .editor-container {
        min-height: 500px;
        margin: 0 0 30px;

        .editor-upload-btn-container {
          text-align: right;
          margin-right: 10px;

          .editor-upload-btn {
            display: inline-block;
          }
        }
      }
    }

    .word-counter {
      width: 40px;
      position: absolute;
      right: -10px;
      top: 0px;
    }
  }

</style>
