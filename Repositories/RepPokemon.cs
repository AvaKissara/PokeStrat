using PokeStat;
using PokeStat.Modeles;
using System;
using System.Collections.Generic;
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
            RequestGetPokemons.CommandText = "SELECT p.id_pok, P.pok_img, P.nom_eng_pok, P.nom_fra_pok,  P.num_pok, P.taille_pok, P.poids_pok, P.base_experience, P.base_hp, P.base_att, P.base_def, P.base_sp_att, P.base_sp_def, P.base_vit, P.legendaire, P.shiny, P2.id_pok as id_evo, P2.pok_img as img_evo, P2.nom_fra_pok as nom_evo, P.niv_evo, V.id_version, V.nom_version, V.num_gen FROM Pokemon AS P LEFT JOIN Version as V ON P.id_version = V.id_version LEFT JOIN Pokemon P2 ON P.id_evo = P2.id_pok";

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

                    MVersion version = null;
                    if (!pokemons.IsDBNull(20)) // Vérifier si la colonne id_version peut être nulle
                    {
                        int idVersion = pokemons.IsDBNull(20) ? 0 : pokemons.GetInt32(20);
                        string nomVersion = pokemons.IsDBNull(21) ? "" : $"{pokemons[21]}";
                        int numVersion = pokemons.IsDBNull(22) ? 0 : pokemons.GetInt32(22);
                        version = new MVersion(idVersion, nomVersion, numVersion);
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
                        version
                    );

                    ListMPokemons.Add(unPokemon);
                }
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMPokemons;
        }

    }
}
