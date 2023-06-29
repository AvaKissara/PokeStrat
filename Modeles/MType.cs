using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MType
    {
        public int idType { get; set; }

        public string nomType { get; set; }

        //public  int emplacement { get; set; }


        public MType(string NomType)
        {
            this.nomType = NomType;
        }
        public MType(int IdType, string NomType)
        {
            this.idType = IdType;
            this.nomType = NomType;
        }

        public MType()
        {
         
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MType other = (MType)obj;
            return idType == other.idType && nomType == other.nomType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + idType.GetHashCode();
                hash = hash * 23 + (nomType != null ? nomType.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
