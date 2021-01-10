using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SysCommon.Repository.Core;

namespace SysCommon.Repository.Domain
{
    /// <summary>
	/// 栏目管理
	/// </summary>
      [Table("Menu")]
    public partial class Menu : BaseEntity
    {
        public Menu()
        {
            //this.FatherId = string.Empty;
            //this.MenuName = string.Empty;
            //this.URL = string.Empty;
            //this.TemplateURL = string.Empty;
            //this.BackstageMenuUrl = string.Empty;
            //this.FrontMenuUrl = string.Empty;
            //this.FrontArticleUrl = string.Empty;
            //this.IconName = string.Empty;
        }

        [Description("父级栏目Id")]
        public string FatherId { get; set; }
        [Description("栏目名称")]
        public string MenuName { get; set; }
        [Description("URL")]
        public string URL { get; set; }
        [Description("模板路径")]
        public string TemplateURL { get; set; }
        [Description("后台栏目模板")]
        public string BackstageMenuUrl { get; set; }
        [Description("前台栏目模板")]
        public string FrontMenuUrl { get; set; }
        [Description("前台文章模板")]
        public string FrontArticleUrl { get; set; }
        [Description("图标名称")]
        public string IconName { get; set; }
        

    }
}