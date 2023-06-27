using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MHabitat
    {
        public int idHab { get; }
        public string nomHab { get; set; }
        public string descHab { get; set; } 
        public MHabitat(int idHab, string nomHab, string descHab)
        {
            this.idHab = idHab;
            this.nomHab = nomHab;
            this.descHab = descHab;
        }
    }
}
