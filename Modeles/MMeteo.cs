using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MMeteo
    {
        public int idMeteo { get; set; }
        public string nomMeteo { get; set; }
        public string descMeteo { get; set; }
        public MMeteo(int IdMeteo, string NomMeteo, string DescMeteo) 
        {
            this.idMeteo = IdMeteo;
            this.nomMeteo = NomMeteo;
            this.descMeteo = DescMeteo;
        } 
    }
}
