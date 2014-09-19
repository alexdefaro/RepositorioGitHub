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
    public class HomeControllerTest
    {
        [TestMethod]
        public void ChamadaAIndexDoHomeControllerDeveRetornarUmaView()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ChamadaAMeusRepositoriosDoHomeControllerDeveRetornarUmaPartialView()
        {
            HomeController controller = new HomeController();
            PartialViewResult result = controller.MeusRepositorios() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ChamadaAFavoritosDoHomeControllerDeveRetornarUmaPartialView()
        {
            HomeController controller = new HomeController();
            PartialViewResult result = controller.Favoritos() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ChamadaAConsultaDoHomeControllerDeveRetornarUmaPartialView()
        {
            HomeController controller = new HomeController();
            PartialViewResult result = controller.Consulta() as PartialViewResult;
            Assert.IsNotNull(result);
        }
    }
}
