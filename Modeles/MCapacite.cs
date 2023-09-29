using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MCapacite
    {
        public int IdCapacite { get; }
        public string NomCapacite { get; set; }
        public string DescCapacite { get; set; }
        public int BasePp { get; set; }
        public int BasePuiss { get; set; }
        public int BasePre { get; set; }
        public int BasePrio { get; set; }
        public int BaseCrit { get; set; }
        public MType TypeCapacite { get; set; }
        public List<MCategorie> CatCapacite { get; set; }
        public MMeteo EffetMeteo { get; set; }
        public MStatut EffetStatut { get; set; }
        public MTerrain EffetTerrain { get; set; }
        public MVersion VersCapacite { get; set; }

        public MCapacite(int idCapacite, string nomCapacite, MType typeCapacite, List<MCategorie> catCapacite)
        {
            IdCapacite = idCapacite;
            NomCapacite = nomCapacite;
            TypeCapacite = typeCapacite;
            CatCapacite = catCapacite;
        }

    }
}
