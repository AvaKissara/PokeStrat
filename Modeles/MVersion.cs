using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MVersion
    {
        public int idVersion { get; }
        public string nomVersion { get; set; }
        public int genId { get; set; }

        public MVersion(int IdVersion, string NomVersion, int GenId) 
        {
            this.idVersion = IdVersion;
            this.nomVersion = NomVersion;
            this.genId = genId;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MVersion other = (MVersion)obj;
            return idVersion == other.idVersion && nomVersion == other.nomVersion;
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
