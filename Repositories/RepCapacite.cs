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
    public class RepCapacite
    {
        public BddTool bddTool;
        public RepCapacite() 
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

        public List<MCapacite> GetCapacite(int idEquipier)
        {
            bddTool.CheckConnexion();

            List<MCapacite> ListMCapacites = new List<MCapacite>();

            SqlCommand RequestGetCaps = bddTool.GetRequest();
            RequestGetCaps.CommandText = "SELECT P.id_pok, PC.cap_id, C.nom_cap FROM pokemon_capacite AS PC LEFT JOIN Pokemons AS P ON PC.pok_id = P.id_pok LEFT JOIN Capacites AS C ON PC.cap_id = cap_id WHERE P.id_pok = @id_pok;";
            SqlParameter idEq = RequestGetCaps.Parameters.Add("@id_pok", SqlDbType.Int);

            idEq.Value = idEquipier;

            using (SqlDataReader caps = RequestGetCaps.ExecuteReader())
            {
                while (caps.Read())
                {
                    MCapacite uneCap = new MCapacite(
                      caps.GetInt32(1),
                      $"{caps[2]}"
                     );
                    ListMCapacites.Add(uneCap);
                }
            }

            bddTool.CloseConnexion();
            return ListMCapacites;
        }
    }
}
