using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Utilitaires
{
    public class BddTool
    {
        private SqlConnection activeConnexion;

        public void DbConnnexion()
        {
            ConnexionBdd con = new ConnexionBdd();
            this.activeConnexion = con.GetConnexion();
        }
        public void CheckConnexion()
        {
            if (activeConnexion.State == ConnectionState.Closed)
            {
                activeConnexion.Open();
            }
        }
        public void CloseConnexion() 
        {
            this.activeConnexion.Close();
        }
        public SqlCommand GetRequest() 
        {
            SqlCommand Request = activeConnexion.CreateCommand();
            return Request;
        }
    }
}
