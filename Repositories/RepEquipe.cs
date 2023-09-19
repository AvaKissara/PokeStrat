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

            //try
            //{
                SqlCommand requestGetEquipes = bddTool.GetRequest();
                requestGetEquipes.CommandText = "SELECT E.id_equipe, E.nom_equipe, E.user_id, U.pseudo, E.date_id FROM Equipes as E LEFT JOIN Users as U on E.user_id = U.id_user;";
                //requestGetEquipes.CommandText = "SELECT * FROM Equipes WHERE userId = @UserId";
                //requestGetEquipes.Parameters.AddWithValue("@UserId", SessionManager.Instance.UserId);

                using (SqlDataReader equipes = requestGetEquipes.ExecuteReader())
                {
                    while (equipes.Read())
                    {
                    MDate creationDate = null;
                    if (!equipes.IsDBNull(4))
                    {
                        DateTime idDate = DateTime.Parse($"{equipes[4]}");
                        creationDate = new MDate(idDate);
                    }
                    MUser user = null;
                    if(!equipes.IsDBNull(2))
                    {
                        int idUser = equipes.IsDBNull(2) ? 0 : equipes.GetInt32(2);
                        string nomUser = nomUser = equipes.IsDBNull(3) ? "" : $"{equipes[3]}";
                        user = new MUser(idUser, nomUser);
                    }

                    MEquipe uneEquipe = new MEquipe(
                            equipes.GetInt32(0), // idEquipe
                            $"{equipes[1]}",    // nomEquipe
                            user, // userId
                            creationDate// dateMatch
                        );

                        observableMEquipes.Add(uneEquipe);
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    // Gestion de l'exception
            //    Console.WriteLine("Erreur lors de la récupération des équipes : " + ex.Message);
            //}

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return observableMEquipes;
        }

    }
}
