using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MStat
    {
      public int idStat { get; set; }
      public string nomStat { get; set; }
      
        public MStat(int IdStat, string NomStat)
        {
            this.idStat = IdStat;
            this.nomStat = NomStat;
        }
    }
}
