using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokeStat.Modeles
{
    public class MUser : MPersonne
    {
    
        public MUser(int idPersonne, string nomPersonne, string prenomPersonne, string pseudoPersonne, string mailPersonne, SecureString mdpPersonne, DateTime roleActualise, MDate roleCree)
      : base(idPersonne, nomPersonne, prenomPersonne, pseudoPersonne, mailPersonne, mdpPersonne, roleActualise, roleCree)
        {

        }

        public MUser(int idPersonne, string pseudoPersonne)
     : base(idPersonne, pseudoPersonne)
        {

        }

        public MUser()
        { }

       


    }
}
