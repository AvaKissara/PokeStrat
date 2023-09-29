
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MMatch
    {
        public int IdMatch { get; }
        public int DureeMatch { get; set; }
        public List<MEquipe> Equipes { get; set; }

        public MMatch(int idMatch)
        {
            IdMatch = idMatch;
            Equipes = new List<MEquipe>();
        }
    }
}
