// ***********************************************************************
// Assembly         : FundationAdmin
// Author           : yubaolee
// Created          : 03-09-2016
//
// Last Modified By : yubaolee
// Last Modified On : 03-09-2016
// ***********************************************************************
// <copyright file="TableData.cs" company="Microsoft">
//     版权所有(C) Microsoft 2015
// </copyright>
// <summary>layui datatable数据返回</summary>
// ***********************************************************************

using System.Collections.Generic;
using Infrastructure;

namespace SysCommon.Service.Response
{
    /// <summary>
    /// table的返回数据
    /// </summary>
    public class TableData
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code;
        /// <summary>
        /// 操作消息
        /// </summary>
        public string message;

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int count;

        /// <summary>
        ///  返回的列表头信息
        /// </summary>
        public List<KeyDescription> columnHeaders;

        /// <summary>
        /// 数据内容
        /// </summary>
        public object data;

        public TableData()
        {
            Code = 100;
            message = "加载成功";
            columnHeaders = new List<KeyDescription>();
        }
    }
    public class CheckData
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code;
        /// <summary>
        /// 操作消息
        /// </summary>
        public string message;
        /// <summary>
        /// 数据内容
        /// </summary>
        public dynamic data;
        public CheckData()
        {
            code = 100;
            message = "加载成功";
        }

    }
    /// <summary>
    /// 有关模板数据的字段
    /// </summary>
    public class CheckDataCommon
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code;
        /// <summary>
        /// 操作消息
        /// </summary>
        public string message;
        /// <summary>
        /// 数据内容
        /// </summary>
        public dynamic data;
        public dynamic data1;
        public CheckDataCommon()
        {
            code = 100;
            message = "加载成功";
        }

    }
}