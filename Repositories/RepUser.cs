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
    public class RepUser : IRepository<MUser>
    {
        private SqlConnection activeConnexion;

        public RepUser()
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

        public List<MUser> GetAll()
        {
            CheckConnexion();

            List<MUser> ListMUsers = new List<MUser>();

            SqlCommand RequestGetUsers = activeConnexion.CreateCommand();
            using (SqlDataReader pokemons = RequestGetUsers.ExecuteReader())
            {
                try
                {
    
                    RequestGetUsers.CommandText = "SELECT * FROM Users";

                    using (SqlDataReader users = RequestGetUsers.ExecuteReader())
                    {
                        while (users.Read())
                        {
                            MUser unUser = new MUser(
                                users.GetInt32(0),
                                $"{users[1]}",
                                $"{users[2]}",
                                $"{users[3]}",
                                $"{users[4]}",
                                $"{users[5]}",
                                DateTime.Parse($"{users[6]}"),
                                DateTime.Parse($"{users[7]}"));

                            ListMUsers.Add(unUser);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gestion de l'exception
                    Console.WriteLine("Erreur lors de la récupération des generations : " + ex.Message);
                }

            }
            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMUsers;
        }
        public void Add(MUser MModele)
        {
            //INSERT INTO Users(nom_user, prenom_user, pseudo, mail_user, mdp_user, actualise_le, date_id)
            //VALUES('Badi', 'Lila', 'Kissara', 'avakissara@gmail.com', 'test', GETDATE(), CONVERT(DATETIME, '2023-07-24 00:04:32.733', 121));
        }

        public void Delete(int idSuppr)
        {

        }    
        public void Update(MUser MModele)
        {

        }
    }
}
