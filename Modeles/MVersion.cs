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
        public int emplacement { get; set; }

        public MVersion(int IdVersion, string NomVersion, int Emplacement) 
        {
            this.idVersion = IdVersion;
            this.nomVersion = NomVersion;
            this.emplacement = Emplacement;
        }
    }
}
