using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MCategorie
    {
        public int idCategorie { get; }
        public string nomCategorie { get; set; }

        public MCategorie(int IdCategorie, string NomCategorie)
        {
            this.idCategorie = IdCategorie;
            this.nomCategorie = NomCategorie;
        }
    }
}
