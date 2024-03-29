﻿using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Controllers
{
    public class AppSettingController : Controller
    {
        private readonly IConfiguration _configuration;

        public AppSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.smsKey = _configuration["Keys:Sms"];
            ViewBag.emaillKey = _configuration.GetSection("Keys")["email"];
            return View();
        }
    }
}
