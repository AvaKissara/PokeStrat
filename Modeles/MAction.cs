using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MAction
    {
        public int IdAction { get; set; }
        public bool Baisse { get; set; }
        public bool Augmente { get; set; }
        public double Val { get; set; }
        public int Tour { get; set; }
        public MMatch Match { get; set; }
        public MEquipier Participant { get; set; }
        public MStat StatCible { get; set; }
        public MStatut StatutParticipant { get; set; }
        public MMeteo Meteo { get; set; }
        public MTerrain Terrain { get; set; }
        public MObjet Objet { get; set; }
        public MCapacite Capacite { get; set; }
        public MType TypeParticipant { get; set; }
    }
}
