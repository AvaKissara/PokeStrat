using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class TypeObj
    {
        public int IdTypeObj { get; }
        public string NomTypeObj { get; set; }
        public string DescTypeObj { get; set; }
        public TypeObj(int IdTypeObj, string NomTypeObj, string DescTypeObj) 
        { 
            this.IdTypeObj = IdTypeObj;
            this.NomTypeObj = NomTypeObj;
            this.DescTypeObj = DescTypeObj;
        }
    }
}
