﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public class MType
    {
        public int idType { get; }
        public string nomType { get; set; }

        public MType(int IdType, string NomType)
        {
            this.idType = IdType;
            this.nomType = NomType;
        }
    }
}
