using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Shooting_range_core.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int? statusCode)
        {
            // status code 1000 for moodle
            if (statusCode != 1000)
            {
                ViewBag.StatusCode = statusCode.ToString();
            }
            else
            {
                ViewBag.StatusCode = "في مودل";
            }
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "عذراً, ولكن الصفحة التي تبحث عنها غير موجودة.";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "عذراً, حصل خطأ في الموقع.";
                    break;

                default:
                    ViewBag.ErrorMessage = "عذراً, حصل خطأ في الموقع.";
                    break;

            }
            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ErrorMessage = "عذراً, حصل خطأ في الموقع.";

            //additional error details
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            if (HttpContext.Request.Query["devMode"].Count > 0)
            {
                var devMode = HttpContext.Request.Query["devMode"].ToString();

                if (devMode == "1")
                {
                    ViewBag.DevMode = true;
                }
            }
            return View("Error");
        }
    }
}