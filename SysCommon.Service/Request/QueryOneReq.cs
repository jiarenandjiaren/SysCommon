using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SysCommon.Service.Request
{
    public class QueryOneReq
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Display(Name = "数据id")]
        [Required(ErrorMessage = "数据id不能为空")]
        public string Id { get; set; }
    }
    public class QueryCheckReq
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Display(Name = "数据id")]
        [Required(ErrorMessage = "数据id不能为空")]
        public string Id { get; set; }
        /// <summary>
        /// 审核（0：未审核   1：通过   2：不通过）
        /// </summary>
        [Display(Name = "审核（0：未审核   1：通过   2：不通过）")]
        [Required(ErrorMessage = "审核意见不能为空")]
        public int Check { get; set; }
    }
}
