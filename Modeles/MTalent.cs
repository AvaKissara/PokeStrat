using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MTalent
    {
        public int idTalent { get; }
        public string nomTalent { get; set; }
        public MVersion versionTalent { get; set; }
        public string descTalent { get; set; }
        public MTerrain effetTerrain { get; set; }

        public MTalent(int IdTalent, string NomTalent, string DescTalent)
        {
            this.idTalent = IdTalent;
            this.nomTalent = NomTalent;
            this.descTalent = DescTalent;
        }
        public MTalent(int IdTalent, string NomTalent)
        {
            this.idTalent = IdTalent;
            this.nomTalent = NomTalent;
        }
    }
}
