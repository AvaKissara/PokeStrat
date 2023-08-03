using PokeStat.Modeles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
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

        public void DbConnecter()
        {
            Connexion con = new Connexion();
            this.activeConnexion = con.GetConnexion();
        }

        public void CheckConnexion()
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
       
            try
            {

                SqlCommand RequestGetUsers = activeConnexion.CreateCommand();
                RequestGetUsers.CommandText = "SELECT * FROM Users";
               
                using (SqlDataReader users = RequestGetUsers.ExecuteReader())
                {
                    while (users.Read())
                    {
                        SecureString mdp = new NetworkCredential("", $"{users[5]}").SecurePassword;
                        MUser unUser = new MUser(
                            users.GetInt32(0),
                            $"{users[1]}",
                            $"{users[2]}",
                            $"{users[3]}",
                            $"{users[4]}",
                            mdp,
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

            
            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMUsers;
        }

        public void Add(MUser nouvelUser)
        {
            CheckConnexion();

            //try
            //{
                SqlCommand RequestAddUsers = activeConnexion.CreateCommand();              
                
                RequestAddUsers.CommandText = "INSERT INTO Users(nom_user, prenom_user, pseudo, mail_user, mdp_user, actualise_le, date_id) VALUES(@nom_user, @prenom_user, @pseudo, @mail_user, @mdp_user, @actualise_le, @date_id)";

                SqlParameter nom = RequestAddUsers.Parameters.Add("@nom_user", SqlDbType.VarChar);             
                SqlParameter prenom = RequestAddUsers.Parameters.Add("@prenom_user", SqlDbType.VarChar);           
                SqlParameter pseuso = RequestAddUsers.Parameters.Add("@pseudo", SqlDbType.VarChar);              
                SqlParameter mail = RequestAddUsers.Parameters.Add("@mail_user", SqlDbType.VarChar);               
                SqlParameter mdp = RequestAddUsers.Parameters.Add("@mdp_user", SqlDbType.VarChar);               
                SqlParameter actualise = RequestAddUsers.Parameters.Add("@actualise_le", SqlDbType.DateTime);              
                SqlParameter cree = RequestAddUsers.Parameters.Add("@date_id", SqlDbType.DateTime);

                nom.Value = nouvelUser.nomUser;
                prenom.Value = nouvelUser.prenomUser;
                pseuso.Value = nouvelUser.pseudoUser;
                mail.Value = nouvelUser.mailUser;
                mdp.Value = nouvelUser.mdpUser;
                actualise.Value = DateTime.Now;
                cree.Value = nouvelUser.cree;
                /* Parse the date string and convert it to DateTime for the "date_id" parameter
                string dateString = "2023-07-24 00:04:32.733";
                DateTime dateId = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                RequestAddUsers.Parameters.AddWithValue("@date_id", dateId);*/

                int result = RequestAddUsers.ExecuteNonQuery();          
            /*}
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error while adding User: " + ex.Message);
            }*/
        }

        public void Delete(int idSuppr)
        {

        }   
        
        public void Update(MUser MModele)
        {

        }
    }
}
