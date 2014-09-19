using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositorioGitHub.Models
{    
    public class DadosDoRepositorio
    {
        public long id { get; set; }
        public string name { get; set; } 
        public string full_name { get; set; } 
        public string description { get; set; } 
        public string language { get; set; } 
        public DateTime updated_at { get; set; }         
        public DadosDoUsuario owner { get; set; }   
        public IList<DadosDoUsuario> colaboradores { get; set; }
        public bool Favorito { get; set; }

        public DadosDoRepositorio()
        {
            colaboradores = new List<DadosDoUsuario>();
        }

    }
}