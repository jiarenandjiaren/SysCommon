using Infrastructure.Enums;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.Request
{
    public class Response1
    {
        public Response1()
        {
        }
        public Response1(bool status)
        {
            this.Status = status;
        }
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        //public string Message { get; set; }
        public object Data { get; set; }

        public Response1 OK()
        {
            this.Status = true;
            return this;
        }

        public static Response1 Instance
        {
            get { return new Response1(); }
        }
        public Response1 OK(string message = null, object data = null)
        {
            this.Status = true;
            this.Message = message;
            this.Data = data;
            return this;
        }
        public Response1 OK(ResponseType responseType)
        {
            return Set(responseType, true);
        }
        public Response1 Error(string message = null)
        {
            this.Status = false;
            this.Message = message;
            return this;
        }
        public Response1 Error(ResponseType responseType)
        {
            return Set(responseType, false);
        }
        public Response1 Set(ResponseType responseType)
        {
            bool? b = null;
            return this.Set(responseType, b);
        }
        public Response1 Set(ResponseType responseType, bool? status)
        {
            return this.Set(responseType, null, status);
        }
        public Response1 Set(ResponseType responseType, string msg)
        {
            bool? b = null;
            return this.Set(responseType, msg, b);
        }
        public Response1 Set(ResponseType responseType, string msg, bool? status)
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
