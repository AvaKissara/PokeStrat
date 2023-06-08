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
        public string descTalent { get; set; }

        public MTalent(int IdTalent, string NomTalent, string DescTalent)
        {
            this.idTalent = IdTalent;
            this.nomTalent = NomTalent;
            this.descTalent = DescTalent;
        }
    }
}
