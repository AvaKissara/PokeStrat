using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepEquipe
    {
        public BddTool bddTool;

        public RepEquipe()
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

        public ObservableCollection<MEquipe> GetAllEquipes()
        {
            bddTool.CheckConnexion();

            ObservableCollection<MEquipe> observableMEquipes = new ObservableCollection<MEquipe>();

            try
            {
                SqlCommand requestGetEquipes = bddTool.GetRequest();
                requestGetEquipes.CommandText = "SELECT * FROM Equipes WHERE userId = @UserId";
                requestGetEquipes.Parameters.AddWithValue("@UserId", SessionManager.Instance.UserId);

                using (SqlDataReader equipes = requestGetEquipes.ExecuteReader())
                {
                    while (equipes.Read())
                    {
                        MEquipe uneEquipe = new MEquipe(
                            equipes.GetInt32(0), // idEquipe
                            $"{equipes[1]}",    // nomEquipe
                            equipes.GetInt32(2), // userId
                            DateTime.Parse($"{equipes[3]}") // dateMatch
                        );

                        observableMEquipes.Add(uneEquipe);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération des équipes : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return observableMEquipes;
        }

    }
}
