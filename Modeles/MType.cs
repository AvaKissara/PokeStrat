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

        //public MType(int IdType, string NomType, int Emplacement)
        //{
        //    this.idType = IdType;
        //    this.nomType = NomType;
        //    this.emplacement = Emplacement;
        //}

        public MType()
        {
        }
    }
}
