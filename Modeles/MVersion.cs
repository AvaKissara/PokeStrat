using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MVersion
    {
        public int idVersion { get; set; }
        public string nomVersion { get; set; }
        public MGeneration gen{ get; set; }

        public MVersion(int IdVersion, string NomVersion, MGeneration Gen) 
        {
            this.idVersion = IdVersion;
            this.nomVersion = NomVersion;
            this.gen = Gen;
        }
        public MVersion(string NomVersion, MGeneration Gen)
        {
            this.nomVersion = NomVersion;
            this.gen = Gen;
        }
        public MVersion() { }


        public override bool Equals(object objTest)
        {
            if (objTest == null || GetType() != objTest.GetType())
                return false;

            MVersion objRef = (MVersion)objTest;
            return idVersion == objRef.idVersion && nomVersion == objRef.nomVersion;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + idVersion.GetHashCode();
                hash = hash * 23 + (nomVersion != null ? nomVersion.GetHashCode() : 0);
                return hash;
            }
        }
    }

}
