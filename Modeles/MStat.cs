using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MStat
    {
        public int IdStat { get; set; }
        public string NomStat { get; set; }

        public MStat(int idStat, string nomStat)
        {
            IdStat = idStat;
            NomStat = nomStat;
        }
    }
}
