using PokeStat;
using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PokeStat.Repositories
{
    public class RepPokemon : IRepository<MSpecimen>
    {
        public BddTool bddTool;

        public RepPokemon()
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

        public List<MSpecimen> GetAll()
        {
            bddTool.CheckConnexion();

            List<MSpecimen> ListMSpecimens = new List<MSpecimen>();

            SqlCommand RequestGetPokemons = bddTool.GetRequest();
            RequestGetPokemons.CommandText = "SELECT p.id_pok, P.pok_img,  P.nom_fra_pok, P.nom_eng_pok,  P.num_pok, P.taille_pok, P.poids_pok, P.base_hp, P.base_att, P.base_def, P.base_sp_att, P.base_sp_def, P.base_vit, P.legendaire, P.shiny, P.mega, P.gigamax, P.fabuleux, P2.id_pok as evo_de_id, P2.pok_img as img_evo, P2.nom_fra_pok as nom_evo, G.id_gen, G.nom_gen FROM Pokemons AS P LEFT JOIN Generations as G ON P.gen_id = G.id_gen LEFT JOIN Pokemons P2 ON P.evo_de_id = P2.id_pok";

            using (SqlDataReader pokemons = RequestGetPokemons.ExecuteReader())
            {
                while (pokemons.Read())
                {
                    decimal taille = pokemons.IsDBNull(5) ? 0.0m : pokemons.GetDecimal(5);
                    decimal poids = pokemons.IsDBNull(6) ? 0.0m : pokemons.GetDecimal(6); 

                    MSpecimen evolution = null;
                    if (!pokemons.IsDBNull(18)) // Vérifier si la colonne id_evo peut être nulle
                    {
                        int idEvo = pokemons.IsDBNull(18) ? 0 : pokemons.GetInt32(18);
                        string imgEvo = pokemons.IsDBNull(19) ? "" : $"{pokemons[19]}";
                        string nomEvo = pokemons.IsDBNull(20) ? "" : $"{pokemons[20]}";
                        evolution = new MSpecimen(idEvo, imgEvo, nomEvo);
                    }


                    MGeneration gen = null;
                    if (!pokemons.IsDBNull(21))
                    {
                        int idGen = pokemons.IsDBNull(21) ? 0 : pokemons.GetInt32(21);
                        string nomGen = pokemons.IsDBNull(22) ? "" : $"{pokemons[22]}";
                        gen = new MGeneration(idGen, nomGen);
                    }
                    int base_hp = pokemons.IsDBNull(7) ? 0 : pokemons.GetInt32(7);
                    int base_att = pokemons.IsDBNull(8) ? 0 : pokemons.GetInt32(8);
                    int base_def = pokemons.IsDBNull(9) ? 0 : pokemons.GetInt32(9);
                    int base_sp_att = pokemons.IsDBNull(10) ? 0 : pokemons.GetInt32(10);
                    int base_sp_def = pokemons.IsDBNull(11) ? 0 : pokemons.GetInt32(11);
                    int base_vit = pokemons.IsDBNull(12) ? 0 : pokemons.GetInt32(12);


                    MSpecimen unPokemon = new MSpecimen(
                        pokemons.GetInt32(0),
                        $"{pokemons[1]}",
                        $"{pokemons[2]}",
                        $"{pokemons[3]}",
                        $"{pokemons[4]}",
                        taille,
                        poids,
                        base_hp,
                        base_att,
                        base_def,
                        base_sp_att,
                        base_sp_def,
                        base_vit,
                        Convert.ToBoolean(pokemons.GetValue(13)),
                        Convert.ToBoolean(pokemons.GetValue(14)),
                        Convert.ToBoolean(pokemons.GetValue(15)),
                        Convert.ToBoolean(pokemons.GetValue(16)),
                        Convert.ToBoolean(pokemons.GetValue(17)),
                        evolution,
                        gen
                    );

                    ListMSpecimens.Add(unPokemon);
                }
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return ListMSpecimens;
        }

        public MSpecimen GetSpecimen(int idPokemon)
        {
            bddTool.CheckConnexion();

            MSpecimen Specimen = null;

            SqlCommand RequestGetSpecimen = bddTool.GetRequest();
            RequestGetSpecimen.CommandText = "SELECT P.id_pok, P.base_hp, P.base_att, P.base_def, P.base_sp_att, P.base_sp_def, P.base_vit FROM Pokemons AS P WHERE P.id_pok = @id_pok;";
            RequestGetSpecimen.Parameters.Add("@id_pok", SqlDbType.Int).Value = idPokemon;

            using (SqlDataReader specimen = RequestGetSpecimen.ExecuteReader())
            {
                while (specimen.Read())
                {        
                    Specimen = new MSpecimen(
                        specimen.GetInt32(0),
                         specimen.GetInt32(1),
                         specimen.GetInt32(2),
                         specimen.GetInt32(3),
                         specimen.GetInt32(4),
                         specimen.GetInt32(5),
                         specimen.GetInt32(6)
                    );
                }
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return Specimen;
        }

        public List<MCapacite> GetPoolCapacite(int idEquipier)
        {
            bddTool.CheckConnexion();

            List<MCapacite> ListMCapacites = new List<MCapacite>();

            SqlCommand RequestGetCapacites = bddTool.GetRequest();
            RequestGetCapacites.CommandText = "SELECT P.id_pok, C.id_cap, C.nom_cap FROM Pokemons_Capacites AS CS LEFT JOIN Pokemons AS P ON CS.pok_id = P.id_pok LEFT JOIN Capacites AS C ON CS.cap_id = C.id_cap WHERE P.id_pok = @id_pok;";
            
            SqlParameter idPok = RequestGetCapacites.Parameters.Add("@id_pok", SqlDbType.Int);

            idPok.Value = idEquipier;
            
            using (SqlDataReader capacites = RequestGetCapacites.ExecuteReader())
            {
                while (capacites.Read())
                {
                    MCapacite uneCapacite = new MCapacite(
                      capacites.GetInt32(1),
                      $"{capacites[2]}"
                     );
                    ListMCapacites.Add(uneCapacite);
                }
            }

            bddTool.CloseConnexion();
            return ListMCapacites;
        }

        public List<MTalent> GetPoolTalent(int idEquipier)
        {
            bddTool.CheckConnexion();

            List<MTalent> ListMTalents = new List<MTalent>();

            SqlCommand RequestGetTalents = bddTool.GetRequest();
            RequestGetTalents.CommandText = "SELECT P.id_pok, PT.talent_id, T.nom_talent FROM Pokemons_Talents AS PT LEFT JOIN Pokemons AS P ON PT.pok_id = P.id_pok LEFT JOIN Talents AS T ON PT.talent_id = T.id_talent WHERE P.id_pok = @id_pok;";

            SqlParameter idPok = RequestGetTalents.Parameters.Add("@id_pok", SqlDbType.Int);

            idPok.Value = idEquipier;

            using (SqlDataReader talents = RequestGetTalents.ExecuteReader())
            {
                while (talents.Read())
                {
                    MTalent unTalent = new MTalent(
                      talents.GetInt32(1),
                      $"{talents[2]}"
                     );
                    ListMTalents.Add(unTalent);
                }
            }

            bddTool.CloseConnexion();
            return ListMTalents;
        }

        public void Add(MSpecimen entreePokemon)
        {
            if (entreePokemon is MSpecimen nouveauPokemon)
            {
                int idGen = nouveauPokemon.Gen.IdGen;

                Add(nouveauPokemon, idGen);
            }
            else
            {
                throw new ArgumentException("L'entrée n'est pas de type MSpecimen");
            }
        }


        public void Add(MSpecimen nouveauPokemon, int idGen)
        {
            bddTool.CheckConnexion();

            try
            {

                SqlCommand RequestAddPokemon = bddTool.GetRequest();
                RequestAddPokemon.CommandText = "INSERT INTO Pokemons (nom_eng_pok, nom_fra_pok, num_pok, taille_pok, poids_pok, base_hp, base_att, base_def, base_sp_att, base_sp_def, base_vit, legendaire, shiny, pok_img, id_evo, id_gen) VALUES (@nom_eng_pok, @nom_fra_pok, @num_pok, @taille_pok, @poids_pok, @base_hp, @base_att, @base_def, @base_sp_att, @base_sp_def, @base_vit, @legendaire, @shiny, @pok_img, @evo_id, @gen_id)";

            
                SqlParameter name = RequestAddPokemon.Parameters.Add("@nom_eng_pok", SqlDbType.VarChar);         
                SqlParameter nom = RequestAddPokemon.Parameters.Add("@nom_fra_pok", SqlDbType.VarChar);
                SqlParameter num = RequestAddPokemon.Parameters.Add("@num_pok", SqlDbType.VarChar);
                SqlParameter taille = RequestAddPokemon.Parameters.Add("@taille_pok", SqlDbType.Decimal);
                SqlParameter poids = RequestAddPokemon.Parameters.Add("@poids_pok", SqlDbType.Int);
                SqlParameter hp = RequestAddPokemon.Parameters.Add("@base_hp", SqlDbType.Int);               
                SqlParameter att = RequestAddPokemon.Parameters.Add("@base_att", SqlDbType.Int);              
                SqlParameter def = RequestAddPokemon.Parameters.Add("@base_def", SqlDbType.Int);
                SqlParameter sp_att = RequestAddPokemon.Parameters.Add("@base_sp_att", SqlDbType.Int);              
                SqlParameter sp_def = RequestAddPokemon.Parameters.Add("@base_sp_def", SqlDbType.Int);               
                SqlParameter vit = RequestAddPokemon.Parameters.Add("@base_vit", SqlDbType.Int);
                SqlParameter legendaire = RequestAddPokemon.Parameters.Add("@legendaire", SqlDbType.Bit);             
                SqlParameter shiny = RequestAddPokemon.Parameters.Add("@shiny", SqlDbType.Bit);
                SqlParameter img = RequestAddPokemon.Parameters.Add("@pok_img", SqlDbType.VarChar);                   
                SqlParameter idEvo = RequestAddPokemon.Parameters.Add("@evo_id", SqlDbType.Int);
                SqlParameter Gen = RequestAddPokemon.Parameters.Add("@gen_id", SqlDbType.Int);


                name.Value = nouveauPokemon.NomEngPokemon;
                nom.Value = nouveauPokemon.NomFraPokemon;
                num.Value = nouveauPokemon.NumPokemon;
                taille.Value = nouveauPokemon.TaillePokemon;
                poids.Value = nouveauPokemon.PoidsPokemon;
                hp.Value = nouveauPokemon.BasePV;
                att.Value = nouveauPokemon.BaseAttaque;
                def.Value = nouveauPokemon.BaseDefense;
                sp_att.Value = nouveauPokemon.BaseAttSpe;
                sp_def.Value = nouveauPokemon.BaseDefSpe;
                vit.Value = nouveauPokemon.BaseVit;
                legendaire.Value = nouveauPokemon.Legendaire;
                shiny.Value = nouveauPokemon.Shiny;
                img.Value = nouveauPokemon.CheminImgPokemon;
                idEvo.Value = (object)nouveauPokemon.Evolution?.IdPokemon ?? DBNull.Value;
                Gen.Value = idGen;

                int result = RequestAddPokemon.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de l'ajout du type : " + ex.Message);
            }


            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }

        public void AddTypePokemon(int IdType, int IdPokemon)
        {
            bddTool.CheckConnexion();

            SqlCommand RequestAddTypePokemon = bddTool.GetRequest();
            RequestAddTypePokemon.CommandText = "INSERT INTO Pokemons_Types_Generations (type_id, pok_id, emplac) VALUES (@type_id, @pok_id, @emplac)";

            SqlParameter idType = RequestAddTypePokemon.Parameters.Add("@type_id", SqlDbType.Int);
            SqlParameter idPok = RequestAddTypePokemon.Parameters.Add("@pok_id", SqlDbType.Int);
            SqlParameter emplac = RequestAddTypePokemon.Parameters.Add("@emplac", SqlDbType.TinyInt);
            idType.Value = IdType;
            idPok.Value = IdPokemon;
            emplac.Value = 3;

            int result = RequestAddTypePokemon.ExecuteNonQuery();
       

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }

        public void Update(MSpecimen MModele)
        {
        }


        public void Delete(int idMModele)
        {
        }
    }
}
