using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorioGitHub;
using RepositorioGitHub.Controllers;

namespace RepositorioGitHub.Tests.Controllers
{
    [TestClass]
    public class ErrorControllerTest
    {
        [TestMethod]
        public void ChamadaAIndexDoErrorControllerDeveRetornarUmaView()
        {
            ErrorController controller = new ErrorController();
            PartialViewResult result = controller.Index( new Exception() ) as PartialViewResult;
            Assert.IsNotNull(result);
        }

    }
}
