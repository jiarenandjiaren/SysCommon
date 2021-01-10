/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： AMenuReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:32   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure;
using SysCommon.Service.Interface;
using SysCommon.Repository.Core;
using SysCommon.Repository.Domain;

namespace SysCommon.Service.Request
{
    public class AMenuReq : BaseAEntityReq
    {
        public AMenuReq()
        {
            //this.FatherId = string.Empty;
            //this.MenuName = string.Empty;
            //this.URL = string.Empty;
            //this.TemplateURL = string.Empty;
            //this.BackstageMenuUrl = string.Empty;
            //this.FrontMenuUrl = string.Empty;
            //this.FrontArticleUrl = string.Empty;
        }
        /// <summary>
        /// 上级Id
        /// </summary>
        public string FatherId { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// URL 
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public string TemplateURL { get; set; }
        /// <summary>
        /// 后面栏目URL
        /// </summary>
        public string BackstageMenuUrl { get; set; }
        /// <summary>
        /// 前台栏目URL
        /// </summary>
        public string FrontMenuUrl { get; set; }
        /// <summary>
        /// 前台文章URL
        /// </summary>
        public string FrontArticleUrl { get; set; }
        public static implicit operator AMenuReq(Menu user)
        {
            return user.MapTo<AMenuReq>();
        }
        public static implicit operator Menu(AMenuReq view)
        {
            return view.MapTo<Menu>();
        }
    }
}