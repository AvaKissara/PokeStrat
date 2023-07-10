using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class TypeObj
    {
        public int idTypeObj { get; }
        public string nomTypeObj { get; set;  }
        public string descTypeObj { get; set; }
        public TypeObj(int IdTypeObj, string NomTypeObj, string DescTypeObj) 
        { 
            this.idTypeObj = IdTypeObj;
            this.nomTypeObj = NomTypeObj;
            this.descTypeObj = DescTypeObj;
        }
    }
}
