using System.Collections.Generic;
using Infrastructure;
using SysCommon.Repository.Domain;
using SysCommon.Service.Request;

namespace SysCommon.Service.Response
{
    public class SysOrgView : BaseViewReq
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public string FatherId { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 组织图标
        /// </summary>
        public string IconName { get; set; }

        public static implicit operator SysOrgView(SysOrg module)
        {
            return module.MapTo<SysOrgView>();
        }

        public static implicit operator SysOrg(SysOrgView view)
        {
            return view.MapTo<SysOrg>();
        }
    }
}