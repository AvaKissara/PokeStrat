﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MAdmin : MPersonne
    {
        public SecureString MdpRenforce { get; set; }
        public int CreateurAdmin { get; set; }


        public MAdmin(int idPersonne, string nomPersonne, string prenomPersonne, string pseudoPersonne, string mailPersonne, SecureString mdpPersonne, DateTime actualise, MDate cree)
      : base(idPersonne, nomPersonne, prenomPersonne, pseudoPersonne, mailPersonne, mdpPersonne, actualise, cree)
        {
            this.MdpRenforce = MdpRenforce;
        }
    }
}
