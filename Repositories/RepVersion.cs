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
                RequestGetVersions.CommandText = "SELECT * FROM Versions";

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
                Console.WriteLine("Erreur lors de la récupération des versions : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMVersions;
        }

        public void AddVersion(MVersion nouvelleVersion)
        {
            CheckConnexion();

            try
            {
                SqlCommand RequestAddVersion = activeConnexion.CreateCommand();
                RequestAddVersion.CommandText = "INSERT INTO Versions (nom_version,gen_id) VALUES (@nom_version,@gen_id)";

                SqlParameter nom = RequestAddVersion.Parameters.Add("@nom_version", SqlDbType.VarChar);
                SqlParameter gen = RequestAddVersion.Parameters.Add("@gen_id", SqlDbType.Int);
                nom.Value = nouvelleVersion.nomVersion;
                gen.Value = nouvelleVersion.genId;

                int result = RequestAddVersion.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de l'ajout du type : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();
        }

    }
}
