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

        public ObservableCollection<MEquipier> GetEquipiers() 
        {
            bddTool.CheckConnexion();

            ObservableCollection<MEquipier> observableMEquipiers = new ObservableCollection<MEquipier>();

            //try
            //{
            SqlCommand requestGetEquipiers = bddTool.GetRequest();
            requestGetEquipiers.CommandText = "SELECT EQ.pok_id, P.pok_img, P.nom_fra_pok, EQ.pv, EQ.att, EQ.def, EQ.att_spe, EQ.def_spe, EQ.vit, EQ.surnom, EQ.niveau, EQ.esquive, EQ.ev, EQ.iv, EQ.niv_bonheur, EQ.nature_id, N.nom_nature, EQ.talent_id, T.nom_talent, EQ.objet_id, O.nom_objet, EQ.cap1_id, C.nom_cap, EQ.cap1_pp, EQ.cap1_pre, EQ.cap1_puiss, EQ.cap1_crit, EQ.cap2_id, C2.nom_cap, EQ.cap2_pp, EQ.cap2_pre, EQ.cap2_puiss, EQ.cap2_crit, EQ.cap3_id, C3.nom_cap, EQ.cap3_pp, EQ.cap3_pre, EQ.cap3_puiss, EQ.cap3_crit, EQ.cap4_id, C4.nom_cap, EQ.cap4_pp, EQ.cap4_pre, EQ.cap4_puiss, EQ.cap4_crit FROM equipiers AS EQ LEFT JOIN Pokemons AS P ON EQ.pok_id = P.id_pok LEFT JOIN Natures AS N ON EQ.nature_id = N.id_nature LEFT JOIN Talents AS T ON EQ.talent_id = T.id_talent LEFT JOIN Objets AS O ON EQ.objet_id = O.id_objet LEFT JOIN Capacites AS C ON EQ.cap1_id = C.id_cap LEFT JOIN Capacites AS C2 ON EQ.cap2_id = C2.id_cap LEFT JOIN Capacites AS C3 ON EQ.cap3_id = C3.id_cap LEFT JOIN Capacites AS C4 ON EQ.cap4_id = C4.id_cap;";


            using (SqlDataReader equipiers = requestGetEquipiers.ExecuteReader())
            {
                while (equipiers.Read())
                {
                    MNature natureEquipier = null;
                    if (!equipiers.IsDBNull(20))
                    {
                        int idNature = equipiers.IsDBNull(20) ? 0 : equipiers.GetInt32(20);
                        string nomNature = equipiers.IsDBNull(21) ? "" : $"{equipiers[21]}";
                        natureEquipier = new MNature(idNature, nomNature);
                    }

                    MTalent talentEquipier = null;
                    if (!equipiers.IsDBNull(22))
                    {
                        int idTalent = equipiers.IsDBNull(22) ? 0 : equipiers.GetInt32(22);
                        string nomTalent = equipiers.IsDBNull(23) ? "" : $"{equipiers[23]}";
                        talentEquipier = new MTalent(idTalent, nomTalent);
                    }

                    MObjet objetTenu = null;
                    if (!equipiers.IsDBNull(24))
                    {
                        int idObjet = equipiers.IsDBNull(24) ? 0 : equipiers.GetInt32(24);
                        string nomObjet = equipiers.IsDBNull(25) ? "" : $"{equipiers[25]}";
                        objetTenu = new MObjet(idObjet, nomObjet);
                    }
              

                    MEquipier unEquipier = new MEquipier(
                        equipiers.GetInt32(0),
                        $"{equipiers[1]}",
                        $"{equipiers[2]}",                    
                        equipiers.GetInt32(3),
                        equipiers.GetInt32(4),
                        equipiers.GetInt32(5),
                        equipiers.GetInt32(6),
                        equipiers.GetInt32(7),
                        equipiers.GetInt32(8),                       
                        Convert.ToBoolean(equipiers.GetValue(9)),
                        Convert.ToBoolean(equipiers.GetValue(10)),
                        Convert.ToBoolean(equipiers.GetValue(11)),
                        Convert.ToBoolean(equipiers.GetValue(12)),
                        Convert.ToBoolean(equipiers.GetValue(13)),
                        $"{equipiers[14]}",
                        equipiers.GetInt32(15),
                        equipiers.GetInt32(16),
                        equipiers.GetInt32(17),
                        equipiers.GetInt32(18),
                        equipiers.GetInt32(19),
                        natureEquipier, 
                        talentEquipier,
                        objetTenu
                        );

                    observableMEquipiers.Add(unEquipier);
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

            return observableMEquipiers;
        }

        public ObservableCollection<MEquipe> GetAllEquipes()
        {
            bddTool.CheckConnexion();

            ObservableCollection<MEquipe> observableMEquipes = new ObservableCollection<MEquipe>();

            //try
            //{
                SqlCommand requestGetEquipes = bddTool.GetRequest();
            //    requestGetEquipes.CommandText = "SELECT E.id_equipe, E.nom_equipe, E.date_id, U.id_user, U.pseudo, EQ.pok_id, P.pok_img, P.nom_fra_pok, EQ.pv, EQ.att, EQ.def, EQ.att_spe, EQ.def_spe, EQ.vit, EQ.surnom, EQ.niveau, EQ.esquive, EQ.ev, EQ.iv, EQ.niv_bonheur, EQ.nature_id, N.nom_nature, EQ.talent_id, T.nom_talent, EQ.objet_id, O.nom_objet, EQ.cap1_id, C.nom_cap, EQ.cap1_pp, EQ.cap1_pre, EQ.cap1_puiss, EQ.cap1_crit, EQ.cap2_id, C2.nom_cap, EQ.cap2_pp, EQ.cap2_pre, EQ.cap2_puiss, EQ.cap2_crit, EQ.cap3_id, C3.nom_cap, EQ.cap3_pp, EQ.cap3_pre, EQ.cap3_puiss, EQ.cap3_crit, EQ.cap4_id, C4.nom_cap, EQ.cap4_pp, EQ.cap4_pre, EQ.cap4_puiss, EQ.cap4_crit FROM Equipes as E LEFT JOIN Users AS U ON E.user_id = U.id_user LEFT JOIN equipiers AS EQ ON E.id_equipe = EQ.equipe_id LEFT JOIN Pokemons AS P ON EQ.pok_id = P.id_pok LEFT JOIN Natures AS N ON EQ.nature_id = N.id_nature LEFT JOIN Talents AS T ON EQ.talent_id = T.id_talent LEFT JOIN Objets AS O ON EQ.objet_id = O.id_objet LEFT JOIN Capacites AS C ON EQ.cap1_id = C.id_cap LEFT JOIN Capacites AS C2 ON EQ.cap2_id = C2.id_cap LEFT JOIN Capacites AS C3 ON EQ.cap3_id = C3.id_cap LEFT JOIN Capacites AS C4 ON EQ.cap4_id = C4.id_cap;";
            requestGetEquipes.CommandText = "SELECT E.id_equipe, E.nom_equipe, E.date_id, U.id_user, U.pseudo FROM Equipes as E LEFT JOIN Users AS U ON E.user_id = U.id_user LEFT JOIN equipiers AS EQ ON E.id_equipe = EQ.equipe_id ;";
            //requestGetEquipes.Parameters.AddWithValue("@UserId", SessionManager.Instance.UserId);

            ObservableCollection<MEquipier> equipiers = GetEquipiers();

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



                        MEquipe uneEquipe = new MEquipe(
                                equipes.GetInt32(0),
                                $"{equipes[1]}",
                                creationDate,
                                user,                             
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
