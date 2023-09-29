using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MTalent
    {
        public int IdTalent { get; }
        public string NomTalent { get; set; }
        public MVersion VersionTalent { get; set; }
        public string DescTalent { get; set; }
        public MTerrain EffetTerrain { get; set; }

        public MTalent(int IdTalent, string NomTalent, string DescTalent)
        {
            this.IdTalent = IdTalent;
            this.NomTalent = NomTalent;
            this.DescTalent = DescTalent;
        }
        public MTalent(int IdTalent, string NomTalent)
        {
            this.IdTalent = IdTalent;
            this.NomTalent = NomTalent;
        }
    }
}
