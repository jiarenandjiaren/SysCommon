import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/wmsInboundOrderTbls/load',
    method: 'get',
    params
  })
}

export function loadForRole(roleId) {
  return request({
    url: '/wmsInboundOrderTbls/loadForRole',
    method: 'get',
    params: { appId: '', firstId: roleId }
  })
}

export function add(data) {
  return request({
    url: '/wmsInboundOrderTbls/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/wmsInboundOrderTbls/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/wmsInboundOrderTbls/delete',
    method: 'post',
    data
  })
}

