using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sol_Demo.Controllers
{
    public class CustomErrorController : Controller
    {
        [HttpGet("error/{code:int}")]
        public IActionResult Index(int code)
        {
            ViewBag.Code = code;

            return View();
        }
    }
}