using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public class MTalent
    {
        public int idTalent { get; }
        public string nomTalent { get; set; }
        public MVersion versionTalent { get; set; }

        public MTalent(int IdTalent, string NomTalent)
        {
            this.idTalent = IdTalent;
            this.nomTalent = NomTalent;
        }
    }
}
