using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MRegion
    {
        public int IdReg { get; set; }
        public string NomReg { get; set; }

        public MRegion(int idReg, string nomReg)
        {
            IdReg = idReg;
            NomReg = nomReg;
        }
    }
}
