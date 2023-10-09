using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

            try
            {
                SqlCommand requestGetEquipiers = bddTool.GetRequest();
                requestGetEquipiers.CommandText = "SELECT EQ.pok_id, P.pok_img, P.nom_fra_pok, EQ.pv, EQ.att, EQ.def, EQ.att_spe, EQ.def_spe, EQ.vit, P.legendaire, P.shiny, P.mega, P.gigamax, P.fabuleux, EQ.surnom, EQ.niveau, EQ.esquive, EQ.ev, EQ.iv, EQ.niv_bonheur, EQ.nature_id, N.nom_nature, EQ.talent_id, T.nom_talent, EQ.objet_id, O.nom_objet, EQ.cap1_id, C.nom_cap, EQ.cap1_pp, EQ.cap1_pre, EQ.cap1_puiss, EQ.cap1_crit, EQ.cap2_id, C2.nom_cap, EQ.cap2_pp, EQ.cap2_pre, EQ.cap2_puiss, EQ.cap2_crit, EQ.cap3_id, C3.nom_cap, EQ.cap3_pp, EQ.cap3_pre, EQ.cap3_puiss, EQ.cap3_crit, EQ.cap4_id, C4.nom_cap, EQ.cap4_pp, EQ.cap4_pre, EQ.cap4_puiss, EQ.cap4_crit, EQ.equipe_id FROM equipiers AS EQ LEFT JOIN Pokemons AS P ON EQ.pok_id = P.id_pok LEFT JOIN Natures AS N ON EQ.nature_id = N.id_nature LEFT JOIN Talents AS T ON EQ.talent_id = T.id_talent LEFT JOIN Objets AS O ON EQ.objet_id = O.id_objet LEFT JOIN Capacites AS C ON EQ.cap1_id = C.id_cap LEFT JOIN Capacites AS C2 ON EQ.cap2_id = C2.id_cap LEFT JOIN Capacites AS C3 ON EQ.cap3_id = C3.id_cap LEFT JOIN Capacites AS C4 ON EQ.cap4_id = C4.id_cap;";


                using (SqlDataReader equipiers = requestGetEquipiers.ExecuteReader())
                {
                    while (equipiers.Read())
                    {
                        ObservableCollection<MCapacite> capacites = new ObservableCollection<MCapacite>();
                        for (int i = 26; i <= 49; i += 6)
                        {
                            if (!equipiers.IsDBNull(i))
                            {
                                int idCapacite = equipiers.GetInt32(i);
                                string nomCapacite = equipiers.IsDBNull(i + 1) ? "" : $"{equipiers[i + 1]}";
                                int pp = equipiers.IsDBNull(i + 2) ? 0 : equipiers.GetInt32(i + 2);
                                int pre = equipiers.IsDBNull(i + 3) ? 0 : equipiers.GetInt32(i + 3);
                                int puiss = equipiers.IsDBNull(i + 4) ? 0 : equipiers.GetInt32(i + 4);
                                int crit = equipiers.IsDBNull(i + 5) ? 0 : equipiers.GetInt32(i + 5);
                                MCapacite capacite = new MCapacite(idCapacite, nomCapacite, pp, pre, puiss, crit);
                                capacites.Add(capacite);
                            }
                        }
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
                            objetTenu,
                            capacites,
                            equipiers.GetInt32(50)
                            );

                        observableMEquipiers.Add(unEquipier);
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

            return observableMEquipiers;
        }

        public ObservableCollection<MEquipe> GetAllEquipes()
        {
            ObservableCollection<MEquipier> equipiers = GetEquipiers();

            bddTool.CheckConnexion();

            ObservableCollection<MEquipe> observableMEquipes = new ObservableCollection<MEquipe>();

            //try
            //{
                SqlCommand requestGetEquipes = bddTool.GetRequest();
            //    requestGetEquipes.CommandText = "SELECT E.id_equipe, E.nom_equipe, E.date_id, U.id_user, U.pseudo, EQ.pok_id, P.pok_img, P.nom_fra_pok, EQ.pv, EQ.att, EQ.def, EQ.att_spe, EQ.def_spe, EQ.vit, EQ.surnom, EQ.niveau, EQ.esquive, EQ.ev, EQ.iv, EQ.niv_bonheur, EQ.nature_id, N.nom_nature, EQ.talent_id, T.nom_talent, EQ.objet_id, O.nom_objet, EQ.cap1_id, C.nom_cap, EQ.cap1_pp, EQ.cap1_pre, EQ.cap1_puiss, EQ.cap1_crit, EQ.cap2_id, C2.nom_cap, EQ.cap2_pp, EQ.cap2_pre, EQ.cap2_puiss, EQ.cap2_crit, EQ.cap3_id, C3.nom_cap, EQ.cap3_pp, EQ.cap3_pre, EQ.cap3_puiss, EQ.cap3_crit, EQ.cap4_id, C4.nom_cap, EQ.cap4_pp, EQ.cap4_pre, EQ.cap4_puiss, EQ.cap4_crit FROM Equipes as E LEFT JOIN Users AS U ON E.user_id = U.id_user LEFT JOIN equipiers AS EQ ON E.id_equipe = EQ.equipe_id LEFT JOIN Pokemons AS P ON EQ.pok_id = P.id_pok LEFT JOIN Natures AS N ON EQ.nature_id = N.id_nature LEFT JOIN Talents AS T ON EQ.talent_id = T.id_talent LEFT JOIN Objets AS O ON EQ.objet_id = O.id_objet LEFT JOIN Capacites AS C ON EQ.cap1_id = C.id_cap LEFT JOIN Capacites AS C2 ON EQ.cap2_id = C2.id_cap LEFT JOIN Capacites AS C3 ON EQ.cap3_id = C3.id_cap LEFT JOIN Capacites AS C4 ON EQ.cap4_id = C4.id_cap;";
            requestGetEquipes.CommandText = "SELECT E.id_equipe, E.nom_equipe, E.date_id, U.id_user, U.pseudo FROM Equipes as E LEFT JOIN Users AS U ON E.user_id = U.id_user LEFT JOIN equipiers AS EQ ON E.id_equipe = EQ.equipe_id GROUP BY E.id_equipe,  E.nom_equipe, E.date_id, U.id_user, U.pseudo";
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

        public void Add(MEquipier nouvelEquipier)
        {
            bddTool.CheckConnexion();

            SqlCommand requestAddEquipier = bddTool.GetRequest();
            requestAddEquipier.CommandText = "INSERT INTO equipiers (equipe_id,  talent_id,  pok_id,  cap1_id,  cap2_id,  cap3_id,  cap4_id,  objet_id,  nature_id,  surnom,  niveau,  niv_bonheur,  pv,  ev,  iv,  att,  def,  att_spe,  def_spe,  vit,  esquive,  cap1_pp,  cap1_puiss,  cap1_pre,  cap1_crit,  cap2_pp,  cap2_puiss,  cap2_pre,  cap2_crit,  cap3_pp,  cap3_puiss,  cap3_pre,  cap3_crit,  cap4_pp,  cap4_puiss,  cap4_pre,  cap4_crit) VALUES (  @equipe_id,  @talent_id,  @pok_id,  @cap1_id,  @cap2_id,  @cap3_id,  @cap4_id,  @objet_id,  @nature_id,  @surnom,  @niveau,  @niv_bonheur,  @pv,  @ev,  @iv,  @att,  @def,  @att_spe,  @def_spe,  @vit,  @esquive,  @cap1_pp,  @cap1_puiss,  @cap1_pre,  @cap1_crit,  @cap2_pp,  @cap2_puiss,  @cap2_pre,  @cap2_crit,  @cap3_pp,  @cap3_puiss,  @cap3_pre,  @cap3_crit,  @cap4_pp,  @cap4_puiss,  @cap4_pre,  @cap4_crit);";

            requestAddEquipier.Parameters.Add("@equipe_id", SqlDbType.Int).Value = nouvelEquipier.EquipeId;
            requestAddEquipier.Parameters.Add("@talent_id", SqlDbType.Int).Value = nouvelEquipier.TalentEquipier.IdTalent;
            requestAddEquipier.Parameters.Add("@pok_id", SqlDbType.Int).Value = nouvelEquipier.IdPokemon;
            requestAddEquipier.Parameters.Add("@cap1_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].IdCapacite;
            requestAddEquipier.Parameters.Add("@cap2_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].IdCapacite;
            requestAddEquipier.Parameters.Add("@cap3_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].IdCapacite;
            requestAddEquipier.Parameters.Add("@cap4_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].IdCapacite;
            requestAddEquipier.Parameters.Add("@objet_id", SqlDbType.Int).Value = nouvelEquipier.ObjetEquipier.IdObjet;
            requestAddEquipier.Parameters.Add("@nature_id", SqlDbType.Int).Value = nouvelEquipier.Nature.IdNature;
            requestAddEquipier.Parameters.Add("@surnom", SqlDbType.VarChar).Value = nouvelEquipier.SurnomEquipier;
            requestAddEquipier.Parameters.Add("@niveau", SqlDbType.Int).Value = nouvelEquipier.NiveauEquipier;
            requestAddEquipier.Parameters.Add("@niv_bonheur", SqlDbType.Int).Value = nouvelEquipier.NiveauBonheur;
            requestAddEquipier.Parameters.Add("@pv", SqlDbType.Int).Value = nouvelEquipier.BasePV;
            requestAddEquipier.Parameters.Add("@ev", SqlDbType.Int).Value = nouvelEquipier.Ev;
            requestAddEquipier.Parameters.Add("@iv", SqlDbType.Int).Value = nouvelEquipier.Iv;
            requestAddEquipier.Parameters.Add("@att", SqlDbType.Int).Value = nouvelEquipier.BaseAttaque;
            requestAddEquipier.Parameters.Add("@def", SqlDbType.Int).Value = nouvelEquipier.BaseDefense;
            requestAddEquipier.Parameters.Add("@att_spe", SqlDbType.Int).Value = nouvelEquipier.BaseAttSpe;
            requestAddEquipier.Parameters.Add("@def_spe", SqlDbType.Int).Value = nouvelEquipier.BaseDefSpe;
            requestAddEquipier.Parameters.Add("@vit", SqlDbType.Int).Value = nouvelEquipier.BaseVit;
            requestAddEquipier.Parameters.Add("@esquive", SqlDbType.Int).Value = nouvelEquipier.EsquiveEquipier;
            requestAddEquipier.Parameters.Add("@cap1_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BasePp;
            requestAddEquipier.Parameters.Add("@cap1_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BasePuiss;
            requestAddEquipier.Parameters.Add("@cap1_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BasePre;
            requestAddEquipier.Parameters.Add("@cap1_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BaseCrit;
            requestAddEquipier.Parameters.Add("@cap2_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BasePp;
            requestAddEquipier.Parameters.Add("@cap2_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BasePuiss;
            requestAddEquipier.Parameters.Add("@cap2_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BasePre;
            requestAddEquipier.Parameters.Add("@cap2_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BaseCrit;
            requestAddEquipier.Parameters.Add("@cap3_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BasePp;
            requestAddEquipier.Parameters.Add("@cap3_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BasePuiss;
            requestAddEquipier.Parameters.Add("@cap3_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BasePre;
            requestAddEquipier.Parameters.Add("@cap3_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BaseCrit;
            requestAddEquipier.Parameters.Add("@cap4_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BasePp;
            requestAddEquipier.Parameters.Add("@cap4_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BasePuiss;
            requestAddEquipier.Parameters.Add("@cap4_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BasePre;
            requestAddEquipier.Parameters.Add("@cap4_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BaseCrit;

            int result = requestAddEquipier.ExecuteNonQuery();

            bddTool.CloseConnexion();
        }

    }
}
