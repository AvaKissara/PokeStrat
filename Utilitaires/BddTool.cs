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
        private readonly object connexionLock = new object();

        public SqlConnection DbConnexion()
        {
            if (activeConnexion == null)
            {
                lock (connexionLock)
                {
                    if (activeConnexion == null)
                    {
                        ConnexionBdd con = new ConnexionBdd();
                        activeConnexion = con.GetConnexion();
                    }
                }
            }
            return activeConnexion;
        }

        public void CheckConnexion()
        {
            SqlConnection connexion = DbConnexion();

            if (connexion.State == ConnectionState.Closed)
            {              
                connexion.Open();       
            }
        }

        public void CloseConnexion()
        {
            if (activeConnexion != null)
            {
                activeConnexion.Close();
            }
        }

        public SqlCommand GetRequest()
        {
            SqlCommand Request = activeConnexion.CreateCommand();
            return Request;
        }
    }
}
