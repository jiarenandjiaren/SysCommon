using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SysCommon.Repository.Core;

namespace SysCommon.Repository.Domain
{
    /// <summary>
    /// 模块元素表(需要权限控制的按钮)
    /// </summary>
    [Table("Element")]
    public partial class Element : BaseEntity
    {
        public Element()
        {
            this.DomId = string.Empty;
            this.Name = string.Empty;
            this.Script = string.Empty;
            this.Icon = string.Empty;
            this.Class = string.Empty;
        }

        /// <summary>
        /// DOM ID
        /// </summary>
        [Description("DOM ID")]
        public string DomId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 元素调用脚本
        /// </summary>
        [Description("元素调用脚本")]
        public string Script { get; set; }
        /// <summary>
        /// 元素图标
        /// </summary>
        [Description("元素图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 元素样式
        /// </summary>
        [Description("元素样式")]
        public string Class { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        [Description("功能栏目Id")]
        public string MenuId { get; set; }
    }
}