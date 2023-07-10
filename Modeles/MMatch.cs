
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MMatch
    {
        public int idMatch { get; }
        public int dureeMatch { get; set; }
        List<MEquipe> equipes { get; set;}
    }
}
