using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

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
                requestGetEquipes.CommandText = "SELECT E.id_equipe, E.nom_equipe, E.date_id, U.id_user, U.pseudo, EQ.pok_id, P.pok_img, P.nom_fra_pok, EQ.pv, EQ.att, EQ.def, EQ.att_spe, EQ.def_spe, EQ.vit, EQ.surnom, EQ.niveau, EQ.esquive, EQ.ev, EQ.iv, EQ.niv_bonheur, EQ.nature_id, N.nom_nature, EQ.talent_id, T.nom_talent, EQ.objet_id, O.nom_objet, EQ.cap1_id, C.nom_cap, EQ.cap1_pp, EQ.cap1_pre, EQ.cap1_puiss, EQ.cap1_crit, EQ.cap2_id, C2.nom_cap, EQ.cap2_pp, EQ.cap2_pre, EQ.cap2_puiss, EQ.cap2_crit, EQ.cap3_id, C3.nom_cap, EQ.cap3_pp, EQ.cap3_pre, EQ.cap3_puiss, EQ.cap3_crit, EQ.cap4_id, C4.nom_cap, EQ.cap4_pp, EQ.cap4_pre, EQ.cap4_puiss, EQ.cap4_crit FROM Equipes as E LEFT JOIN Users AS U ON E.user_id = U.id_user LEFT JOIN equipiers AS EQ ON E.id_equipe = EQ.equipe_id LEFT JOIN Pokemons AS P ON EQ.pok_id = P.id_pok LEFT JOIN Natures AS N ON EQ.nature_id = N.id_nature LEFT JOIN Talents AS T ON EQ.talent_id = T.id_talent LEFT JOIN Objets AS O ON EQ.objet_id = O.id_objet LEFT JOIN Capacites AS C ON EQ.cap1_id = C.id_cap LEFT JOIN Capacites AS C2 ON EQ.cap2_id = C2.id_cap LEFT JOIN Capacites AS C3 ON EQ.cap3_id = C3.id_cap LEFT JOIN Capacites AS C4 ON EQ.cap4_id = C4.id_cap;";
                //requestGetEquipes.CommandText = "SELECT * FROM Equipes WHERE userId = @UserId";
                //requestGetEquipes.Parameters.AddWithValue("@UserId", SessionManager.Instance.UserId);

                using (SqlDataReader equipes = requestGetEquipes.ExecuteReader())
                {
                    while (equipes.Read())
                    {
                    MDate creationDate = null;
                    if (!equipes.IsDBNull(2))
                    {
                        DateTime idDate = DateTime.Parse($"{equipes[2]}");
                        creationDate = new MDate(idDate);
                    }
                    MUser user = null;
                    if(!equipes.IsDBNull(3))
                    {
                        int idUser = equipes.IsDBNull(3) ? 0 : equipes.GetInt32(3);
                        string pseudoUser = equipes.IsDBNull(4) ? "" : $"{equipes[4]}";
                        user = new MUser(idUser, pseudoUser);
                    }

                    ObservableCollection<MEquipier> equipiers = new ObservableCollection<MEquipier>();

                    for (int i = 5; i <= 19; i++) 
                    {
                        MEquipier equipier = null;
                        if (!equipes.IsDBNull(5))
                        {
                            int idEquipier = equipes.GetInt32(5);
                            string imgEquipier = $"{equipes[6]}";
                            string nomEquipier = $"{equipes[7]}";
                            int pvEquipier = equipes.GetInt32(8);
                            int attEquipier = equipes.GetInt32(9);
                            int defEquipier = equipes.GetInt32(10);
                            int attSpeEquipier = equipes.GetInt32(11);
                            int defSpeEquipier = equipes.GetInt32(12);
                            int vitEquipier = equipes.GetInt32(13);
                            string surnomEquipier = $"{equipes[14]}";
                            int nivEquipier = equipes.GetInt32(15);
                            int esquiveEquipier = equipes.GetInt32(16);
                            int evEquipier = equipes.GetInt32(17);
                            int ivEquipier = equipes.GetInt32(18);
                            int bonhEquipier = equipes.GetInt32(19);

                          

                            if (equipier != null)
                            {
                                equipiers.Add(equipier);
                            }
                        }
                    }

                    MEquipe uneEquipe = new MEquipe(
                            equipes.GetInt32(0),
                            $"{equipes[1]}",    
                            user, 
                            creationDate,
                            equipiers
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
