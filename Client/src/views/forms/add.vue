<template>
  <form-detail :is-edit='false'></form-detail>
  <!-- <FormContainer></FormContainer> -->
</template>

<script>
import FormDetail from './components/formDetail'
// import { KFormDesign } from '@/packages/index'
// import FormContainer from '@/formcreated/index'

export default {
  name: 'addForm',
  components: { FormDetail },
  // components: { FormContainer },
  methods: {
    handleSave() {}
  }
}
</script>

