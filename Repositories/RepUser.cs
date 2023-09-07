using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepUser : IRepository<MUser>
    {
        public BddTool bddTool;

        public RepUser()
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

        public List<MUser> GetAll()
        {
            bddTool.CheckConnexion();

            List<MUser> ListMUsers = new List<MUser>();
       
            try
            {
                SqlCommand RequestGetUsers = bddTool.GetRequest();
                RequestGetUsers.CommandText = "SELECT * FROM Users";
               
                using (SqlDataReader users = RequestGetUsers.ExecuteReader())
                {
                    while (users.Read())
                    {
                        SecureString mdp = new NetworkCredential("", $"{users[5]}").SecurePassword;
                        MDate creationDate = null;
                        if(!users.IsDBNull(7))
                        {
                            DateTime idDate = DateTime.Parse($"{users[7]}");
                            creationDate = new MDate(idDate);
                        }

                        MUser unUser = new MUser(
                            users.GetInt32(0),
                            $"{users[1]}",
                            $"{users[2]}",
                            $"{users[3]}",
                            $"{users[4]}",
                            mdp,
                            DateTime.Parse($"{users[6]}"),
                            creationDate);

                        ListMUsers.Add(unUser);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération des utilisateurs: " + ex.Message);
            }

            
            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return ListMUsers;
        }

        public SecureString GetSalt(int idUserLogin)
        {
            bddTool.CheckConnexion();

            SecureString salt = new SecureString();

            SqlCommand RequestGetSaltUser = bddTool.GetRequest();
            RequestGetSaltUser.CommandText = "SELECT sel_user FROM Users WHERE id_user = @id_user";

            SqlParameter id = RequestGetSaltUser.Parameters.Add("@id_user", SqlDbType.Int);

            id.Value = idUserLogin;

            SqlDataReader reader = RequestGetSaltUser.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Récupére la valeur du sel depuis la base de données
                    string saltValue = reader.GetString(0);

                    // Ajoute chaque caractère du sel à la SecureString
                    foreach (char c in saltValue)
                    {
                        salt.AppendChar(c);
                    }
                }
            }
            reader.Close();

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return salt;
        }

        public void Add(MUser nouvelUser)
        {
            bddTool.CheckConnexion();
            // Convertir le SecureString en string
            string mdpString = PasswordManager.ToInsecureString(nouvelUser.mdpPersonne);

            // Utiliser PasswordManager pour hacher le mot de passe
            (string hash, string salt) = PasswordManager.HashPassword(mdpString);

            try
            {
                SqlCommand RequestAddUser = bddTool.GetRequest();              
                
                RequestAddUser.CommandText = "INSERT INTO Users(nom_user, prenom_user, pseudo, mail_user, mdp_user, actualise_le, date_id, sel_user) VALUES(@nom_user, @prenom_user, @pseudo, @mail_user, @mdp_user, @actualise_le, @date_id, @sel_user)";

                SqlParameter nom = RequestAddUser.Parameters.Add("@nom_user", SqlDbType.VarChar);             
                SqlParameter prenom = RequestAddUser.Parameters.Add("@prenom_user", SqlDbType.VarChar);           
                SqlParameter pseuso = RequestAddUser.Parameters.Add("@pseudo", SqlDbType.VarChar);              
                SqlParameter mail = RequestAddUser.Parameters.Add("@mail_user", SqlDbType.VarChar);               
                SqlParameter mdp = RequestAddUser.Parameters.Add("@mdp_user", SqlDbType.VarChar);               
                SqlParameter actualise = RequestAddUser.Parameters.Add("@actualise_le", SqlDbType.DateTime);              
                SqlParameter cree = RequestAddUser.Parameters.Add("@date_id", SqlDbType.DateTime);
                SqlParameter sel = RequestAddUser.Parameters.Add("@sel_user", SqlDbType.VarChar);

                nom.Value = nouvelUser.nomPersonne;
                prenom.Value = nouvelUser.prenomPersonne;
                pseuso.Value = nouvelUser.pseudoPersonne;
                mail.Value = nouvelUser.mailPersonne;
                mdp.Value = hash;
                actualise.Value = DateTime.Now;
                cree.Value = nouvelUser.cree.idDate;
                sel.Value = salt;

                int result = RequestAddUser.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout de l'utilisateur: " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }

        public void Delete(int idSuppr)
        {
            bddTool.CheckConnexion();

            try
            {
                SqlCommand RequestDeleteUser = bddTool.GetRequest();

                RequestDeleteUser.CommandText = "DELETE FROM Users WHERE id_user = @id_user";

                SqlParameter id = RequestDeleteUser.Parameters.Add("@id_user", SqlDbType.Int);

                id.Value = idSuppr;

                int result = RequestDeleteUser.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la suppression de l'utilisateur: " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }   
        
        public void Update(MUser modifUser)
        {
            bddTool.CheckConnexion();

            // Convertir le SecureString en string
            string mdpString = modifUser.ToInsecureString(modifUser.mdpPersonne);
           
              
            // Utiliser PasswordManager pour hacher le mot de passe
            (string hash, string salt) = PasswordManager.HashPassword(mdpString);
         

            SqlCommand RequestUpdateUser = bddTool.GetRequest();

             if (mdpString == null) 
             {
                RequestUpdateUser.CommandText = "UPDATE Users SET nom_user = @nom_user, prenom_user = @prenom_user, pseudo = @pseudo, mail_user = @mail_user, actualise_le = @actualise_le WHERE id_user = @id_user";
             }
             else
             {
                RequestUpdateUser.CommandText = "UPDATE Users SET nom_user = @nom_user, prenom_user = @prenom_user, pseudo = @pseudo, mail_user = @mail_user, mdp_user = @mdp_user, actualise_le = @actualise_le, sel_user = @sel_user WHERE id_user = @id_user";
             }

            SqlParameter id = RequestUpdateUser.Parameters.Add("@id_user", SqlDbType.Int);
            SqlParameter nom = RequestUpdateUser.Parameters.Add("@nom_user", SqlDbType.VarChar);
            SqlParameter prenom = RequestUpdateUser.Parameters.Add("@prenom_user", SqlDbType.VarChar);
            SqlParameter pseuso = RequestUpdateUser.Parameters.Add("@pseudo", SqlDbType.VarChar);
            SqlParameter mail = RequestUpdateUser.Parameters.Add("@mail_user", SqlDbType.VarChar);
            SqlParameter mdp = RequestUpdateUser.Parameters.Add("@mdp_user", SqlDbType.VarChar);
            SqlParameter actualise = RequestUpdateUser.Parameters.Add("@actualise_le", SqlDbType.DateTime);
            SqlParameter sel = RequestUpdateUser.Parameters.Add("@sel_user", SqlDbType.VarChar);

            id.Value = modifUser.idPersonne;
            nom.Value = modifUser.nomPersonne;
            prenom.Value = modifUser.prenomPersonne;
            pseuso.Value = modifUser.pseudoPersonne;
            mail.Value = modifUser.mailPersonne;
            mdp.Value = hash;
            actualise.Value = DateTime.Now;
            sel.Value = salt;

            int result = RequestUpdateUser.ExecuteNonQuery();

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }
    }
}
