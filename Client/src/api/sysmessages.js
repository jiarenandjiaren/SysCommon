import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/sysMessages/load',
    method: 'get',
    params
  })
}

export function loadForRole(roleId) {
  return request({
    url: '/sysMessages/loadForRole',
    method: 'get',
    params: { appId: '', firstId: roleId }
  })
}

export function add(data) {
  return request({
    url: '/sysMessages/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/sysMessages/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/sysMessages/delete',
    method: 'post',
    data
  })
}

