/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： SaveModel
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 14:52   N/A    初版
*
* Copyright (c) 2020 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.DomainModels
{
    public class SaveModel
    {
        public Dictionary<string, object> MainData { get; set; }
        public List<Dictionary<string, object>> DetailData { get; set; }
        public List<object> DelKeys { get; set; }

        /// <summary>
        /// 从前台传入的其他参数(自定义扩展可以使用)
        /// </summary>
        public object Extra { get; set; }
    }
}
