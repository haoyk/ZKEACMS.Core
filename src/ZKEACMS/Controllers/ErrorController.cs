/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ZKEACMS.Controllers
{

    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }
        public ActionResult Index()
        {
            Response.StatusCode = 500;
            return View();
        }

        public ActionResult NotFond()
        {
            Response.StatusCode = 404;
            //Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            //Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}
