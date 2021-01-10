<template>
  <div class="createPost-container">
    <sticky :className="'sub-navbar '">
      <el-row>
        <el-col :span="8"> {{flowObj.customName}}</el-col>
        <el-col :span="16">
          <el-input style="width:200px" v-model="postObj.verificationOpinion" placeholder="请输入审核意见"></el-input>
          <verify-status v-model="postObj.verificationFinally" />
          <el-select v-model="postObj.NodeRejectType" placeholder="" v-if="postObj.verificationFinally =='3'">
            <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
          <el-select v-model="postObj.NodeRejectStep" placeholder="" v-if="postObj.NodeRejectType =='2' && postObj.verificationFinally =='3'">
            <el-option v-for="item in FlowNodes" :key="item.id" :label="item.name" :value="item.id">
            </el-option>
          </el-select>
          <el-button v-loading="loading" style="margin-left: 10px;" type="success" @click="submitForm">提交
          </el-button>
        </el-col>
      </el-row>
    </sticky>

    <el-card class="box-card" v-if="flowObj.frmType ==1">
      <component v-bind:is="flowObj.dbName != '' && flowObj.dbName +'Detail'" :frmData="flowObj.frmData"></component>
    </el-card>
    <el-card class="box-card" v-if="flowObj.frmType == 2">
      <div style="padding-right: 25px;">
        <CreatedForm insite="true" ref="createdForm" v-if="createdFormData.list && createdFormData.list.length > 0"
          :data="createdFormData" :value="defaultValue"></CreatedForm>
      </div>
    </el-card>
    <el-card class="box-card" v-else>
      <p v-html="flowObj.frmPreviewHtml"></p>
    </el-card>
    <el-card class="box-card">
      <div style="height: 500px">
        <CreatedFlow ref="createdFlow" :form-template="currentForm" :isEdit="true" :isShowContent="isShowContent"
          :scheme-content="flowObj.schemeContent"></CreatedFlow>
      </div>
    </el-card>
    <!-- <flow-detail ref="flowEditor" :instance="flowObj"></flow-detail> -->
  </div>
</template>

<script>
  import Sticky from '@/components/Sticky' // 粘性header组件
  import VerifyStatus from './Dropdown/VerifyStatus'
  import * as apiFlowinstances from '@/api/flowinstances'
  import CreatedFlow from '@/components/CreatedFlow'
  import CreatedForm from '@/components/Formcreated/components/CreatedForm'

  export default {
    name: 'verify-flowinstance',
    components: {
      Sticky,
      VerifyStatus,
      CreatedFlow,
      CreatedForm
    },
    data() {
      return {
        isShowContent: true,
        currentForm: null,
        loading: false, // 按钮加载loading
        FlowNodes: [], // 当前实例的所有节点
        postObj: {
          flowInstanceId: '',
          verificationOpinion: '',
          verificationFinally: 1,
          NodeRejectStep: '',
          NodeRejectType: '0' // 默认上一步
        },
        createdFormData: {
          list: [],
          config: {}
        },
        options: [{
          value: '0',
          label: '上一步'
        }, {
          value: '1',
          label: '第一步'
        }, {
          value: '2',
          label: '指定步骤'
        }],
        defaultValue: {},
        flowObj: {
          customName: '',
          schemeContent: '',
          createDate: '',
          activityName: '',
          code: '',
          dbName: '',
          frmType: 0,
          frmData: '',
          activityId: ''
        }
      }
    },
    created() {
      const id = this.$route.params && this.$route.params.id
      this.postObj.flowInstanceId = id
      apiFlowinstances.get({
        id: id
      }).then(response => {
        this.flowObj = response.result
        if (this.flowObj.frmType !== 1) {
          this.createdFormData = this.setCreatedFormData(JSON.parse(response.result.frmContentData))
        }
        this.defaultValue = JSON.parse(response.result.frmData)
        var nodes = JSON.parse(this.flowObj.schemeContent).nodes
        this.FlowNodes = nodes // 需要过滤条件，应该退回到已经处理过的某个节点
      }).catch(err => {
        console.log(err)
      })
    },
    methods: {
      submitForm() {
        this.loading = true
        apiFlowinstances.verify(this.postObj).then(() => {
          this.loading = false
          this.$notify({
            title: '成功',
            message: '创建成功',
            type: 'success',
            duration: 2000
          })
        })
      },
      setCreatedFormData(data) {
        data && data.list && data.list.length > 0 && data.list.forEach(item => {
          item.options.disabled = true
        })
        return data
      }
    }
  }

</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  @import "src/styles/mixin.scss";

  .createPost-container {
    position: relative;

    .createPost-main-container {
      padding: 40px 45px 20px 50px;

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
