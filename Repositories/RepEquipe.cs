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
                        MEquipier equipierParDefaut = new MEquipier(
                            IdPokemon: 0,
                            CheminImgPokemon: "0.png",
                            NomFraPokemon: "Nouveau",
                            BasePV: 0,
                            BaseAttaque: 0,
                            BaseDefense: 0,
                            BaseAttSpe: 0,
                            BaseDefSpe: 0,
                            BaseVit: 0,
                            Legendaire: false,
                            Shiny: false,
                            Mega: false,
                            Giga: false,
                            Fab: false,
                            SurnomEquipier: "",
                            NiveauEquipier: 0,
                            EsquiveEquipier: 0,
                            NiveauBonheur: 0,
                            Ev: 0,
                            Iv: 0,
                            Nature: null,
                            TalentEquipier: null,
                            ObjetEquipier: null,
                            SetCapacites: new ObservableCollection<MCapacite>(),
                            EquipeId: equipiers.GetInt32(50),
                            EquipierOrigine: null                  
                            );



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
                            equipiers.GetInt32(50),
                            equipierParDefaut
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

          public MEquipier GetEquipier(MEquipier equipier)
          {
            bddTool.CheckConnexion();
            MEquipier Equipier = null;
            try
            {
                SqlCommand requestGetEquipier = bddTool.GetRequest();
                requestGetEquipier.CommandText = "SELECT EQ.pok_id, P.pok_img, P.nom_fra_pok, EQ.pv, EQ.att, EQ.def, EQ.att_spe, EQ.def_spe, EQ.vit, P.legendaire, P.shiny, P.mega, P.gigamax, P.fabuleux, EQ.surnom, EQ.niveau, EQ.esquive, EQ.ev, EQ.iv, EQ.niv_bonheur, EQ.nature_id, N.nom_nature, EQ.talent_id, T.nom_talent, EQ.objet_id, O.nom_objet, EQ.cap1_id, C.nom_cap, EQ.cap1_pp, EQ.cap1_pre, EQ.cap1_puiss, EQ.cap1_crit, EQ.cap2_id, C2.nom_cap, EQ.cap2_pp, EQ.cap2_pre, EQ.cap2_puiss, EQ.cap2_crit, EQ.cap3_id, C3.nom_cap, EQ.cap3_pp, EQ.cap3_pre, EQ.cap3_puiss, EQ.cap3_crit, EQ.cap4_id, C4.nom_cap, EQ.cap4_pp, EQ.cap4_pre, EQ.cap4_puiss, EQ.cap4_crit, EQ.equipe_id FROM equipiers AS EQ LEFT JOIN Pokemons AS P ON EQ.pok_id = P.id_pok LEFT JOIN Natures AS N ON EQ.nature_id = N.id_nature LEFT JOIN Talents AS T ON EQ.talent_id = T.id_talent LEFT JOIN Objets AS O ON EQ.objet_id = O.id_objet LEFT JOIN Capacites AS C ON EQ.cap1_id = C.id_cap LEFT JOIN Capacites AS C2 ON EQ.cap2_id = C2.id_cap LEFT JOIN Capacites AS C3 ON EQ.cap3_id = C3.id_cap LEFT JOIN Capacites AS C4 ON EQ.cap4_id = C4.id_cap WHERE equipe_id = @equipeIdModifier AND talent_id = @talentId AND pok_id = @pokIdOrigine AND cap1_id = @cap1IdOrigine AND cap2_id = @cap2IdOrigine AND cap3_id = @cap3IdOrigine AND cap4_id = @cap4IdOrigine AND objet_id = @objetIdOrigine AND nature_id = @natureIdOrigine ;";

                requestGetEquipier.Parameters.Add("@equipeIdModifier", SqlDbType.Int).Value = equipier.EquipeId;
                requestGetEquipier.Parameters.Add("@talentId", SqlDbType.Int).Value = equipier.TalentEquipier.IdTalent;
                requestGetEquipier.Parameters.Add("@pokIdOrigine", SqlDbType.Int).Value = equipier.IdPokemon;
                requestGetEquipier.Parameters.Add("@cap1IdOrigine", SqlDbType.Int).Value = equipier.SetCapacites[0].IdCapacite;
                requestGetEquipier.Parameters.Add("@cap2IdOrigine", SqlDbType.Int).Value = equipier.SetCapacites[1].IdCapacite;
                requestGetEquipier.Parameters.Add("@cap3IdOrigine", SqlDbType.Int).Value = equipier.SetCapacites[2].IdCapacite;
                requestGetEquipier.Parameters.Add("@cap4IdOrigine", SqlDbType.Int).Value = equipier.SetCapacites[3].IdCapacite;
                requestGetEquipier.Parameters.Add("@objetIdOrigine", SqlDbType.Int).Value = equipier.ObjetEquipier.IdObjet;
                requestGetEquipier.Parameters.Add("@natureIdOrigine", SqlDbType.Int).Value = equipier.Nature.IdNature;

                using (SqlDataReader equipiers = requestGetEquipier.ExecuteReader())
                {
                    if (equipiers.Read())
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
                            int idNature = equipiers.GetInt32(20);
                            string nomNature = equipiers.IsDBNull(21) ? "" : $"{equipiers[21]}";
                            natureEquipier = new MNature(idNature, nomNature);
                        }

                        MTalent talentEquipier = null;
                        if (!equipiers.IsDBNull(22))
                        {
                            int idTalent = equipiers.GetInt32(22);
                            string nomTalent = equipiers.IsDBNull(23) ? "" : $"{equipiers[23]}";
                            talentEquipier = new MTalent(idTalent, nomTalent);
                        }

                        MObjet objetTenu = null;
                        if (!equipiers.IsDBNull(24))
                        {
                            int idObjet = equipiers.GetInt32(24);
                            string nomObjet = equipiers.IsDBNull(25) ? "" : $"{equipiers[25]}";
                            objetTenu = new MObjet(idObjet, nomObjet);
                        }
                        MEquipier equipierOrigineDefaut = null;
                        Equipier = new MEquipier(
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
                            equipiers.GetInt32(50),
                            equipierOrigineDefaut
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération de l'équipier : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return Equipier;
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

            SqlCommand RequestUpdateEquipier = bddTool.GetRequest();
            RequestUpdateEquipier.CommandText = "INSERT INTO equipiers (equipe_id,  talent_id,  pok_id,  cap1_id,  cap2_id,  cap3_id,  cap4_id,  objet_id,  nature_id,  surnom,  niveau,  niv_bonheur,  pv,  ev,  iv,  att,  def,  att_spe,  def_spe,  vit,  esquive,  cap1_pp,  cap1_puiss,  cap1_pre,  cap1_crit,  cap2_pp,  cap2_puiss,  cap2_pre,  cap2_crit,  cap3_pp,  cap3_puiss,  cap3_pre,  cap3_crit,  cap4_pp,  cap4_puiss,  cap4_pre,  cap4_crit) VALUES (  @equipe_id,  @talent_id,  @pok_id,  @cap1_id,  @cap2_id,  @cap3_id,  @cap4_id,  @objet_id,  @nature_id,  @surnom,  @niveau,  @niv_bonheur,  @pv,  @ev,  @iv,  @att,  @def,  @att_spe,  @def_spe,  @vit,  @esquive,  @cap1_pp,  @cap1_puiss,  @cap1_pre,  @cap1_crit,  @cap2_pp,  @cap2_puiss,  @cap2_pre,  @cap2_crit,  @cap3_pp,  @cap3_puiss,  @cap3_pre,  @cap3_crit,  @cap4_pp,  @cap4_puiss,  @cap4_pre,  @cap4_crit);";

            RequestUpdateEquipier.Parameters.Add("@equipe_id", SqlDbType.Int).Value = nouvelEquipier.EquipeId;
            RequestUpdateEquipier.Parameters.Add("@talent_id", SqlDbType.Int).Value = nouvelEquipier.TalentEquipier.IdTalent;
            RequestUpdateEquipier.Parameters.Add("@pok_id", SqlDbType.Int).Value = nouvelEquipier.IdPokemon;
            RequestUpdateEquipier.Parameters.Add("@cap1_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@cap2_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@cap3_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@cap4_id", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@objet_id", SqlDbType.Int).Value = nouvelEquipier.ObjetEquipier.IdObjet;
            RequestUpdateEquipier.Parameters.Add("@nature_id", SqlDbType.Int).Value = nouvelEquipier.Nature.IdNature;
            RequestUpdateEquipier.Parameters.Add("@surnom", SqlDbType.VarChar).Value = nouvelEquipier.SurnomEquipier;
            RequestUpdateEquipier.Parameters.Add("@niveau", SqlDbType.Int).Value = nouvelEquipier.NiveauEquipier;
            RequestUpdateEquipier.Parameters.Add("@niv_bonheur", SqlDbType.Int).Value = nouvelEquipier.NiveauBonheur;
            RequestUpdateEquipier.Parameters.Add("@pv", SqlDbType.Int).Value = nouvelEquipier.BasePV;
            RequestUpdateEquipier.Parameters.Add("@ev", SqlDbType.Int).Value = nouvelEquipier.Ev;
            RequestUpdateEquipier.Parameters.Add("@iv", SqlDbType.Int).Value = nouvelEquipier.Iv;
            RequestUpdateEquipier.Parameters.Add("@att", SqlDbType.Int).Value = nouvelEquipier.BaseAttaque;
            RequestUpdateEquipier.Parameters.Add("@def", SqlDbType.Int).Value = nouvelEquipier.BaseDefense;
            RequestUpdateEquipier.Parameters.Add("@att_spe", SqlDbType.Int).Value = nouvelEquipier.BaseAttSpe;
            RequestUpdateEquipier.Parameters.Add("@def_spe", SqlDbType.Int).Value = nouvelEquipier.BaseDefSpe;
            RequestUpdateEquipier.Parameters.Add("@vit", SqlDbType.Int).Value = nouvelEquipier.BaseVit;
            RequestUpdateEquipier.Parameters.Add("@esquive", SqlDbType.Int).Value = nouvelEquipier.EsquiveEquipier;
            RequestUpdateEquipier.Parameters.Add("@cap1_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap1_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap1_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap1_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[0].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@cap2_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap2_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap2_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap2_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[1].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@cap3_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap3_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap3_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap3_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[2].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@cap4_pp", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap4_puiss", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap4_pre", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap4_crit", SqlDbType.Int).Value = nouvelEquipier.SetCapacites[3].BaseCrit;

            int result = RequestUpdateEquipier.ExecuteNonQuery();

            bddTool.CloseConnexion();
        }

        public void Delete(MEquipier equipierSupprimer)
        {
            bddTool.CheckConnexion();

            try
            {
                SqlCommand RequestDeleteEquipier = bddTool.GetRequest();
                RequestDeleteEquipier.CommandText = "DELETE FROM equipiers WHERE equipe_id = @equipeId " +
                                        "AND talent_id = @talentId " +
                                        "AND pok_id = @pokId " +
                                        "AND cap1_id = @cap1Id " +
                                        "AND cap2_id = @cap2Id " +
                                        "AND cap3_id = @cap3Id " +
                                        "AND cap4_id = @cap4Id " +
                                        "AND objet_id = @objetId " +
                                        "AND nature_id = @natureId";

                SqlParameter idEquipe = RequestDeleteEquipier.Parameters.Add("@equipeId", SqlDbType.Int);
                SqlParameter idTalent = RequestDeleteEquipier.Parameters.Add("@talentId", SqlDbType.Int);
                SqlParameter idPok = RequestDeleteEquipier.Parameters.Add("@pokId", SqlDbType.Int);
                SqlParameter idCap1 = RequestDeleteEquipier.Parameters.Add("@cap1Id", SqlDbType.Int);
                SqlParameter idCap2 = RequestDeleteEquipier.Parameters.Add("@cap2Id", SqlDbType.Int);
                SqlParameter idCap3 = RequestDeleteEquipier.Parameters.Add("@cap3Id", SqlDbType.Int);
                SqlParameter idCap4 = RequestDeleteEquipier.Parameters.Add("@cap4Id", SqlDbType.Int);
                SqlParameter idObjet = RequestDeleteEquipier.Parameters.Add("@objetId", SqlDbType.Int);
                SqlParameter idNature = RequestDeleteEquipier.Parameters.Add("@natureId", SqlDbType.Int);

                idEquipe.Value = equipierSupprimer.EquipeId;
                idTalent.Value = equipierSupprimer.TalentId;
                idPok.Value = equipierSupprimer.IdPokemon;
                idCap1.Value = equipierSupprimer.Cap1Id;
                idCap2.Value = equipierSupprimer.Cap2Id;
                idCap3.Value = equipierSupprimer.Cap3Id;
                idCap4.Value = equipierSupprimer.Cap4Id;
                idObjet.Value = equipierSupprimer.ObjetId;
                idNature.Value = equipierSupprimer.NatureId;

                int result = RequestDeleteEquipier.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la suppression du type : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }

        public void Update(MEquipier equipierModifier, MEquipier equipierOrigine)
        {
            bddTool.CheckConnexion();

            SqlCommand RequestUpdateEquipier = bddTool.GetRequest();
            RequestUpdateEquipier.CommandText = "UPDATE equipiers SET talent_id = @talent_id, pok_id = @pok_id, cap1_id = @cap1_id, cap2_id = @cap2_id, cap3_id = @cap3_id, cap4_id = @cap4_id, objet_id = @objet_id, nature_id = @nature_id, surnom = @surnom, niveau = @niveau, niv_bonheur = @niv_bonheur, pv = @pv, ev = @ev, iv = @iv, att = @att, def = @def, att_spe = @att_spe, def_spe = @def_spe, vit = @vit, esquive = @esquive, cap1_pp = @cap1_pp, cap1_puiss = @cap1_puiss, cap1_pre = @cap1_pre, cap1_crit = @cap1_crit, cap2_pp = @cap2_pp, cap2_puiss = @cap2_puiss, cap2_pre = @cap2_pre, cap2_crit = @cap2_crit, cap3_pp = @cap3_pp, cap3_puiss = @cap3_puiss, cap3_pre = @cap3_pre, cap3_crit = @cap3_crit, cap4_pp = @cap4_pp, cap4_puiss = @cap4_puiss, cap4_pre = @cap4_pre, cap4_crit = @cap4_crit WHERE equipe_id = @equipeIdModifier AND talent_id = @talentId AND pok_id = @pokIdOrigine AND cap1_id = @cap1IdOrigine AND cap2_id = @cap2IdOrigine AND cap3_id = @cap3IdOrigine AND cap4_id = @cap4IdOrigine AND objet_id = @objetIdOrigine AND nature_id = @natureIdOrigine";

            RequestUpdateEquipier.Parameters.Add("@talent_id", SqlDbType.Int).Value = equipierModifier.TalentEquipier.IdTalent;
            RequestUpdateEquipier.Parameters.Add("@pok_id", SqlDbType.Int).Value = equipierModifier.IdPokemon;
            RequestUpdateEquipier.Parameters.Add("@cap1_id", SqlDbType.Int).Value = equipierModifier.SetCapacites[0].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@cap2_id", SqlDbType.Int).Value = equipierModifier.SetCapacites[1].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@cap3_id", SqlDbType.Int).Value = equipierModifier.SetCapacites[2].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@cap4_id", SqlDbType.Int).Value = equipierModifier.SetCapacites[3].IdCapacite;
            RequestUpdateEquipier.Parameters.Add("@objet_id", SqlDbType.Int).Value = equipierModifier.ObjetEquipier.IdObjet;
            RequestUpdateEquipier.Parameters.Add("@nature_id", SqlDbType.Int).Value = equipierModifier.Nature.IdNature;
            RequestUpdateEquipier.Parameters.Add("@surnom", SqlDbType.VarChar).Value = equipierModifier.SurnomEquipier;
            RequestUpdateEquipier.Parameters.Add("@niveau", SqlDbType.Int).Value = equipierModifier.NiveauEquipier;
            RequestUpdateEquipier.Parameters.Add("@niv_bonheur", SqlDbType.Int).Value = equipierModifier.NiveauBonheur;
            RequestUpdateEquipier.Parameters.Add("@pv", SqlDbType.Int).Value = equipierModifier.BasePV;
            RequestUpdateEquipier.Parameters.Add("@ev", SqlDbType.Int).Value = equipierModifier.Ev;
            RequestUpdateEquipier.Parameters.Add("@iv", SqlDbType.Int).Value = equipierModifier.Iv;
            RequestUpdateEquipier.Parameters.Add("@att", SqlDbType.Int).Value = equipierModifier.BaseAttaque;
            RequestUpdateEquipier.Parameters.Add("@def", SqlDbType.Int).Value = equipierModifier.BaseDefense;
            RequestUpdateEquipier.Parameters.Add("@att_spe", SqlDbType.Int).Value = equipierModifier.BaseAttSpe;
            RequestUpdateEquipier.Parameters.Add("@def_spe", SqlDbType.Int).Value = equipierModifier.BaseDefSpe;
            RequestUpdateEquipier.Parameters.Add("@vit", SqlDbType.Int).Value = equipierModifier.BaseVit;
            RequestUpdateEquipier.Parameters.Add("@esquive", SqlDbType.Int).Value = equipierModifier.EsquiveEquipier;
            RequestUpdateEquipier.Parameters.Add("@cap1_pp", SqlDbType.Int).Value = equipierModifier.SetCapacites[0].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap1_puiss", SqlDbType.Int).Value = equipierModifier.SetCapacites[0].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap1_pre", SqlDbType.Int).Value = equipierModifier.SetCapacites[0].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap1_crit", SqlDbType.Int).Value = equipierModifier.SetCapacites[0].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@cap2_pp", SqlDbType.Int).Value = equipierModifier.SetCapacites[1].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap2_puiss", SqlDbType.Int).Value = equipierModifier.SetCapacites[1].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap2_pre", SqlDbType.Int).Value = equipierModifier.SetCapacites[1].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap2_crit", SqlDbType.Int).Value = equipierModifier.SetCapacites[1].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@cap3_pp", SqlDbType.Int).Value = equipierModifier.SetCapacites[2].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap3_puiss", SqlDbType.Int).Value = equipierModifier.SetCapacites[2].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap3_pre", SqlDbType.Int).Value = equipierModifier.SetCapacites[2].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap3_crit", SqlDbType.Int).Value = equipierModifier.SetCapacites[2].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@cap4_pp", SqlDbType.Int).Value = equipierModifier.SetCapacites[3].BasePp;
            RequestUpdateEquipier.Parameters.Add("@cap4_puiss", SqlDbType.Int).Value = equipierModifier.SetCapacites[3].BasePuiss;
            RequestUpdateEquipier.Parameters.Add("@cap4_pre", SqlDbType.Int).Value = equipierModifier.SetCapacites[3].BasePre;
            RequestUpdateEquipier.Parameters.Add("@cap4_crit", SqlDbType.Int).Value = equipierModifier.SetCapacites[3].BaseCrit;
            RequestUpdateEquipier.Parameters.Add("@equipeIdModifier", SqlDbType.Int).Value = equipierModifier.EquipeId;
            RequestUpdateEquipier.Parameters.Add("@talentId", SqlDbType.Int).Value = equipierOrigine.TalentId;
            RequestUpdateEquipier.Parameters.Add("@pokIdOrigine", SqlDbType.Int).Value = equipierOrigine.IdPokemon;
            RequestUpdateEquipier.Parameters.Add("@cap1IdOrigine", SqlDbType.Int).Value = equipierOrigine.Cap1Id;
            RequestUpdateEquipier.Parameters.Add("@cap2IdOrigine", SqlDbType.Int).Value = equipierOrigine.Cap2Id;
            RequestUpdateEquipier.Parameters.Add("@cap3IdOrigine", SqlDbType.Int).Value = equipierOrigine.Cap3Id;
            RequestUpdateEquipier.Parameters.Add("@cap4IdOrigine", SqlDbType.Int).Value = equipierOrigine.Cap4Id;
            RequestUpdateEquipier.Parameters.Add("@objetIdOrigine", SqlDbType.Int).Value = equipierOrigine.ObjetId;
            RequestUpdateEquipier.Parameters.Add("@natureIdOrigine", SqlDbType.Int).Value = equipierOrigine.NatureId;

            int result = RequestUpdateEquipier.ExecuteNonQuery();

            bddTool.CloseConnexion();
        }
    }
}
