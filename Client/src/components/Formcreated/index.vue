<template>
  <el-container class="fm2-container" style="height: 100%;">
    <el-header style="height: auto;padding:0;border-bottom: 1px solid #ccc;">
      
        <!-- <el-aside width="250px"> -->
          <div class="components-list" style="padding-bottom: 0;">
            <template v-if="basicFields.length">
              <!-- <div class="created-cate">基础字段</div> -->
              <draggable tag="ul" :list="basicComponents" 
                v-bind="{group:{ name:'people', pull:'clone',put:false},sort:false, ghostClass: 'ghost'}"
                @end="handleMoveEnd"
                @start="handleMoveStart"
                :move="handleMove"
                style="padding-bottom: 0;"
              >
                <template v-for="(item, index) in basicComponents">
                  <li v-if="basicFields.indexOf(item.type)>=0" class="form-edit-created-label" :class="{'no-put': item.type == 'divider'}" :key="index">
                    <a>
                      <i class="icon iconfont" :class="item.icon"></i>
                      <span>{{item.name}}</span>
                    </a>
                  </li>
                </template>
              </draggable>
            </template>
            
            <!-- <template v-if="advanceFields.length">
              <div class="created-cate">高级字段</div>
              <draggable tag="ul" :list="advanceComponents" 
                v-bind="{group:{ name:'people', pull:'clone',put:false},sort:false, ghostClass: 'ghost'}"
                @end="handleMoveEnd"
                @start="handleMoveStart"
                :move="handleMove"
              >
                <template v-for="(item, index) in advanceComponents">
                  <li v-if="advanceFields.indexOf(item.type) >= 0" class="form-edit-created-label" :class="{'no-put': item.type == 'table'}" :key="index">
                    <a>
                      <i class="icon iconfont" :class="item.icon"></i>
                      <span>{{item.name}}</span>
                    </a>
                  </li>
                </template>
              </draggable>
            </template> -->

            
            <!-- <template v-if="layoutFields.length">
              <draggable tag="ul" :list="layoutComponents" 
                v-bind="{group:{ name:'people', pull:'clone',put:false},sort:false, ghostClass: 'ghost'}"
                @end="handleMoveEnd"
                @start="handleMoveStart"
                :move="handleMove"
              >
                <template v-for="(item, index) in layoutComponents">
                  <li v-if="layoutFields.indexOf(item.type) >=0" class="form-edit-created-label no-put" :key="index">
                    <a>
                      <i class="icon iconfont" :class="item.icon"></i>
                      <span>{{item.name}}</span>
                    </a>
                  </li>
                </template>
              </draggable>
            </template> -->
            
          </div>
          
        <!-- </el-aside> -->
    </el-header>
    <el-main class="fm2-main">
      <el-container>
        <el-container class="center-container" direction="vertical">
          <el-main :class="{'created-empty': createdFormData.list.length == 0}">
            <ShowForm v-if="!resetJson"  ref="createdFormData" :data="createdFormData" :select.sync="formDataSelect"></ShowForm>
          </el-main>
          <el-header class="btn-bar" style="height: 45px;">
            <slot name="action">
            </slot>
            <!-- <el-button v-if="upload" type="text" size="medium" icon="el-icon-upload2" @click="handleUpload">导入JSON</el-button> -->
            <el-button v-if="clearable" type="text" size="mini" icon="el-icon-delete" @click="handleClear">清空</el-button>
            <el-button v-if="preview" type="text" size="mini" icon="el-icon-view" @click="handlePreview">预览</el-button>
            <!-- <el-button v-if="generateJson" type="primary" size="mini" icon="el-icon-tickets" @click="handleGenerateJson">提交</el-button> -->
            <!-- <el-button v-if="generateCode" type="text" size="medium" icon="el-icon-document" @click="handleGenerateCode">生成代码</el-button> -->
          </el-header>
        </el-container>
        
        <el-aside class="created-config-container">
          <el-container>
            <el-header height="45px" style="font-size: 0;">
              <div class="config-tab" :class="{active: configTab=='created'}" @click="handleConfigSelect('created')">字段属性</div>
              <div class="config-tab" :class="{active: configTab=='form'}" @click="handleConfigSelect('form')">表单属性</div>
            </el-header>
            <el-main class="config-content">
              <config v-show="configTab=='created'" :data="formDataSelect"></config>
              <CreatedFormConfig v-show="configTab=='form'" :data="createdFormData.config"></CreatedFormConfig>
            </el-main>
          </el-container>
        </el-aside>
        <Model
          :visible="previewVisible"
          @on-close="previewVisible = false"
          ref="formPreview"
          width="1000px"
          form
        >
          <CreatedForm insite="true" @on-change="handleDataChange" v-if="previewVisible" :data="createdFormData" :value="formDataModels" :remote="remoteFuncs" ref="generateForm">
            <template v-slot:blank="scope">
              Width <el-input v-model="scope.model.blank.width" style="width: 100px"></el-input>
              Height <el-input v-model="scope.model.blank.height" style="width: 100px"></el-input>
            </template>
          </CreatedForm>

          <template slot="action">
            <el-button type="primary" @click="handleGetData">获取数据</el-button>
            <el-button @click="handleReset">重置</el-button>
          </template>
        </Model>

        <!-- <Model
          :visible="uploadVisible"
          @on-close="uploadVisible = false"
          @on-submit="handleUploadJson"
          ref="uploadJson"
          width="800px"
          form
        >
          <el-alert type="info" :title="'JSON格式如下，直接复制生成的json覆盖此处代码点击确定即可'"></el-alert>
          <codemirror
            style="height:100%;"
            ref="myEditor"
            v-model="exampleJSON"
          ></codemirror>
        </Model> -->

        <Model
          :visible="jsonVisible"
          @on-close="jsonVisible = false"
          ref="jsonPreview"
          width="800px"
          form
        >
          
          <codemirror
            style="height:100%;"
            ref="myEditor"
            v-model="jsonTemplate"
          ></codemirror>          
          <template slot="action">
            <el-button type="primary" class="json-btn" :data-clipboard-text="jsonCopyValue">复制</el-button>
          </template>
        </Model>

        <!-- <Model
          :visible="codeVisible"
          @on-close="codeVisible = false"
          ref="codePreview"
          width="800px"
          form
          :action="false"
        >
          <codemirror
            style="height:100%;"
            ref="myEditor"
            v-model="htmlTemplate"
          ></codemirror>
        </Model> -->
      </el-container>
    </el-main>
  </el-container>
  
</template>

<script>
import { codemirror } from 'vue-codemirror-lite'
import Draggable from 'vuedraggable'
import Config from './components/Config'
import CreatedFormConfig from './components/CreatedFormConfig'
import ShowForm from './components/ShowForm'
import Model from './components/Model'
import CreatedForm from './components/CreatedForm'
// import Clipboard from 'clipboard'
import { basicComponents, layoutComponents, advanceComponents } from './config/config.js'
import code from './config/code.js'
export default {
  name: 'making-form',
  components: {
    Draggable,
    Config,
    CreatedFormConfig,
    ShowForm,
    Model,
    CreatedForm,
    codemirror
  },
  props: {
    editInfo: {
      type: Object
    },
    preview: {
      type: Boolean,
      default: true
    },
    generateCode: {
      type: Boolean,
      default: true
    },
    generateJson: {
      type: Boolean,
      default: true
    },
    upload: {
      type: Boolean,
      default: true
    },
    clearable: {
      type: Boolean,
      default: true
    },
    basicFields: {
      type: Array,
      default: () => ['grid', 'input', 'textarea', 'number', 'radio', 'checkbox', 'time', 'date', 'rate', 'color', 'select', 'switch', 'slider', 'text', 'imgupload']
    }
    // advanceFields: {
    //   type: Array,
    //   default: () => ['blank', 'imgupload', 'editor', 'cascader']
    // },
    // layoutFields: {
    //   type: Array,
    //   default: () => ['grid']
    // }
  },
  data() {
    return {
      basicComponents,
      layoutComponents,
      advanceComponents,
      resetJson: false,
      createdFormData: {
        list: [],
        config: {
          labelWidth: 100,
          labelPosition: 'right',
          size: 'small'
        }
      },
      configTab: 'created',
      formDataSelect: null,
      previewVisible: false,
      jsonVisible: false,
      // codeVisible: false,
      // uploadVisible: false,
      remoteFuncs: {
        func_test(resolve) {
          setTimeout(() => {
            const options = [
              { id: '1', name: '1111' },
              { id: '2', name: '2222' },
              { id: '3', name: '3333' }
            ]
            resolve(options)
          }, 2000)
        },
        funcGetToken() {
          // request.get('http://tools-server.xiaoyaoji.cn/api/uptoken').then(res => {
          //   resolve(res.uptoken)
          // })
        },
        upload_callback() {
        }
      },
      formDataModels: {},
      blank: '',
      htmlTemplate: '',
      jsonTemplate: '',
      uploadEditor: null,
      jsonCopyValue: '',
      jsonClipboard: null
      // exampleJSON: `{
      //   "list": [],
      //   "config": {
      //     "labelWidth": 100,
      //     "labelPosition": "top",
      //     "size": "small"
      //   }
      // }`
    }
  },
  created() {
    this._loadComponents()
  },
  methods: {
    _loadComponents() {
      const fields = {
        input: '单行文本',
        textarea: '多行文本',
        number: '计数器',
        radio: '单选框组',
        checkbox: '多选框组',
        time: '时间选择器',
        date: '日期选择器',
        select: '下拉选择框',
        switch: '开关',
        text: '文字',
        imgupload: '图片/文件',
        grid: '栅格布局'
      }
      this.basicComponents = this.basicComponents.map(item => {
        return {
          ...item,
          name: fields[item.type]
        }
      })
      this.advanceComponents = this.advanceComponents.map(item => {
        return {
          ...item,
          name: fields[item.type]
        }
      })
      this.layoutComponents = this.layoutComponents.map(item => {
        return {
          ...item,
          name: fields[item.type]
        }
      })
    },
    handleConfigSelect(value) {
      this.configTab = value
    },
    handleMoveEnd() {
    },
    handleMoveStart() {
    },
    handleMove() {
      return true
    },
    handlePreview() {
      this.previewVisible = true
    },
    handleGetData() {
      this.$refs.generateForm.getData().then(data => {
        this.jsonVisible = true
        this.jsonTemplate = JSON.stringify(data, null, '\t')
        // this.$alert(data, '').catch(e => {})
        // this.$refs.formPreview.end()
      }).catch(() => {
        this.$refs.formPreview.end()
      })
    },
    handleReset() {
      this.$refs.generateForm.reset()
    },
    handleGenerateJson() {
      // this.jsonVisible = true
      // this.jsonTemplate = JSON.stringify(this.createdFormData, null, '\t')
      // console.log(this.createdFormData)
      return this.createdFormData
      // this.$nextTick(() => {
      //   // const editor = ace.edit('jsoneditor')
      //   // editor.session.setMode('ace/mode/json')
      //   if (!this.jsonClipboard) {
      //     this.jsonClipboard = new Clipboard('.json-btn')
      //     this.jsonClipboard.on('success', (e) => {
      //       this.$message.success('复制成功')
      //     })
      //   }
      //   this.jsonCopyValue = JSON.stringify(this.createdFormData)
      // })
    },
    // handleGenerateCode() {
    //   this.codeVisible = true
    //   this.htmlTemplate = code(JSON.stringify(this.createdFormData))
    //   this.$nextTick(() => {
    //     // const editor = ace.edit('codeeditor')
    //     // editor.session.setMode('ace/mode/html')
    //   })
    // },
    // handleUpload() {
    //   // this.uploadVisible = true
    //   this.$nextTick(() => {
    //     // this.uploadEditor = ace.edit('uploadeditor')
    //     // this.uploadEditor.session.setMode('ace/mode/json')
    //   })
    // },
    // handleUploadJson() {
    //   try {
    //     // this.setJSON(JSON.parse(this.uploadEditor.getValue()))
    //     this.uploadVisible = false
    //   } catch (e) {
    //     this.$message.error(e.message)
    //     this.$refs.uploadJson.end()
    //   }
    // },
    handleClear() {
      this.createdFormData = {
        list: [],
        config: {
          labelWidth: 100,
          labelPosition: 'right',
          size: 'small',
          customClass: ''
        }
      }
      this.formDataSelect = {}
    },
    getJSON() {
      return this.createdFormData
    },
    getHtml() {
      return code(JSON.stringify(this.createdFormData))
    },
    setJSON(json) {
      this.createdFormData = json
      if (json.list.length > 0) {
        this.formDataSelect = json.list[0]
      }
    },
    handleInput(val) {
      this.blank = val
    },
    handleDataChange() {
    }
  },
  watch: {
    createdFormData: {
      deep: true,
      handler: function() {
      }
    },
    editInfo: {
      deep: true,
      handler() {
        if (this.editInfo && this.editInfo.list) {
          this.createdFormData = { ...this.editInfo }
          this.formDataSelect = this.editInfo && this.editInfo.list.length > 0 && this.editInfo.list[0]
        }
      }
    }
    // '$lang': function(val) {
    //   this._loadComponents()
    // }
  }
}
</script>

<style lang="scss">
@import './styles/index.scss';
.created-empty{
  background-position: 50%;
}
</style>