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
        public int IdHab { get; }
        public string NomHab { get; set; }
        public string DescHab { get; set; }

        public MHabitat(int idHab, string nomHab, string descHab)
        {
            IdHab = idHab;
            NomHab = nomHab;
            DescHab = descHab;
        }
    }
}
