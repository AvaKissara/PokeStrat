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
    public class RepStat
    {
        public BddTool bddTool;

        public RepStat()
        {
            bddTool = new BddTool();

            try
            {
                this.bddTool.DbConnexion();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la connexion à la base de données : " + ex.Message);
            }
        }

        public Dictionary<MStat, int> GetAll()
        {
            bddTool.CheckConnexion();

            Dictionary<MStat, int> allStats = new Dictionary<MStat, int>();
            SqlCommand RequestGetStats = bddTool.GetRequest();
            RequestGetStats.CommandText = "SELECT id_stat, nom_stat FROM Stats";

            using (SqlDataReader stats = RequestGetStats.ExecuteReader())
            {
                while (stats.Read())
                {
                    MStat stat = new MStat(
                        stats.GetInt32(0),
                        stats.GetString(1)
                     );
                
                   allStats.Add(stat, 0);
                }     
            }

            return allStats;
        }

        public (MStat statBonus, MStat statMalus) GetNatureStats(int idNature)
        {
            bddTool.CheckConnexion();
            SqlCommand RequestGetNatureStats = bddTool.GetRequest();
            RequestGetNatureStats.CommandText = "SELECT N.stat_bonus_id, S.abbr_stat, N.stat_malus_id, S2.abbr_stat FROM Natures AS N LEFT JOIN Stats AS S ON stat_bonus_id = id_stat LEFT JOIN Stats AS S2 ON stat_malus_id = S2.id_stat WHERE id_nature = @id_nature";
            RequestGetNatureStats.Parameters.Add("@id_nature", SqlDbType.Int).Value = idNature;

            using (SqlDataReader natureStats = RequestGetNatureStats.ExecuteReader())
            {
                if (natureStats.Read())
                {
                    int statBonusId = natureStats.GetInt32(0);
                    string statBonusNom = natureStats.GetString(1);
                    int statMalusId = natureStats.GetInt32(2);
                    string statMalusNom = natureStats.GetString(3);

                    MStat statBonus = new MStat(statBonusId, statBonusNom);
                    MStat statMalus = new MStat(statMalusId, statMalusNom);

                    return (statBonus, statMalus);
                }
                else
                {
                    return (null, null);
                }
            }
        }
    }

  

}
