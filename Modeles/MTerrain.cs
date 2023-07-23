using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MTerrain
    {
        public int idTerrain { get; set; }
        public string nomTerrain { get; set; }
        public string descTerrain { get; set; }

        public MTerrain(int IdTerrain, string NomTerrain) 
        {
            this.idTerrain = IdTerrain;
            this.nomTerrain = NomTerrain;
        }
    }
}
