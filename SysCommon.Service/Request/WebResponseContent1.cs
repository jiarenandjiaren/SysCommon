using Infrastructure.Enums;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.Request
{
    public class WebResponseContent1
    {
        public WebResponseContent1()
        {
        }
        public WebResponseContent1(bool status)
        {
            this.Status = status;
        }
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        //public string Message { get; set; }
        public object Data { get; set; }

        public WebResponseContent1 OK()
        {
            this.Status = true;
            return this;
        }

        public static WebResponseContent1 Instance
        {
            get { return new WebResponseContent1(); }
        }
        public WebResponseContent1 OK(string message = null, object data = null)
        {
            this.Status = true;
            this.Message = message;
            this.Data = data;
            return this;
        }
        public WebResponseContent1 OK(ResponseType responseType)
        {
            return Set(responseType, true);
        }
        public WebResponseContent1 Error(string message = null)
        {
            this.Status = false;
            this.Message = message;
            return this;
        }
        public WebResponseContent1 Error(ResponseType responseType)
        {
            return Set(responseType, false);
        }
        public WebResponseContent1 Set(ResponseType responseType)
        {
            bool? b = null;
            return this.Set(responseType, b);
        }
        public WebResponseContent1 Set(ResponseType responseType, bool? status)
        {
            return this.Set(responseType, null, status);
        }
        public WebResponseContent1 Set(ResponseType responseType, string msg)
        {
            bool? b = null;
            return this.Set(responseType, msg, b);
        }
        public WebResponseContent1 Set(ResponseType responseType, string msg, bool? status)
        {
            if (status != null)
            {
                this.Status = (bool)status;
            }
            this.Code = ((int)responseType).ToString();
            if (!string.IsNullOrEmpty(msg))
            {
                Message = msg;
                return this;
            }
            Message = responseType.GetMsg();
            return this;
        }

    }
}
