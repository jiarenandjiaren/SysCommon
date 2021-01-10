/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 常用提示信息
* 类 名： ApiMessage
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.Enum
{
    public struct ApiMessage
    {
        public const string IdentityAuth = "接口启动了OAuth认证,暂时不能使用该方式登录!";
        public const string VerificationCode = "验证码失效！";
        public const string VerificationUuId = "验证码不正确！";
        public const string ExistUser = "用户不存在！"; 
        public const string IsDeleteUser = "用户已经被删除！";
        /// <summary>
        /// 参数有误
        /// </summary>
        public const string ParameterError = "请求参数不正确!";
        /// <summary>
        /// 没有配置好输入参数
        /// </summary>
        public const string NotInputEntity = "没有配置好输入参数!";
        /// <summary>
        /// token丢失
        /// </summary>
        public const string TokenLose = "认证失败，请提供认证信息!";
        
        /// <summary>
        /// 版本号不能为空
        /// </summary>

        public const string VersionEmpty = "版本号不能为空!";
        /// <summary>
        /// content不能为空
        /// </summary>

        public const string ContentEmpty = "biz_content不能为空!";
        /// <summary>
        /// content不能为空
        /// </summary>
        public const string TokenError = "token不正确";

        public const string AccountLocked = "帐号已被锁定!";

        public const string PhoneNoInvalid = "输入的不是手机号";


        public const string PINTypeNotRange = "获取验证的类型不正确";
        public const string OperToBusy = "操作太频繁，请稍后再试";

        public const string SendSTKError = "短信发送异常,请稍后再试";
        public const string SendSTKSuccess = "短信发送成功";
        public const string STKNotSend = "请先获取验证码";
        public const string AccountExists = "手机号已经被注册";

        public const string AccountNotExists = "手机号没有注册";

        public const string PINExpire = "验证码已过期,请重新获取";

        public const string PINError = "验证码不正确";

        public const string ParameterEmpty = "参数不能为空";
    }
}
