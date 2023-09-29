using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MTerrain
    {
        public int IdTerrain { get; set; }
        public string NomTerrain { get; set; }
        public string DescTerrain { get; set; }

        public MTerrain(int IdTerrain, string NomTerrain) 
        {
            this.IdTerrain = IdTerrain;
            this.NomTerrain = NomTerrain;
        }
    }
}
