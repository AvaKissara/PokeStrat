using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MUser
    {
        public int idUser { get; set; }   
        public string nomUser { get; set; }
        public string prenomUser { get; set; }
        public string pseudoUser { get; set; }
        public string mailUser { get; set; }
        public SecureString mdpUser { get; set; }
        public DateTime actualise { get; set; }  
        public DateTime cree { get; set; }

        public MUser(int IdUser, string NomUser, string PrenomUser, string PseudoUser, string MailUser, SecureString MdpUser, DateTime Actualise,  DateTime Cree) 
        {
            this.idUser = IdUser;
            this.nomUser = NomUser;
            this.prenomUser = PrenomUser;
            this.pseudoUser = PseudoUser;
            this.mailUser = MailUser;
            this.mdpUser = MdpUser;
            this.actualise = Actualise;
            this.cree= Cree;
        }
    }
}
