<template>
  <div class="form-config-container">
    <el-form label-position="top">
      <el-form-item :label="'标签对齐方式'">
        <el-radio-group v-model="data.labelPosition" size="small">
          <el-radio-button label="left">左对齐</el-radio-button>
          <el-radio-button label="right">右对齐</el-radio-button>
          <el-radio-button label="top">顶对齐</el-radio-button>
        </el-radio-group>
      </el-form-item>

      <el-form-item :label="'表单标签宽度'">
        <el-input-number v-model="data.labelWidth" :min="0" :max="200" size="small" :step="10"></el-input-number>
      </el-form-item>

      <el-form-item :label="'组件大小'">
        <el-radio-group v-model="data.size" size="small">
          <el-radio-button label="medium">medium</el-radio-button>
          <el-radio-button label="small">small</el-radio-button>
          <el-radio-button label="mini">mini</el-radio-button>
        </el-radio-group>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'created-form-config',
  props: ['data']
}
</script>