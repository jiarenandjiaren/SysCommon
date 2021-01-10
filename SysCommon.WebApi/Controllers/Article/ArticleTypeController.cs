/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章类型操作
* 类 名： ArticleTypeController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/11/30 9:30   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/

using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service;
using SysCommon.Service.Interface;
using SysCommon.Service.Response;
using SysCommon.Service.SSO;
using System;
using Microsoft.Extensions.Logging;
using SysCommon.Service.Request;
using System.Collections.Generic;
using SysCommon.Repository.Domain;
using Infrastructure.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Infrastructure.Extensions;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 文章类型操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleTypeController : ControllerBase
    {
        private ILogger _logger;
        public readonly ArticleTypeService _articleTypeService;

        public ArticleTypeController(ILogger<ArticleTypeController> logger, ArticleTypeService articleTypeService)
        {
            _logger = logger;
            _articleTypeService = articleTypeService;
        }
        #region 添加
        /// <summary>
        /// 添加文章类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public WebResponseContent<AArticleTypeReq> Add([FromBody] AArticleTypeReq request)
        {
            var result = new WebResponseContent<AArticleTypeReq>();
            try
            {
                _articleTypeService.Add(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 修改
        /// <summary>
        /// 修改文章类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public WebResponseContent<EArticleTypeReq> Update([FromBody] EArticleTypeReq request)
        {
            var result = new WebResponseContent<EArticleTypeReq>();
            try
            {
                _articleTypeService.Edit(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除文章类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public WebResponseContent Delete([FromBody] QueryOneReq request)
        {
            var result = new WebResponseContent();
            try
            {
                _articleTypeService.Delete(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 获取文章列表 
        /// <summary>
        /// 获取文章列表
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public TableData Show([FromBody] QueryArticleTypeListReq request)
        {
            return _articleTypeService.Show(request);
        }
        #endregion
        #region 展示单条数据
        /// <summary>
        /// 展示单条数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        [AllowAnonymous]
        [HttpPost]
        public TableData GetDataOne([FromBody] QueryOneReq request)
        {
            return _articleTypeService.GetDataOne(request);
        }
        #endregion
        #region 展示在用所有数据
        /// <summary>
        /// 展示在用所有数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        [AllowAnonymous]
        [HttpPost]
        public TableData ShowAll()
        {
            return _articleTypeService.ShowAll();
        }
        #endregion

        #region 展示类型及对应标签
        /// <summary>
        /// 展示类型及对应标签
        /// </summary>
        /// <param name="updateAdminReq"></param>
        [AllowAnonymous]
        [HttpPost]
        public TableData ShowTypeLabel()
        {
            return _articleTypeService.ShowTypeLabel();
        }
        #endregion
    }
}