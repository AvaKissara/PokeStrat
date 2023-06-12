using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MStat
    {
        public int pv { get; set; }
        public int attaque { get; set; }
        public int defense { get; set; }
        public int attSpe { get; set; }
        public int defSpe { get; set; }
        public int vitesse { get; set; }
        public int pp { get; set; }
        public int puiss { get; set; }
        public int pre { get; set; }


        public MStat(int PV, int Attaque, int Defense, int AttSpe, int DefSpe, int Vitesse, int Pp, int Puiss, int Pre)
        {
            this.pv = PV;
            this.attaque = Attaque;
            this.defense = Defense;
            this.attSpe = AttSpe;
            this.defSpe = DefSpe;
            this.vitesse = Vitesse;
            this.pp = Pp;
            this.puiss = Puiss;
            this.pre = Pre;
        }
    }
}
