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
    public class RepVersion
    {
        private SqlConnection activeConnexion;

        public RepVersion()
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
        private void DbConnecter()
        {
            Connexion con = new Connexion();
            this.activeConnexion = con.GetConnexion();
        }

        private void CheckConnexion()
        {
            if (activeConnexion.State == ConnectionState.Closed)
            {
                activeConnexion.Open();
            }
        }
        public List<MVersion> GetVersions()
        {
            CheckConnexion();

            List<MVersion> ListMVersions = new List<MVersion>();

            try
            {
                SqlCommand RequestGetVersions = activeConnexion.CreateCommand();
                RequestGetVersions.CommandText = "SELECT * FROM Version";

                using (SqlDataReader versions = RequestGetVersions.ExecuteReader())
                {
                    while (versions.Read())
                    {
                        MVersion uneVersion = new MVersion(
                            versions.GetInt32(0),
                            $"{versions[1]}",
                            versions.GetInt32(2)
                        );

                        ListMVersions.Add(uneVersion);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération des types : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMVersions;
        }
    }
}
