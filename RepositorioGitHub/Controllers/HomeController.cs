using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Web.Script.Serialization;

using RepositorioGitHub.Models;
using RepositorioGitHub.Services;

namespace RepositorioGitHub.Controllers
{
    public class HomeController : Controller
    {
        private string _UserName = "alexdefaro";

        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult MeusRepositorios()
        {
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = GitHubService.BuscarRepositorios(this._UserName);
            return PartialView("_MeusRepositorios", listaDeItensDoRepositorio);
        }

        public ActionResult Favoritos()
        {
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = GitHubService.BuscarFavoritos(this._UserName);
            return PartialView("_Favoritos", listaDeItensDoRepositorio); 
        }
        
        public ActionResult Consulta()
        {
            return PartialView("_Consulta");
        }
        
        public ActionResult PesquisarRepositorios(string nomeDoItemDoRepositorio)
        {
            ConsultaDoRepositorio dadosDoRepositorio = GitHubService.PesquisarRepositorios(nomeDoItemDoRepositorio);
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = dadosDoRepositorio.items;
            return PartialView("_ListaDeRepositorios", listaDeItensDoRepositorio); 
        }

        public ActionResult DetalhesDoRepositorio(string nomeDoDonoDoRepositorio, string nomeDoItemDoRepositorio)
        {
            DadosDoRepositorio dadosDoRepositorio = GitHubService.BuscarDadosDoRepositorio(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio);
            dadosDoRepositorio.colaboradores = GitHubService.BuscarColaboradores(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio); 
            dadosDoRepositorio.Favorito      = GitHubService.VerificarSeRepositorioEhFavorito(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio); 
            
            return PartialView("_DetalhesDoRepositorio", dadosDoRepositorio);
        } 

        public ActionResult AlterarStatusDeFavorito(string nomeDoDonoDoRepositorio, string nomeDoItemDoRepositorio, bool statusAtual)
        {
            GitHubService.AlterarStatusDeFavorito(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio, statusAtual);

            DadosDoRepositorio dadosDoRepositorio = GitHubService.BuscarDadosDoRepositorio(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio);
            dadosDoRepositorio.colaboradores = GitHubService.BuscarColaboradores(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio); 
            dadosDoRepositorio.Favorito      = GitHubService.VerificarSeRepositorioEhFavorito(nomeDoDonoDoRepositorio, nomeDoItemDoRepositorio); 
            
            return PartialView("_DetalhesDoRepositorio", dadosDoRepositorio);
        } 
        
    }
}
