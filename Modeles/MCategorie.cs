using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MCategorie
    {
        public int IdCategorie { get; }
        public string NomCategorie { get; set; }

        public MCategorie(int IdCategorie, string NomCategorie)
        {
            this.IdCategorie = IdCategorie;
            this.NomCategorie = NomCategorie;
        }
    }
}
