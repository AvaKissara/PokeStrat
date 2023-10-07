using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepTalent
    {
        public BddTool bddTool;

        public RepTalent()
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
        public List<MTalent> GetAll()
        {
            bddTool.CheckConnexion();

            List<MTalent> ListMTalents = new List<MTalent>();

            SqlCommand RequestGetTalents = bddTool.GetRequest();
            RequestGetTalents.CommandText = "SELECT P.id_pok, PT.talent_id, T.nom_talent FROM pokemon_talent AS PT LEFT JOIN Pokemons AS P ON PT.pok_id = P.id_pok LEFT JOIN Talents AS T ON PT.talent_id = T.id_talent;";

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
    }
}
