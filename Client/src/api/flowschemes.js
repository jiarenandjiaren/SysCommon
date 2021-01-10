import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/flowschemes/load',
    method: 'post',
    params
  })
}

export function get(params) {
  return request({
    url: '/flowschemes/get',
    method: 'post',
    params
  })
}

export function add(data) {
  return request({
    url: '/flowschemes/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/flowschemes/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/flowschemes/delete',
    method: 'post',
    data
  })
}

