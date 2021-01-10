<template>
  <div class="created-form-content">
    <div class="form-model" v-if="isDisabled"></div>
    <el-form ref="generateForm" 
      label-suffix=":"
      :size="data.config.size"
      :model="models" :rules="rules" :label-position="data.config.labelPosition" :label-width="`${data.config.labelWidth}px`">
      <template v-for="item in data.list">
        <template v-if="item.type == 'grid'">
          <el-row
            :key="item.key"
            type="flex"
            :gutter="item.options.gutter ? item.options.gutter : 0"
            :justify="item.options.justify"
            :align="item.options.align"
          >
            <el-col v-for="(col, colIndex) in item.columns" :key="colIndex" :span="col.span">
              <template v-for="citem in col.list" >
                <el-form-item v-if="citem.type=='blank'" :label="citem.name" :prop="citem.model" :key="citem.key">
                  <slot :name="citem.model" :model="models"></slot>
                </el-form-item>
                <CreatedFormItem v-else 
                  :key="citem.key" 
                  :models.sync="models" 
                  :remote="remote" 
                  :rules="rules" 
                  :formData="citem">
                </CreatedFormItem>
              </template>
            </el-col>
          </el-row>
        </template>
        <template v-else-if="item.type == 'blank'">
          <el-form-item :label="item.name" :prop="item.model" :key="item.key">
            <slot :name="item.model" :model="models"></slot>
          </el-form-item>
        </template>
        <template v-else>
          <CreatedFormItem 
            :key="item.key" 
            :models.sync="models" 
            :rules="rules" 
            :formData="item"
            :remote="remote">
          </CreatedFormItem>
        </template>
      </template>
    </el-form>
  </div>
</template>

<script>
import CreatedFormItem from './CreatedFormItem'
export default {
  name: 'fm-generate-form',
  components: {
    CreatedFormItem
  },
  props: ['data', 'remote', 'value', 'insite', 'isDisabled'],
  data() {
    return {
      models: {},
      rules: {}
    }
  },
  created() {
    this.generateModle(this.data.list)
  },
  mounted() {
  },
  methods: {
    generateModle(arr) {
      arr.length > 0 && arr.forEach(arrItem => {
        if (arrItem.type === 'grid') {
          arrItem.columns.forEach(item => {
            this.generateModle(item.list)
          })
        } else {
          if (this.value && Object.keys(this.value).indexOf(arrItem.model) >= 0) {
            this.models[arrItem.model] = this.value[arrItem.model]
          } else {
            if (arrItem.type === 'blank') {
              this.$set(this.models, arrItem.model, arrItem.options.defaultType === 'String' ? '' : (arrItem.options.defaultType === 'Object' ? {} : []))
            } else {
              this.models[arrItem.model] = arrItem.options.defaultValue
            }
          }

          if (this.rules[arrItem.model]) {
            this.rules[arrItem.model] = [...this.rules[arrItem.model], ...arrItem.rules.map(item => {
              if (item.pattern) {
                return { ...item, pattern: JSON.stringify(item.pattern) }
              } else {
                return { ...item }
              }
            })]
          } else {
            this.rules[arrItem.model] = [...arrItem.rules.map(item => {
              if (item.pattern) {
                return { ...item, pattern: JSON.stringify(item.pattern) }
              } else {
                return { ...item }
              }
            })]
          }
        }
      })
    },
    getData() {
      return new Promise((resolve, reject) => {
        this.$refs.generateForm.validate(valid => {
          if (valid) {
            resolve(this.models)
          } else {
            reject(new Error(this.$t('fm.message.validError')).message)
          }
        })
      })
    },
    reset() {
      this.$refs.generateForm.resetFields()
    },
    onInputChange(value, field) {
      console.log(value, field)
      // this.$emit('on-change', field, value, this.models)
    }
  },
  watch: {
    data: {
      deep: true,
      handler(val) {
        this.generateModle(val.list)
      }
    },
    value: {
      deep: true,
      handler(val) {
        this.models = { ...this.models, ...val }
      }
    }
  }
}
</script>

<style lang="scss">
.created-form-content{
  position: relative;
  .form-model{
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    z-index: 1;
  }
}
</style>