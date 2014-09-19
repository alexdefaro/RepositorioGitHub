using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositorioGitHub.Models
{    
    public class ConsultaDoRepositorio
    {
        public long total_count { get; set; } 
        public IList<DadosDoRepositorio> items { get; set; }

        public ConsultaDoRepositorio()
        {
            items = new List<DadosDoRepositorio>();
        }

    }
}