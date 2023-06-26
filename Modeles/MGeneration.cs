using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MGeneration
    {
        public int idGen { get; }
        public string nomGen { get; set; }

        public MGeneration(int IdGen, string NomGen)
        { 
            this.idGen = IdGen; 
            this.nomGen = NomGen; 
        }

    }
}
