using Infrastructure;

namespace SysCommon.Service.SSO
{
    public class LoginResult : Response<object>
    {
        public string ReturnUrl;
        public string Token;
    }
}