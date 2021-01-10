import Vue from 'vue'
import layer from 'vue-layer'

import 'normalize.css/normalize.css'// A modern alternative to CSS resets

import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import locale from 'element-ui/lib/locale/lang/zh-CN' // lang i18n
import VueContextMenu from 'vue-contextmenu'

import '@/styles/index.scss' // global css

import App from './App'
import router from './router'
import store from './store'

import '@/icons' // icon
import '@/permission' // permission control
import '@/assets/public/css/iconfont/iconfont.css'
import '@/assets/public/css/iconfont/iconfont.js'
import '@/assets/public/css/comIconfont/iconfont/iconfont.css'
import '@/assets/public/css/comIconfont/iconfont/iconfont.js'

// 请假条表单和详情
import FrmLeaveReqAdd from '@/views/forms/userDefine/frmLeaveReq/add'
import FrmLeaveReqDetail from '@/views/forms/userDefine/frmLeaveReq/detail'

// import '../public/ueditor/ueditor.config.js'
// import '../public/ueditor/ueditor.all.min.js'
// import '../public/ueditor/lang/zh-cn/zh-cn.js'
// import '../public/ueditor/ueditor.parse.min.js'
// import '../public/ueditor/formdesign/leipi.formdesign.v4.js'

Vue.use(ElementUI, { locale })
Vue.use(VueContextMenu)

Vue.config.productionTip = false
Vue.prototype.$layer = layer(Vue, {
  msgtime: 3
})
Vue.component('FrmLeaveReqAdd', FrmLeaveReqAdd)
Vue.component('FrmLeaveReqDetail', FrmLeaveReqDetail)
new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
