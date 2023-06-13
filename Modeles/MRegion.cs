using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MRegion
    {
        public int idReg {  get; set; }
        public string nomReg { get; set; }
        public MRegion(int IdReg, string NomReg) 
        {
            this.idReg = IdReg;
            this.nomReg = NomReg;
        } 
    }
}
