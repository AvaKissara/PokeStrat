using PokeStrat;
using PokeStrat.Modeles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Repositories
{
    public class RepPokemon
    {
        private SqlConnection activeConnexion;
        public RepPokemon()
        {
            this.dbConnecter();
        }

        private void dbConnecter()
        {
            Connexion con = new Connexion();
            this.activeConnexion = con.GetConnexion();
        }

        public List<MPokemon> GetPokemons()
        {
            List<MPokemon> ListMPokemons = new List<MPokemon>();

            SqlCommand RequestGetPokemons = activeConnexion.CreateCommand();
            RequestGetPokemons.CommandText = "SELECT P.nom_eng_pok, P.nom_fra_pok, P.pok_img, P.num_pok, P.taille_pok, P.poids_pok, P.base_experience, P.base_hp, P.base_att, P.base_def, P.base_sp_att, P.base_sp_def, P.base_vit, P.legendaire, P.shiny, P2.id_pok as id_evo, P2.pok_img as img_evo, P2.nom_fra_pok as nom_evo, P.niv_evo, V.nom_version, V.num_version\r\nFROM Pokemon AS P\r\nLEFT JOIN Version as V ON P.id_version = V.id_version\r\nLEFT JOIN Pokemon P2 ON P.id_evo = P2.id_pok";

            SqlDataReader pokemons = RequestGetPokemons.ExecuteReader();

            while (pokemons.Read())
            {
                 MPokemon unPokemon = new MPokemon(
                 pokemons.GetInt32(0),
                 $"{pokemons[1]}",
                 $"{pokemons[2]}",
                 $"{pokemons[3]}",
                 $"{pokemons[4]}",
                 pokemons.GetDouble(5),
                 pokemons.GetInt32(6),
                 pokemons.GetInt32(7),
                 pokemons.GetInt32(8),
                 pokemons.GetInt32(9),
                 pokemons.GetInt32(10),
                 pokemons.GetInt32(11),
                 pokemons.GetInt32(12),
                 pokemons.GetInt32(13),
                 Convert.ToBoolean(pokemons.GetInt32(14)),
                 Convert.ToBoolean(pokemons.GetInt32(15)),
                 new MPokemon(pokemons.GetInt32(17), $"{pokemons[18]}", $"{pokemons[19]}"),
                 pokemons.GetInt32(16),
                 new MVersion(pokemons.GetInt32(20), $"{pokemons[21]}", pokemons.GetInt32(22))
             );

                ListMPokemons.Add(unPokemon);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMPokemons;
        }

    }
}
