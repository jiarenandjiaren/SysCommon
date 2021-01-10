<template>
	<div style="height: 100%;" class="flow-attr-box">
		<el-tabs size="small" :value="activeKey" style="height: 100%;">
			<el-tab-pane name="flow-attr" disabled>
				<span slot="label">
					<i class="el-icon-s-operation" />
					流程属性
				</span>
				<el-form layout="horizontal">
					<el-form-item size="small" label="流程id" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
						<el-input :value="flowData.attr.id" disabled></el-input>
					</el-form-item>
				</el-form>
			</el-tab-pane>
			<el-tab-pane name="node-attr" disabled>
				<span slot="label">
					<i class="el-icon-notebook-2" />
					节点属性
				</span>
				<template v-if="currentSelect && currentSelect.id">
					<el-form :rules="rules" ref="dataForm" :model="currentSelect">
						<el-form-item size="small" :label="'Id'" prop="id">
							<el-input v-model="currentSelect.id" readonly disabled></el-input>
						</el-form-item>

						<el-form-item size="small" :label="'名称'" prop="name">
							<el-input v-model="currentSelect.name"></el-input>
						</el-form-item>
						<template  v-if="currentSelect.setInfo">
							<el-form-item size="small" :label="'三方回调URL'" prop="ThirdPartyUrl">
								<el-input v-model="currentSelect.setInfo.ThirdPartyUrl"></el-input>
							</el-form-item>
							<el-form-item size="small" :label="'执行权限'" prop="NodeDesignate">
								<el-select
									class="filter-item"
									style="width:100%"
									v-model="currentSelect.setInfo.NodeDesignate"
									placeholder="请选择"
									@change="handleChangeRoles"
									:popper-append-to-body="false"
								>
									<el-option
										v-for="item in  NodeDesignates"
										:key="item.id"
										:label="item.name"
										:value="item.id"
									></el-option>
								</el-select>
							</el-form-item>

							<el-form-item
								size="small"
								:label="'指定用户'"
								v-if="currentSelect.setInfo.NodeDesignate ==='SPECIAL_USER'"
								prop="NodeDesignateUsers"
							>
								<select-users
									:userNames.sync="currentSelect.setInfo.NodeDesignateData.Texts"
									:users="currentSelect.setInfo.NodeDesignateData.users"
									v-on:users-change="usersChange"
								></select-users>
							</el-form-item>

							<el-form-item
								size="small"
								:label="'指定角色'"
								v-if="currentSelect.setInfo.NodeDesignate ==='SPECIAL_ROLE'"
								prop="NodeDesignateUsers"
							>
								<select-roles
									:userNames.sync="currentSelect.setInfo.NodeDesignateData.Texts"
									:roles="currentSelect.setInfo.NodeDesignateData.roles"
									v-on:roles-change="rolesChange"
								></select-roles>
							</el-form-item>

							<el-form-item
								size="small"
								:label="'会签类型'"
								v-if="currentSelect.type=='fork'"
								prop="NodeConfluenceType"
							>
								<el-select
									class="filter-item"
									style="width:100%"
									v-model="currentSelect.setInfo.NodeConfluenceType"
									placeholder="请选择"
									:popper-append-to-body="false"
								>
									<el-option
										v-for="item in  NodeConfluenceTypes"
										:key="item.id"
										:label="item.name"
										:value="item.id"
									></el-option>
								</el-select>
							</el-form-item>
						</template>
					</el-form>
				</template>
			</el-tab-pane>
			<el-tab-pane name="link-attr" disabled>
				<span slot="label">
					<i class="el-icon-share" />
					连线属性
				</span>
				<el-form :label-position="'top'">
					<el-form-item size="small" label="id" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
						<el-input :value="currentSelect.id" disabled />
					</el-form-item>
					<el-form-item size="small" label="源节点" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
						<el-input :value="currentSelect.from" disabled />
					</el-form-item>
					<el-form-item size="small" label="目标节点" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
						<el-input :value="currentSelect.to" disabled />
					</el-form-item>
					<el-form-item size="small" label="文本" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
						<el-input v-model="currentSelect.label" @change="linkLabelChange" />
					</el-form-item>
					<el-form-item style="display: inline-block;" size="small" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol" label="表单字段条件">
						<div v-for="(compare, index) in Compares" :key="index" style="margin-bottom: 5px;">
							<el-select :popper-append-to-body="false" v-model="compare.FieldName" style="width: 85px;" placeholder="请选择">
								<el-option
									v-for="item in formTemplate"
									:key="item.name"
									:label="item.title"
									:value="item.name"
								></el-option>
							</el-select>
							<el-select :popper-append-to-body="false" :disabled="!compare.FieldName" v-model="compare.Operation" style="width: 85px;" placeholder="请选择">
								<el-option label=">" value=">"></el-option>
								<el-option label=">=" value=">="></el-option>
								<el-option label="<" value="<"></el-option>
								<el-option label="<=" value="<="></el-option>
								<el-option label="=" value="="></el-option>
								<el-option label="!=" value="!="></el-option>
							</el-select>
							<el-input v-model="compare.Value" style="width: 80px;" :disabled="!compare.FieldName" placeholder="值"></el-input>
							<el-button
								type="primary"
								size="mini"
								icon="el-icon-plus"
								v-if="index === 0"
								title="并且"
								@click="btnAddCompare"
							></el-button>
							<el-button
								type="danger"
								size="mini"
								icon="el-icon-delete"
								v-if="index != 0"
								title="删除"
								@click="btnDelCompare(index)"
							></el-button>
						</div>
					</el-form-item>
				</el-form>
			</el-tab-pane>
		</el-tabs>
	</div>
</template>

<script>
// import jsplumb from 'jsplumb'
import SelectUsers from '@/components/SelectUsers'
import SelectRoles from '@/components/SelectRoles'
import {mapGetters, mapActions} from 'vuex'

export default {
  props: ['plumb', 'flowData', 'formTemplate'],
  components: {
    // jsplumb,
    SelectUsers,
    SelectRoles
  },
  data() {
    return {
      currentSelect: '',
			Compares: '',
			flag: false,
      formItemLayout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 16 }
      },
      NodeConfluenceTypes: [
        { id: 'all', name: '全部通过' },
        { id: 'one', name: '至少一个通过' }
      ],
      NodeDesignates: [
        { id: 'SPECIAL_USER', name: '指定用户' },
        { id: 'ALL_USER', name: '所有人' },
        { id: 'SPECIAL_ROLE', name: '指定角色' }
      ],
      NodeRejectTypes: [
        { id: '0', name: '前一步' },
        { id: '1', name: '第一步' },
        { id: '2', name: '某一步' },
        { id: '3', name: '用户指定' },
        { id: '4', name: '不处理' }
      ],
      rules: {
        name: [{ required: true, message: '名称不能为空', trigger: 'blur' }]
      },
      activeKey: 'flow-attr'
    }
	},
	computed: {
		...mapGetters({
			currentSelectData: 'currentSelect'
		})
	},
	mounted() {
		this.currentSelect = Object.assign({}, this.currentSelectData)
		this.Compares = this.currentSelect.Compares
	},
  methods: {
		...mapActions({
			saveCurrentSelect: 'saveCurrentSelect'
		}),
    nodeNameChange(e) {
      this.currentSelect.name = e.target.value
    },
    btnAddCompare() {
      this.Compares.push({
        FieldName: '',
        Operation: '',
        Value: ''
      })
    },
    btnDelCompare(e) {
      this.Compares.splice(e, 1)
    },
    linkLabelChange(value) {
      const that = this
      const label = value

      that.currentSelect.label = label
      const conn = that.plumb.getConnections({
        source: that.currentSelect.from,
        target: that.currentSelect.to
      })[0]
      if (label !== '') {
        conn.setLabel({
          label: label,
          cssClass: 'linkLabel'
        })
      } else {
        const labelOverlay = conn.getLabelOverlay()
        if (labelOverlay) conn.removeOverlay(labelOverlay.id)
      }
    },
		usersChange(name, val) {
			// 可执行用户
			this.currentSelect.setInfo.NodeDesignateData[name] = val
		},
    rolesChange(name, val) {
			// 可执行角色
      this.currentSelect.setInfo.NodeDesignateData[name] = val
    },
    handleChangeRoles() {
      this.currentSelect.setInfo.NodeDesignateData.Texts = ''
      this.currentSelect.setInfo.NodeDesignateData.roles = []
      this.currentSelect.setInfo.NodeDesignateData.users = []
    }
  },
  watch: {
    currentSelectData: {
      deep: true,
      handler(val) {
				if(this.flag) {
					this.flag = false
					return
				}
				this.currentSelect = Object.assign({}, {...val})
        if (this.currentSelectData.type === 'sl') {
          this.activeKey = 'link-attr'
        } else if (!this.currentSelectData.type) {
          this.activeKey = 'flow-attr'
        } else {
          this.activeKey = 'node-attr'
        }
      }
		},
    currentSelect: {
      handler(val) {
        this.Compares = val.Compares
				this.saveCurrentSelect(val)
				this.flag = true
      },
      deep: true
    }
  }
}
</script>

<style lang="scss">
	@import '../style/flow-attr.scss';
	.flow-attr-box{
		.el-tabs__header{
			margin-bottom: 0;
		}
		.el-tabs__item.is-active{
			color: #409EFF !important;
		}
		.el-tabs__item.is-disabled{
			color: #333;
		}
		.el-tabs__content{
			height: calc(100% - 40px);
			overflow: auto;
			padding: 0 10px;
		}
	}
</style>
