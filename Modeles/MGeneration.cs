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
        public MGeneration(string NomGen)
        {
            this.nomGen = NomGen;
        }
        public MGeneration() { }

        public override bool Equals(object objTest)
        {
            if (objTest == null || GetType() != objTest.GetType())
                return false;

            MGeneration objRef = (MGeneration)objTest;
            return idGen == objRef.idGen && nomGen == objRef.nomGen;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + idGen.GetHashCode();
                hash = hash * 23 + (nomGen != null ? nomGen.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
