using PokeStat.Vues.Authentification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat
{
    public class ConnexionBdd
    {
        SqlConnection connexion;
        public SqlConnection GetConnexion()
        {
            connexion = new SqlConnection(@"Data Source=LEODAGAN;Initial Catalog=PokeStat;Persist Security Info=True;User ID=sa;Password=It@chi8!");
            connexion.Open();
            return connexion;
        }

        public void Deconnexion()
        {
            if (connexion.State == ConnectionState.Open)
            {
                connexion.Close();
            }
        }
    }
}
