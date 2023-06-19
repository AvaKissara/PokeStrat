using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MNature
    {
        public int idNature { get; }
        public string nomNature { get; set; }

        public MNature(int IdNature, string NomNature) 
        {
            idNature = IdNature;
            nomNature = NomNature;
        }
    }
}
