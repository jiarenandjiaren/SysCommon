using System;
namespace Infrastructure.PayConfig
{
	public class AliConfig
	{
		// 应用ID,您的APPID
		public static string AppId = "2021000119613616";
		/// <summary>
		/// 合作商户uid
		/// </summary>
		public static string Uid = "";

		// 支付宝网关
		public static string Gatewayurl = "https://openapi.alipaydev.com/gateway.do";

		// 商户私钥，您的原始格式RSA私钥
		public static string PrivateKey = "";

		// 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
		public static string AlipayPublicKey = "";

		// 签名方式
		public static string SignType = "RSA2";

		// 编码格式
		public static string CharSet = "UTF-8";
	}
	public class Common
	{
        /**
       * 根据当前系统时间加随机序列来生成订单号
        * @return 订单号
       */
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", AliConfig.AppId, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
