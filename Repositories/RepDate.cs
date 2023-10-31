using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    /// <summary>
    /// Excessif mais peut devenir intéressant selon l'évolution du projet
    /// </summary>
    public class RepDate
    {
        public BddTool bddTool;
        public RepDate()
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

        public void Add(MDate nouvelleDate) 
        {
            bddTool.CheckConnexion();
            //try
            //{
                SqlCommand RequestAddDate = bddTool.GetRequest();
                RequestAddDate.CommandText = "INSERT INTO Dates VALUES (@jjmmaaaa)";

                SqlParameter idDate = RequestAddDate.Parameters.Add("@jjmmaaaa", SqlDbType.DateTime);
                idDate.Value = nouvelleDate.IdDate;

                int result = RequestAddDate.ExecuteNonQuery();
            //}
            //catch (Exception ex) 
            //{
            //    Console.WriteLine("Erreur lors de l'ajout de la date : " + ex.Message);
            //}

            bddTool.CloseConnexion();
        }
    }
}
