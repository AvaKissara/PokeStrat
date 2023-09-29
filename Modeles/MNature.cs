using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MNature
    {
        public int IdNature { get; }
        public string NomNature { get; set; }

        public MNature(int IdNature, string NomNature) 
        {
            this.IdNature = IdNature;
            this.NomNature = NomNature;
        }
    }
}
