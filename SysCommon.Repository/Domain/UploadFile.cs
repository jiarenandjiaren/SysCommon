// <copyright file="UploadFile.cs" company="SysCommon.me">
// Copyright (c) 2019 SysCommon.me. All rights reserved.
// </copyright>
// <author>www.cnblogs.com/yubaolee</author>
// <date>2019-03-07</date>
// <summary>附加实体</summary>

using System;
using System.ComponentModel.DataAnnotations.Schema;
using SysCommon.Repository.Core;

namespace SysCommon.Repository.Domain
{
    /// <summary>
	/// 文件
	/// </summary>
    [Table("UploadFile")]
    public partial class UploadFile : BaseEntity
    {
        public UploadFile()
        {
          this.FileName= string.Empty;
          this.FilePath= string.Empty;
          this.Description= string.Empty;
          this.FileType= string.Empty;
          this.Extension= string.Empty;
          this.CreateTime= DateTime.Now;
          this.Thumbnail= string.Empty;
          this.BelongApp= string.Empty;
          this.BelongAppId= string.Empty;
        }

        /// <summary>
	    /// 文件名称
	    /// </summary>
        public string FileName { get; set; }
        /// <summary>
	    /// 文件路径
	    /// </summary>
        public string FilePath { get; set; }
        /// <summary>
	    /// 文件类型
	    /// </summary>
        public string FileType { get; set; }
        /// <summary>
	    /// 文件大小
	    /// </summary>
        public long FileSize { get; set; }
        /// <summary>
	    /// 扩展名称
	    /// </summary>
        public string Extension { get; set; }
        /// <summary>
	    /// 缩略图
	    /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
	    /// 所属应用
	    /// </summary>
        public string BelongApp { get; set; }
        /// <summary>
	    /// 所属应用ID
	    /// </summary>
        public string BelongAppId { get; set; }

    }
}