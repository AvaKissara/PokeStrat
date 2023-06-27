﻿using PokeStat;
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
    public class RepPokemon
    {
        private SqlConnection activeConnexion;

        public RepPokemon()
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

        public List<MPokemon> GetPokemons()
        {
            CheckConnexion();

            List<MPokemon> ListMPokemons = new List<MPokemon>();

            SqlCommand RequestGetPokemons = activeConnexion.CreateCommand();
            RequestGetPokemons.CommandText = "SELECT p.id_pok, P.pok_img, P.nom_eng_pok, P.nom_fra_pok,  P.num_pok, P.taille_pok, P.poids_pok, P.base_experience, P.base_hp, P.base_att, P.base_def, P.base_sp_att, P.base_sp_def, P.base_vit, P.legendaire, P.shiny, P2.id_pok as evo_id, P2.pok_img as img_evo, P2.nom_fra_pok as nom_evo, P.niv_evo, G.id_gen, G.nom_gen FROM Pokemons AS P \r\nLEFT JOIN Generations as G ON P.gen_id = G.id_gen \r\nLEFT JOIN Pokemons P2 ON P.evo_id = P2.id_pok\r\n";

            using (SqlDataReader pokemons = RequestGetPokemons.ExecuteReader())
            {
                while (pokemons.Read())
                {
                    decimal taille = pokemons.IsDBNull(5) ? 0.0m : pokemons.GetDecimal(5); // Récupérer la valeur de la taille ou utiliser 0.0m si elle est nulle
                    decimal poids = pokemons.IsDBNull(6) ? 0.0m : pokemons.GetDecimal(6); // Récupérer la valeur du poids ou utiliser 0.0m si elle est nulle

                    MPokemon evolution = null;
                    if (!pokemons.IsDBNull(16)) // Vérifier si la colonne id_evo peut être nulle
                    {
                        int idEvo = pokemons.IsDBNull(16) ? 0 : pokemons.GetInt32(16);
                        string imgEvo = pokemons.IsDBNull(17) ? "" : $"{pokemons[17]}";
                        string nomEvo = pokemons.IsDBNull(18) ? "" : $"{pokemons[18]}";
                        evolution = new MPokemon(idEvo, imgEvo, nomEvo);
                    }

                    int niveauEvo = pokemons.IsDBNull(19) ? 0 : pokemons.GetInt32(19);

                    MGeneration gen = null;
                    if (!pokemons.IsDBNull(20)) 
                    {
                        int idGen = pokemons.IsDBNull(20) ? 0 : pokemons.GetInt32(20);
                        string nomGen = pokemons.IsDBNull(21) ? "" : $"{pokemons[21]}";
                        int numGen = pokemons.IsDBNull(22) ? 0 : pokemons.GetInt32(22);
                        gen = new MGeneration(idGen, nomGen);
                    }

                    MPokemon unPokemon = new MPokemon(
                        pokemons.GetInt32(0),
                        $"{pokemons[1]}",
                        $"{pokemons[2]}",
                        $"{pokemons[3]}",
                        $"{pokemons[4]}",
                        taille,
                        poids,
                        pokemons.GetInt32(7),
                        pokemons.GetInt32(8),
                        pokemons.GetInt32(9),
                        pokemons.GetInt32(10),
                        pokemons.GetInt32(11),
                        pokemons.GetInt32(12),
                        pokemons.GetInt32(13),
                        Convert.ToBoolean(pokemons.GetValue(14)),
                        Convert.ToBoolean(pokemons.GetValue(15)),
                        evolution,
                        niveauEvo,
                        gen
                    );

                    ListMPokemons.Add(unPokemon);
                }
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMPokemons;
        }
        public void AddPokemon(MPokemon nouveauPokemon, int idGen)
        {
            CheckConnexion();

            try
            {

                SqlCommand RequestAddPokemon = activeConnexion.CreateCommand();
                RequestAddPokemon.CommandText = "INSERT INTO Pokemons (nom_eng_pok, nom_fra_pok, num_pok, taille_pok, poids_pok, base_experience, base_hp, base_att, base_def, base_sp_att, base_sp_def, base_vit, legendaire, shiny, pok_img, niv_evo, id_evo, id_gen) VALUES (@nom_eng_pok, @nom_fra_pok, @num_pok, @taille_pok, @poids_pok, @base_experience, @base_hp, @base_att, @base_def, @base_sp_att, @base_sp_def, @base_vit, @legendaire, @shiny, @pok_img, @niv_evo, @evo_id, @gen_id)";

            
                SqlParameter name = RequestAddPokemon.Parameters.Add("@nom_eng_pok", SqlDbType.VarChar);         
                SqlParameter nom = RequestAddPokemon.Parameters.Add("@nom_fra_pok", SqlDbType.VarChar);
                SqlParameter num = RequestAddPokemon.Parameters.Add("@num_pok", SqlDbType.VarChar);
                SqlParameter taille = RequestAddPokemon.Parameters.Add("@taille_pok", SqlDbType.Decimal);
                SqlParameter poids = RequestAddPokemon.Parameters.Add("@poids_pok", SqlDbType.Int);
                SqlParameter experience = RequestAddPokemon.Parameters.Add("@base_experience", SqlDbType.Int);
                SqlParameter hp = RequestAddPokemon.Parameters.Add("@base_hp", SqlDbType.Int);               
                SqlParameter att = RequestAddPokemon.Parameters.Add("@base_att", SqlDbType.Int);              
                SqlParameter def = RequestAddPokemon.Parameters.Add("@base_def", SqlDbType.Int);
                SqlParameter sp_att = RequestAddPokemon.Parameters.Add("@base_sp_att", SqlDbType.Int);              
                SqlParameter sp_def = RequestAddPokemon.Parameters.Add("@base_sp_def", SqlDbType.Int);               
                SqlParameter vit = RequestAddPokemon.Parameters.Add("@base_vit", SqlDbType.Int);
                SqlParameter legendaire = RequestAddPokemon.Parameters.Add("@legendaire", SqlDbType.Bit);             
                SqlParameter shiny = RequestAddPokemon.Parameters.Add("@shiny", SqlDbType.Bit);
                SqlParameter img = RequestAddPokemon.Parameters.Add("@pok_img", SqlDbType.VarChar);
                SqlParameter nivEvo = RequestAddPokemon.Parameters.Add("@niv_evo", SqlDbType.Int);                   
                SqlParameter idEvo = RequestAddPokemon.Parameters.Add("@evo_id", SqlDbType.Int);
                SqlParameter Gen = RequestAddPokemon.Parameters.Add("@gen_id", SqlDbType.Int);


                name.Value = nouveauPokemon.nomEngPokemon;
                nom.Value = nouveauPokemon.nomFraPokemon;
                num.Value = nouveauPokemon.numPokemon;
                taille.Value = nouveauPokemon.taillePokemon;
                poids.Value = nouveauPokemon.poidsPokemon;
                experience.Value = nouveauPokemon.baseXp;
                hp.Value = nouveauPokemon.pv;
                att.Value = nouveauPokemon.attaque;
                def.Value = nouveauPokemon.defense;
                sp_att.Value = nouveauPokemon.attSpe;
                sp_def.Value = nouveauPokemon.defSpe;
                vit.Value = nouveauPokemon.vitesse;
                legendaire.Value = nouveauPokemon.legendaire;
                shiny.Value = nouveauPokemon.shiny;
                img.Value = nouveauPokemon.cheminImgPokemon;
                nivEvo.Value = (object)nouveauPokemon.nivEvolution ?? DBNull.Value;
                idEvo.Value = (object)nouveauPokemon.evolution?.idPokemon ?? DBNull.Value;
                Gen.Value = idGen;

                int result = RequestAddPokemon.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de l'ajout du type : " + ex.Message);
            }


            // Fermeture de la connexion
            this.activeConnexion.Close();
        }

        public void AddTypePokemon(int IdType, int IdPokemon)
        {
            CheckConnexion();

            SqlCommand RequestAddTypePokemon = activeConnexion.CreateCommand();
            RequestAddTypePokemon.CommandText = "INSERT INTO pokemon_types (type_id, pok_id, emplac) VALUES (@type_id, @pok_id, @emplac)";

            SqlParameter idType = RequestAddTypePokemon.Parameters.Add("@type_id", SqlDbType.Int);
            SqlParameter idPok = RequestAddTypePokemon.Parameters.Add("@pok_id", SqlDbType.Int);
            SqlParameter emplac = RequestAddTypePokemon.Parameters.Add("@emplac", SqlDbType.TinyInt);
            idType.Value = IdType;
            idPok.Value = IdPokemon;
            emplac.Value = 3;

            int result = RequestAddTypePokemon.ExecuteNonQuery();
       

            // Fermeture de la connexion
            this.activeConnexion.Close();
        }
    }
}
