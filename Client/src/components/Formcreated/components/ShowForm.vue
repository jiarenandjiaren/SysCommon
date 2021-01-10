<template>
  <div class="created-form-container" style="height:100%;">
    <div v-if="data.list.length == 0" class="form-empty">从顶部拖拽来添加字段</div>
    <el-form style="height:calc(100% - 10px)" :size="data.config.size" label-suffix=":" :label-position="data.config.labelPosition" :label-width="data.config.labelWidth + 'px'">
      <draggable class="" 
        v-model="data.list" 
        v-bind="{group:'people', ghostClass: 'ghost',animation: 200, handle: '.drag-form'}"
        @end="handleMoveEnd"
        @add="handleAddFormItem"
        style="height:100%;"
      >
        <transition-group name="fade" tag="div" class="created-form-list" style="min-height:calc(100% - 10px);">
          <template v-for="(element, index) in data.list">
            <template v-if="element.type == 'grid'">
                <el-row class="created-col created-view" v-if="element && element.key" :key="element.key" 
                  type="flex"
                  :class="{active: selectFormData.key == element.key}"
                  :gutter="element.options.gutter ? element.options.gutter : 0"
                  :justify="element.options.justify"
                  :align="element.options.align"
                  @click.native="handleSelectForm(index)">
                  <el-col  v-for="(col, colIndex) in element.columns" :key="colIndex" :span="col.span ? col.span : 0">
                      <draggable
                        v-model="col.list"
                        :no-transition-on-drag="true"
                        v-bind="{group:'people', ghostClass: 'ghost',animation: 200, handle: '.drag-form'}"
                        @end="handleMoveEnd"
                        @add="handleAddFormCol($event, element, colIndex)"
                      >
                        <transition-group name="fade" tag="div" class="created-col-list">
                          <template v-for="(el, i) in col.list">
                            <ShowFormItem
                              :key="el.key"
                              v-if="el.key"
                              :element="el"
                              :select.sync="selectFormData" 
                              :index="i" 
                              :data="col">
                            </ShowFormItem>
                          </template>
                        </transition-group>
                        
                      </draggable>
                  </el-col>
                  <div class="created-view-action created-col-action" v-if="selectFormData.key == element.key">
                    <i class="el-icon-delete" title="删除" @click.stop="handleDeleteFormItem(index)"></i>
                  </div>

                  <div class="created-view-drag created-col-drag" v-if="selectFormData.key == element.key">
                    <i class="el-icon-sort drag-form"></i>
                  </div>
                </el-row>
            </template>
            <template v-else>
              <!-- <span :key="element.key">{{element}}></span> -->
              <ShowFormItem v-if="element && element.key"  :key="element.key" :element="element" :select.sync="selectFormData" :index="index" :data="data"></ShowFormItem>
            </template>
          </template>
        </transition-group>
      </draggable>
    </el-form>
  </div>
</template>

<script>
import Draggable from 'vuedraggable'
import ShowFormItem from './ShowFormItem'
export default {
  components: {
    Draggable,
    ShowFormItem
  },
  props: ['data', 'select'],
  data() {
    return {
      selectFormData: this.select
    }
  },
  mounted() {
    console.log('data>>>>>>>>>', this.data)
    document.body.ondrop = function(event) {
      const isFirefox = navigator.userAgent.toLowerCase().indexOf('firefox') > -1
      if (isFirefox) {
        event.preventDefault()
        event.stopPropagation()
      }
    }
  },
  methods: {
    handleMoveEnd() {
    },
    handleSelectForm(index) {
      this.selectFormData = this.data.list[index]
    },
    handleAddFormItem(evt) {
      const newIndex = evt.newIndex
      // const to = evt.to

      // 为拖拽到容器的元素添加唯一 key
      const key = Date.parse(new Date()) + '_' + Math.ceil(Math.random() * 99999)
      this.$set(this.data.list, newIndex, {
        ...this.data.list[newIndex],
        options: {
          ...this.data.list[newIndex].options,
          remoteFunc: 'func_' + key
        },
        key,
        // 绑定键值
        model: this.data.list[newIndex].type + '_' + key,
        rules: []
      })
      if (this.data.list[newIndex].type === 'radio' || this.data.list[newIndex].type === 'checkbox' || this.data.list[newIndex].type === 'select') {
        this.$set(this.data.list, newIndex, {
          ...this.data.list[newIndex],
          options: {
            ...this.data.list[newIndex].options,
            options: this.data.list[newIndex].options.options.map(item => ({
              ...item
            }))
          }
        })
      }
      if (this.data.list[newIndex].type === 'grid') {
        this.$set(this.data.list, newIndex, {
          ...this.data.list[newIndex],
          columns: this.data.list[newIndex].columns.map(item => ({ ...item }))
        })
      }
      this.selectFormData = this.data.list[newIndex]
    },
    handleAddFormCol($event, row, colIndex) {
      const newIndex = $event.newIndex
      const oldIndex = $event.oldIndex
      const item = $event.item
      // 防止布局元素的嵌套拖拽
      if (item.className.indexOf('data-grid') >= 0) {
        // 如果是列表中拖拽的元素需要还原到原来位置
        item.tagName === 'DIV' && this.data.list.splice(oldIndex, 0, row.columns[colIndex].list[newIndex])
        row.columns[colIndex].list.splice(newIndex, 1)
        return false
      }
      const key = Date.parse(new Date()) + '_' + Math.ceil(Math.random() * 99999)
      this.$set(row.columns[colIndex].list, newIndex, {
        ...row.columns[colIndex].list[newIndex],
        options: {
          ...row.columns[colIndex].list[newIndex].options,
          remoteFunc: 'func_' + key
        },
        key,
        // 绑定键值
        model: row.columns[colIndex].list[newIndex].type + '_' + key,
        rules: []
      })
      if (row.columns[colIndex].list[newIndex].type === 'radio' || row.columns[colIndex].list[newIndex].type === 'checkbox' || row.columns[colIndex].list[newIndex].type === 'select') {
        this.$set(row.columns[colIndex].list, newIndex, {
          ...row.columns[colIndex].list[newIndex],
          options: {
            ...row.columns[colIndex].list[newIndex].options,
            options: row.columns[colIndex].list[newIndex].options.options.map(item => ({
              ...item
            }))
          }
        })
      }
      this.selectFormData = row.columns[colIndex].list[newIndex]
    },
    handleDeleteFormItem(index) {
      if (this.data.list.length - 1 === index) {
        if (index === 0) {
          this.selectFormData = {}
        } else {
          this.selectFormData = this.data.list[index - 1]
        }
      } else {
        this.selectFormData = this.data.list[index + 1]
      }
      this.$nextTick(() => {
        this.data.list.splice(index, 1)
      })
    }
  },
  watch: {
    select(val) {
      this.selectFormData = val
    },
    selectFormData: {
      handler(val) {
        this.$emit('update:select', val)
      },
      deep: true
    }
  }
}
</script>