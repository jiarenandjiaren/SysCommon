using System;
using System.ComponentModel.DataAnnotations;

namespace SysCommon.Service.SSO
{
    public class PassportLoginRequest
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        //[Display(Name = "验证码标记")]
        //[Required(ErrorMessage ="验证码标记不能为空")]
        public string Uuid { get; set; }
        //[Display(Name = "验证码")]
        //[Required(ErrorMessage ="验证码不能为空")]
        public string VerificationCode { get; set; }

        //public string AppKey { get; set; }

        //public void Trim()
        //{
        //    //if (string.IsNullOrEmpty(UserName))
        //    //{
        //    //    throw new Exception("用户名不能为空");
        //    //}

        //    //if (string.IsNullOrEmpty(Password))
        //    //{
        //    //    throw new Exception("密码不能为空");
        //    //}
        //    //UserName = UserName.Trim();
        //    //Password = Password.Trim();
        //    ////if(!string.IsNullOrEmpty(AppKey)) AppKey = AppKey.Trim();
        //}
    }
}