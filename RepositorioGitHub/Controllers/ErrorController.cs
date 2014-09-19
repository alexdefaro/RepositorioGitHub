using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositorioGitHub.Controllers
{
    public class ErrorController : Controller
    { 
        public ActionResult Index( Exception exception )
        {
            return PartialView("_Index", exception.Message);
        }

    }
}
