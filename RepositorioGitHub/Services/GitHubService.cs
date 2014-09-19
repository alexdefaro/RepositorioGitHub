using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Text; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using RepositorioGitHub.Models;

namespace RepositorioGitHub.Services
{
    public static class GitHubService
    {
        static JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
         
        private static string AcessarGitHubByPut( string endereco )
        { 
            return AcessarGitHubUsandoHttpWebRequest(endereco, HttpVerbs.Put);
        }

        private static string AcessarGitHubByDelete( string endereco )
        { 
            return AcessarGitHubUsandoHttpWebRequest(endereco, HttpVerbs.Delete);
        }

        private static string AcessarGitHubByGet( string endereco )
        { 
            return AcessarGitHubUsandoHttpWebRequest(endereco, HttpVerbs.Get);
            //return AcessarGitHubUsandoHttpClient(endereco, HttpVerbs.Get).Result;
        }        
        
        private static async Task<string> AcessarGitHubUsandoHttpClient( string endereco, HttpVerbs httpVerb )
        { 
            try
            {    
                string resultadoDaConsulta = String.Empty;
                
                HttpClient httpClient = new HttpClient( new HttpClientHandler { Credentials = new NetworkCredential( "alexdefaro", "alex2909" ) } ); 
                 
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                   
                resultadoDaConsulta = await httpClient.GetStringAsync( endereco );
                
                return resultadoDaConsulta; 

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync( endereco );
                httpResponseMessage.EnsureSuccessStatusCode();
                
                JsonResult jsonResult = await httpResponseMessage.Content.ReadAsAsync<JsonResult>();

                resultadoDaConsulta = jsonResult.ToString(); 

                return resultadoDaConsulta; 
            }
            catch (WebException e)
            {
                throw e; 
            }
        }

        private static string AcessarGitHubUsandoHttpWebRequest( string endereco, HttpVerbs httpVerb )
        { 
            try
            {    
                HttpWebRequest  webRequest = WebRequest.Create( endereco ) as HttpWebRequest;
                webRequest.Method = httpVerb.ToString().ToUpper();
                webRequest.ContentLength=0;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.UserAgent = "RepositorioGitHub";
                webRequest.Headers.Add("Authorization","Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes("alexdefaro"+":"+"alex2909")));

                string resultadoDaConsulta = String.Empty;
                if ( httpVerb == HttpVerbs.Get )
                {  
                    Stream responseStream = webRequest.GetResponse().GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);
                    resultadoDaConsulta = streamReader.ReadToEnd();   
                }
                else 
                {  
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    resultadoDaConsulta = response.StatusCode.ToString();
                }


                return resultadoDaConsulta;
            }
            catch (WebException e)
            {
                throw e; 
            }
        }
         
        public static DadosDoRepositorio BuscarDadosDoRepositorio(string nomeDoDonoDoRepositorio, string nomeDoItemDoRepositorio)
        {
            string resultadoDaConsulta = GitHubService.AcessarGitHubByGet("https://api.github.com/repos/"+nomeDoDonoDoRepositorio+"/"+nomeDoItemDoRepositorio); 
            DadosDoRepositorio dadosDoRepositorio = _JavaScriptSerializer.Deserialize<DadosDoRepositorio>(resultadoDaConsulta);

            return dadosDoRepositorio;
        }

        public static IList<DadosDoUsuario> BuscarColaboradores(string nomeDoDonoDoRepositorio, string nomeDoItemDoRepositorio)
        {
            string resultadoDaConsulta = GitHubService.AcessarGitHubByGet("https://api.github.com/repos/"+nomeDoDonoDoRepositorio+"/"+nomeDoItemDoRepositorio+@"/collaborators"); 
            IList<DadosDoUsuario> listaDeColaboradores = _JavaScriptSerializer.Deserialize<List<DadosDoUsuario>>(resultadoDaConsulta);
            
            return listaDeColaboradores;
        }

        public static IList<DadosDoRepositorio> BuscarRepositorios(string nomeDoUsuario)
        {
            string resultadoDaConsulta = GitHubService.AcessarGitHubByGet("https://api.github.com/users/"+nomeDoUsuario+"/repos");
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = _JavaScriptSerializer.Deserialize<List<DadosDoRepositorio>>(resultadoDaConsulta);

            return listaDeItensDoRepositorio;
        }
        
        public static IList<DadosDoRepositorio> BuscarFavoritos(string nomeDoUsuario)
        {
            string resultadoDaConsulta = GitHubService.AcessarGitHubByGet("https://api.github.com/users/"+nomeDoUsuario+"/starred");
            IList<DadosDoRepositorio> listaDeItensDoRepositorio = _JavaScriptSerializer.Deserialize<List<DadosDoRepositorio>>(resultadoDaConsulta);

            return listaDeItensDoRepositorio;
        } 
        
        public static ConsultaDoRepositorio PesquisarRepositorios(string nomeDoItemDoRepositorio)
        {
            string resultadoDaConsulta = GitHubService.AcessarGitHubByGet("https://api.github.com/search/repositories?q="+nomeDoItemDoRepositorio+"+in:name");
            ConsultaDoRepositorio dadosDoRepositorio = _JavaScriptSerializer.Deserialize<ConsultaDoRepositorio>(resultadoDaConsulta);
            
            return dadosDoRepositorio;
        }

        public static bool VerificarSeRepositorioEhFavorito(string nomeDoDonoDoRepositorio, string nomeDoItemDoRepositorio)
        {
            try
            {
                string resultadoDaConsulta = GitHubService.AcessarGitHubByGet("https://api.github.com/user/starred/"+nomeDoDonoDoRepositorio+"/"+nomeDoItemDoRepositorio); 
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        public static void AlterarStatusDeFavorito(string nomeDoDonoDoRepositorio, string nomeDoItemDoRepositorio, bool statusAtual)
        {
            if ( statusAtual == true )
            {
                GitHubService.AcessarGitHubByDelete("https://api.github.com/user/starred/"+nomeDoDonoDoRepositorio+"/"+nomeDoItemDoRepositorio); 
            }
            else
            {
                GitHubService.AcessarGitHubByPut("https://api.github.com/user/starred/"+nomeDoDonoDoRepositorio+"/"+nomeDoItemDoRepositorio); 
            }
        } 
    }
}