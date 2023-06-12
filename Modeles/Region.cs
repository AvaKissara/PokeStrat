using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class Region
    {
        public int idReg {  get; set; }
        public string nomReg { get; set; }
        public Region(int IdReg, string NomReg) 
        {
            this.idReg = IdReg;
            this.nomReg = NomReg;
        } 
    }
}
