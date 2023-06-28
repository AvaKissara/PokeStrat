using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MUser
    {
        public int idUser { get; set; }   
        public string nomUser { get; set; }

        public MUser(int IdUser, string NomUser) 
        {
            this.idUser = IdUser;
            this.nomUser = NomUser;
        }
    }
}
