using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayUserFinanceinfoShareResponse.
    /// </summary>
    public class AlipayUserFinanceinfoShareResponse : AlipayResponse
    {
        /// <summary>
        /// 查询出的信用卡列表，包含0到多张卡，每张卡对应一组信息，包含卡号（已脱敏）和开户行代码
        /// </summary>
        [JsonProperty("credit_card_list")]
        public List<AlipayUserCreditCard> CreditCardList { get; set; }
    }
}
