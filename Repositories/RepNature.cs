using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepNature
    {
        public BddTool bddTool;

        public RepNature()
        {
            bddTool = new BddTool();

            try
            {
                this.bddTool.DbConnnexion();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la connexion à la base de données : " + ex.Message);
            }
        }

        public List<MNature> GetAll()
        {
            bddTool.CheckConnexion();

            List<MNature> ListMNatures = new List<MNature>();
            try
            {
                SqlCommand RequestGetNatures = bddTool.GetRequest();
                RequestGetNatures.CommandText = "SELECT * FROM Natures";

                using (SqlDataReader natures = RequestGetNatures.ExecuteReader())
                {
                    while (natures.Read())
                    {
                        MNature uneNature = new MNature(
                            natures.GetInt32(0),
                            $"{natures[1]}"
                        );

                        ListMNatures.Add(uneNature);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération des natures : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
            return ListMNatures;
        }
    }
}
