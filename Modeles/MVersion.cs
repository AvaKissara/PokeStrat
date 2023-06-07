using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public class MVersion
    {
        public int idVersion { get; }
        public string nomVersion { get; set; }
        public int emlplacement { get; set; }

        public MVersion(int IdVersion, string NomVersion, int Emplacement) 
        {
            this.idVersion = IdVersion;
            this.nomVersion = NomVersion;
            this.emlplacement = Emplacement;
        }
    }
}
