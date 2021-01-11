using SysCommon.Repository.Domain;

namespace SysCommon.App.Response
{
    public class FlowVerificationResp :FlowInstance
    {
        /// <summary>
        /// 预览表单数据
        /// </summary>
        /// <value>The FRM data HTML.</value>
        public string FrmPreviewHtml
        {
            get { return FormUtil.Preview(this); }
        }
    }
}
