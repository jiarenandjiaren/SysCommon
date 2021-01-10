﻿using Microsoft.AspNetCore.Mvc;
using SysCommon.Service.Interface;

namespace SysCommon.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
       
       
        
        public ActionResult Git()
        {
            return View();
        }


        public HomeController(IAuth authUtil) : base(authUtil)
        {
        }
    }
}