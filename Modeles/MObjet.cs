using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MObjet
    {
        public int idObjet { get; }
        public string nomObjet { get; set; }
        public string descObjet { get; set; }

        public MObjet(int IdObjet, string NomObjet, string DescObjet)
        {
            this.idObjet = IdObjet;
            this.nomObjet = NomObjet;
            this.descObjet = DescObjet;
        }
        public MObjet(int IdObjet, string NomObjet)
        {
            this.idObjet = IdObjet;
            this.nomObjet = NomObjet;
        }
    }
}
