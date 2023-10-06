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
    public class RepObjet
    {
        public BddTool bddTool;

        public RepObjet()
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

        public List<MObjet> GetAll()
        {
            bddTool.CheckConnexion();

            List<MObjet> ListMObjets = new List<MObjet>();
            try
            {
                SqlCommand RequestGetObjets = bddTool.GetRequest();
                RequestGetObjets.CommandText = "SELECT * FROM Objets";

                using (SqlDataReader objets = RequestGetObjets.ExecuteReader())
                {
                    while (objets.Read())
                    {
                        MObjet uneObjet = new MObjet(
                            objets.GetInt32(0),
                            $"{objets[1]}"
                        );

                        ListMObjets.Add(uneObjet);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération des objets : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
            return ListMObjets;
        }
    }
}
