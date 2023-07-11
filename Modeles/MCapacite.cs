using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MCapacite
    {
        public int idCapacite { get; }
        public string nomCapacite { get; set; }
        public string descCapacite { get; set; }    
        public int base_pp { get; set; }
        public int base_puiss { get; set; }
        public int base_pre { get; set; }
        public int base_prio { get; set; }
        public int base_crit { get; set; }
        public MType typeCapacite { get; set; }
        public List<MCategorie> catCapacite { get; set; }
        public MMeteo effetMeteo { get; set; }
        public MStatut effetStatut { get; set; }
        public MTerrain effetTerrain { get; set; }
        public MVersion versCapacite { get; set; }
        public MCapacite(int IdCapacite, string NomCapacite, MType TypeCapacite, List<MCategorie> CatCapacite)
        {
            this.idCapacite = IdCapacite;
            this.nomCapacite = NomCapacite;
            this.typeCapacite = TypeCapacite;
            this.catCapacite = CatCapacite;
        }
    }
}
