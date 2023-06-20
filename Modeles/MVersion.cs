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
        public int numGen { get; set; }

        public MVersion(int IdVersion, string NomVersion, int NumGen) 
        {
            this.idVersion = IdVersion;
            this.nomVersion = NomVersion;
            this.numGen = NumGen;
        }
    }
}
