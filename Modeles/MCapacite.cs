using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public class MCapacite
    {
        public int idCapacite { get; }
        public string nomCapacite { get; set; }
        public MType typeCapacite { get; set; }
        public List<MCategorie> catCapacite { get; set; }

        public MCapacite(int IdCapacite, string NomCapacite, MType TypeCapacite, List<MCategorie> CatCapacite)
        {
            this.idCapacite = IdCapacite;
            this.nomCapacite = NomCapacite;
            this.typeCapacite = TypeCapacite;
            this.catCapacite = CatCapacite;
        }
    }
}
