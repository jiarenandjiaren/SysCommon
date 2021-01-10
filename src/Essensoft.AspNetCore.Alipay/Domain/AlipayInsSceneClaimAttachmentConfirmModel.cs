using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayInsSceneClaimAttachmentConfirmModel Data Structure.
    /// </summary>
    public class AlipayInsSceneClaimAttachmentConfirmModel : AlipayObject
    {
        /// <summary>
        /// 理赔申请报案号，通过理赔申请【alipay.ins.scene.claim.Servicely】接口的返回字段claim_report_no获取
        /// </summary>
        [JsonProperty("claim_report_no")]
        public string ClaimReportNo { get; set; }

        /// <summary>
        /// 上传的文件名清单列表，即alipay.ins.scene.claim.attachment.upload  接口中的attachment_name  用逗号(,)隔离
        /// </summary>
        [JsonProperty("upload_files")]
        public List<string> UploadFiles { get; set; }
    }
}
