import Vue from 'vue'
import Vuex from 'vuex'
import app from './modules/app'
import serverConf from './modules/serverConf'
import user from './modules/user'
import tagsView from './modules/tagsView'
import permission from './modules/permission'
import dataPrivilegerules from './modules/dataPrivilegerules'
import storage from './modules/storage'
import flow from './modules/flow'
import getters from './getters'
import { vuexOidcCreateStoreModule } from 'vuex-oidc'

Vue.use(Vuex)
// console.log(JSON.parse(process.env.VUE_APP_OIDC), process)
const store = new Vuex.Store({
  modules: {
    app,
    user,
    serverConf,
    permission,
    dataPrivilegerules,
    storage,
    tagsView,
    flow,
    oidcStore: vuexOidcCreateStoreModule(
      // process.env.OIDC,
      // process.env.VUE_APP_OIDC,
      {
        authority: '"http://localhost:12796"',
        clientId: '"WMS"',
        redirectUri: '"http://localhost:1803/#/oidc-callback"',
        postLogoutRedirectUri:'"http://localhost:1803"',
        responseType: '"code"',
        scope: '"openid profile openauthapi"',
        automaticSilentRenew: true,
        silentRedirectUri: '"http://localhost:1803/silent-renew-oidc.html"'
      },
      // Optional OIDC store settings
      {
        namespaced: false,
        dispatchEventsOnWindow: true
      },
      // Optional OIDC event listeners
      {
        userLoaded: (user) => console.log('OIDC user is loaded:', user),
        userUnloaded: () => console.log('OIDC user is unloaded'),
        accessTokenExpiring: () => console.log('Access token will expire'),
        accessTokenExpired: () => console.log('Access token did expire'),
        silentRenewError: () => console.log('OIDC user is unloaded'),
        userSignedOut: () => console.log('OIDC user is signed out')
      }
    )
  },
  getters
})

export default store
