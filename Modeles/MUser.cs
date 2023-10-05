
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
    
        public MUser(int IdPersonne, string NomPersonne, string PrenomPersonne, string PseudoPersonne, string MailPersonne, SecureString MdpPersonne, DateTime RoleActualise, MDate RoleCree)
      : base(IdPersonne, NomPersonne, PrenomPersonne, PseudoPersonne, MailPersonne, MdpPersonne, RoleActualise, RoleCree)
        {

        }

        public MUser(int IdPersonne, string PseudoPersonne)
     : base(IdPersonne, PseudoPersonne)
        {

        }

        public MUser()
        { }

       


    }
}
