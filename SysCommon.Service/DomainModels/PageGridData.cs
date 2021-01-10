/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： PageGridData
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 14:48   N/A    初版
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
    public class PageGridData<T>
    {
        public int status { get; set; }
        public string msg { get; set; }
        public int total { get; set; }
        public List<T> rows { get; set; }
        public object summary { get; set; }
        /// <summary>
        /// 可以在返回前，再返回一些额外的数据，比如返回其他表的信息，前台找到查询后的方法取出来
        /// </summary>
        public object extra { get; set; }
    }
}
