using System.Collections.Generic;
using Infrastructure;
using SysCommon.Repository.Domain;
using SysCommon.Service.Request;

namespace SysCommon.Service.Response
{
    public class MenuView: BaseViewReq
    {
        /// <summary>
        /// 父级栏目Id
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
        /// 模板路径
        /// </summary>
        public string TemplateURL { get; set; }
        /// <summary>
        /// 后台栏目模板
        /// </summary>
        public string BackstageMenuUrl { get; set; }
        /// <summary>
        /// 前台栏目模板
        /// </summary>
        public string FrontMenuUrl { get; set; }
        /// <summary>
        /// 前台文章模板
        /// </summary>
        public string FrontArticleUrl { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string IconName { get; set; }
        

        /// <summary>
        /// 模块中的元素
        /// </summary>
        public List<Element> Elements { get; set; }

        public static implicit operator MenuView(Menu module)
        {
            return module.MapTo<MenuView>();
        }

        public static implicit operator Menu(MenuView view)
        {
            return view.MapTo<Menu>();
        }
    }
}