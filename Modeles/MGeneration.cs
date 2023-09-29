using System;

namespace PokeStat.Modeles
{
    public class MGeneration
    {
        public int IdGen { get; set; }
        public string NomGen { get; set; }

        public MGeneration(int idGen, string nomGen)
        {
            IdGen = idGen;
            NomGen = nomGen;
        }

        public MGeneration(string nomGen)
        {
            NomGen = nomGen;
        }

        public MGeneration() { }

        public override bool Equals(object objTest)
        {
            if (objTest == null || GetType() != objTest.GetType())
                return false;

            MGeneration objRef = (MGeneration)objTest;
            return IdGen == objRef.IdGen && NomGen == objRef.NomGen;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + IdGen.GetHashCode();
                hash = hash * 23 + (NomGen != null ? NomGen.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
