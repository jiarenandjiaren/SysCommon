import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/roles/load',
    method: 'get',
    params
  })
}

export function loadForUser(userId) {
  return request({
    url: '/roles/loadforuser',
    method: 'get',
    params: { userId: userId }
  })
}

export function add(data) {
  return request({
    url: '/roles/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/roles/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/roles/delete',
    method: 'post',
    data
  })
}
// 添加用户
export function AssignRoleUsers(data) {
  return request({
    url: '/AccessObjs/AssignRoleUsers',
    method: 'post',
    data
  })
}

