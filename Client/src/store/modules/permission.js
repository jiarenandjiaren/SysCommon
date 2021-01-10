import { constantRouterMap } from '@/router'
import Layout from '@/views/layout/Layout'
import EmptyLayout from '@/views/layout/EmptyLayout'
// import { changeIcon } from '@/utils'
const groupRoutes = (data) => {
  const parentPath = data.item.url
  var newPath = {
    path: parentPath || '/',
    // component: data.children && data.children.length > 0 ? Layout : () => import('@/views' + data.item.url.toLowerCase()),
    component: data.children && data.children.length > 0 ? (data.item && data.item.parentId === null ? Layout : EmptyLayout) : () => import('@/views' + data.item.url.toLowerCase()),
    meta: {
      title: data.item.name,
      sortNo: data.item.sortNo,
      // icon: changeIcon(data.item.iconName),
      icon: data.item.iconName || 'streamlist',
      elements: data.item && data.item.elements || ''
    },
    name: data.item.name,
    hidden: false,
    children: []
  }
  if (data.children && data.children.length > 0) {
    data.children.forEach(child => {
      newPath.children.push(groupRoutes(child))
    })
  }
  return newPath
}
const permission = {
  state: {
    routers: constantRouterMap,
    addRouters: []
  },
  mutations: {
    SET_ROUTERS: (state, routers) => {
      state.addRouters = routers
      state.routers = constantRouterMap.concat(routers)
    }
  },
  actions: {

    GenerateRoutes({ commit }, data) {
      return new Promise(resolve => {
        (async() => {
          const newPaths = []
          await data.modules.forEach((value) => {
            newPaths.push(groupRoutes(value))
          })
          commit('SET_ROUTERS', newPaths)
          resolve(newPaths)
        })()
      })
    }
  }
}

export default permission
