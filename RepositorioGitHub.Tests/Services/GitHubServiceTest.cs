using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RepositorioGitHub;
using RepositorioGitHub.Models;
using RepositorioGitHub.Services;

namespace RepositorioGitHub.Tests.Controllers
{
    [TestClass]
    public class RepositorioGitHubTest
    {
        [TestMethod]
        public void BuscarRepositoriosDeAlexDeFaroDeveRetornarAlgumRepositorio()
        {
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = GitHubService.BuscarRepositorios("alexdefaro");
            Assert.AreEqual(true,listaDeItensDoRepositorio.Any());
        }

        [TestMethod]
        public void BuscarFavoritosDeAlexDeFaroDeveRetornarAlgumRepositorio()
        {
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = GitHubService.BuscarFavoritos("alexdefaro");
            Assert.AreEqual(true,listaDeItensDoRepositorio.Any());
        }

        [TestMethod]
        public void BuscarColaboradoresDeAlexDeFaroConsultaWay2DeveRetornarAlgumUsuario()
        {
            IList<DadosDoUsuario> listaDeItensDoRepositorio = GitHubService.BuscarColaboradores("alexdefaro", "ConsultaWay2");
            Assert.AreEqual(true,listaDeItensDoRepositorio.Any());
        }
                
        [TestMethod]
        public void BuscarDadosDoRepositorioConsultaWay2DeAlexDeFaroDeveRetornarUmRepositorio()
        {
            DadosDoRepositorio dadosDoRepositorio = GitHubService.BuscarDadosDoRepositorio("alexdefaro", "ConsultaWay2");
            Assert.IsNotNull(dadosDoRepositorio);
        }

        [TestMethod]
        public void PesquisarRepositoriosComParametroConsultaWay2DeveRetornarAlgumRepositorio()
        {
            ConsultaDoRepositorio dadosDoRepositorio = GitHubService.PesquisarRepositorios("ConsultaWay2");
            Assert.IsNotNull(dadosDoRepositorio);
            Assert.AreEqual(true,dadosDoRepositorio.items.Any());
        }

        [TestMethod]
        public void VerificarSeRepositorioalexdefaroRepositorioGitHubEhFavoritoDeveRetornarTrue()
        {
            bool repositorioEhFavorito = GitHubService.VerificarSeRepositorioEhFavorito("alexdefaro","RepositorioGitHub");
            Assert.AreEqual(true,repositorioEhFavorito);
        }

        [TestMethod]
        public void VerificarSeRepositoriofabianoFooEhFavoritoDeveRetornarTrue()
        {
            bool repositorioEhFavorito = GitHubService.VerificarSeRepositorioEhFavorito("fabiano","Foo");
            Assert.AreEqual(false,repositorioEhFavorito);
        }

        [TestMethod]
        public void AlterarStatusDoRepositorioalexdefaroRepositorioGitHubParaFalseDeveAlterarParaFalse()
        {
            GitHubService.AlterarStatusDeFavorito("alexdefaro","RepositorioGitHub",true);

            bool repositorioEhFavorito = GitHubService.VerificarSeRepositorioEhFavorito("alexdefaro","RepositorioGitHub");
            Assert.AreEqual(false,repositorioEhFavorito);
        }
    }
}
