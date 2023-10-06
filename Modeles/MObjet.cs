using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MObjet
    {
        public int IdObjet { get; }
        public string NomObjet { get; set; }
        public string DescObjet { get; set; }

        public MObjet(int IdObjet, string NomObjet, string DescObjet)
        {
            this.IdObjet = IdObjet;
            this.NomObjet = NomObjet;
            this.DescObjet = DescObjet;
        }
        public MObjet(int IdObjet, string NomObjet)
        {
            this.IdObjet = IdObjet;
            this.NomObjet = NomObjet;
        }
    }
}
