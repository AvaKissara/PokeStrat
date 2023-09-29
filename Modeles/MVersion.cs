using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MVersion
    {
        public int IdVersion { get; set; }
        public string NomVersion { get; set; }
        public MGeneration Gen { get; set; }

        public MVersion(int IdVersion, string NomVersion, MGeneration Gen) 
        {
            this.IdVersion = IdVersion;
            this.NomVersion = NomVersion;
            this.Gen = Gen;
        }
        public MVersion(string NomVersion, MGeneration Gen)
        {
            this.NomVersion = NomVersion;
            this.Gen = Gen;
        }
        public MVersion() { }

        public override bool Equals(object objTest)
        {
            if (objTest == null || GetType() != objTest.GetType())
                return false;

            MVersion objRef = (MVersion)objTest;
            return IdVersion == objRef.IdVersion && NomVersion == objRef.NomVersion;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + IdVersion.GetHashCode();
                hash = hash * 23 + (NomVersion != null ? NomVersion.GetHashCode() : 0);
                return hash;
            }
        }
    }

}
