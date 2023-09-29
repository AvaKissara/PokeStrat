using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MMeteo
    {
        public int IdMeteo { get; set; }
        public string NomMeteo { get; set; }
        public string DescMeteo { get; set; }

        public MMeteo(int IdMeteo, string NomMeteo, string DescMeteo)
        {
            this.IdMeteo = IdMeteo;
            this.NomMeteo = NomMeteo;
            this.DescMeteo = DescMeteo;
        }
    }

}
