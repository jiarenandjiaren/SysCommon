/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文件上传
* 类 名： FilesController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 13:05   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SysCommon.Service;
using SysCommon.Repository.Domain;
using Microsoft.AspNetCore.Hosting;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController :ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private FileService _fileService;
        private ILogger _logger;

        public FilesController(FileService fileService,ILogger<FilesController> logger, IHostingEnvironment hostingEnvironment)
        {
            _fileService = fileService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 批量上传文件接口
        /// <summary>
        ///  批量上传文件接口
        /// </summary>
        /// <param name="files"></param>
        /// <returns>服务器存储的文件信息</returns>
        [HttpPost]
        [AllowAnonymous]
        public WebResponseContent<IList<UploadFile>> Upload(IFormFileCollection files)
        {
            var result = new WebResponseContent<IList<UploadFile>>();
            try
            {
                //_app.Add(files);
                result.Data = _fileService.Add(files);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion
        #region 展示指定目录文件名称
        /// <summary>
        /// 展示指定目录文件名称
        /// </summary>
        /// <param name="testDir"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<ArrayList> Show([FromBody] string testDir)
        {
            var result = new WebResponseContent<ArrayList>();
            try
            {
                string path = _hostingEnvironment.ContentRootPath + "/" + testDir;
                ArrayList arrayList = FileHelper.getallfilesbyfolder(path, true);
                // string testDir = "D:/IIS/ht/qiantaimoban";
                result.Data = arrayList;
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion 
        #region 批量上传文件接口
        /// <summary>
        ///  删除文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns>服务器存储的文件信息</returns>
        [HttpPost]
        [AllowAnonymous]
        public WebResponseContent DeleteFile(string filePath)
        {
            var result = new WebResponseContent();
            try
            {
                _fileService.DeleteFile(filePath);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion
    }
}
