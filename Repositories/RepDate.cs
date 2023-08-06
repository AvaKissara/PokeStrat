using PokeStat.Modeles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    /// <summary>
    /// Excessif mais peut devenir intéressant selon l'évolution du projet
    /// </summary>
    public class RepDate
    {
        private SqlConnection activeConnexion;

        public RepDate()
        {
            try
            {
                this.DbConnecter();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la connexion à la base de données : " + ex.Message);
            }
        }

        public void DbConnecter()
        {
            Connexion con = new Connexion();
            this.activeConnexion = con.GetConnexion();
        }
        public void CheckConnexion()
        {
            if (activeConnexion.State == ConnectionState.Closed)
            {
                activeConnexion.Open();
            }
        }

        public void Add(MDate nouvelleDate) 
        {
            CheckConnexion();
            //try
            //{
                SqlCommand RequestAddDate = activeConnexion.CreateCommand();
                RequestAddDate.CommandText = "INSERT INTO Dates VALUES (@jjmmaaaa)";

                SqlParameter idDate = RequestAddDate.Parameters.Add("@jjmmaaaa", SqlDbType.DateTime);
                idDate.Value = nouvelleDate.idDate;

                int result = RequestAddDate.ExecuteNonQuery();
            //}
            //catch (Exception ex) 
            //{
            //    Console.WriteLine("Erreur lors de l'ajout de la date : " + ex.Message);
            //}

            this.activeConnexion.Close();
        }
    }
}
