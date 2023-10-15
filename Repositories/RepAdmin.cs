using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepAdmin 
    {
        public BddTool bddTool;

        public RepAdmin()
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

        public List<MAdmin> GetAll()
        {
            bddTool.CheckConnexion();

            List<MAdmin> ListMAdmins = new List<MAdmin>();

            try
            {
                SqlCommand RequestGetAdmins = bddTool.GetRequest();
                RequestGetAdmins.CommandText = "SELECT * FROM Admins";

                using (SqlDataReader users = RequestGetAdmins.ExecuteReader())
                {
                    while (users.Read())
                    {
                        SecureString mdp = new NetworkCredential("", $"{users[5]}").SecurePassword;
                        SecureString mdpFort = new NetworkCredential("", $"{users[8]}").SecurePassword;
                        MDate creationDate = null;
                        if (!users.IsDBNull(7))
                        {
                            DateTime idDate = DateTime.Parse($"{users[7]}");
                            creationDate = new MDate(idDate);
                        }

                        MAdmin unAdmin = new MAdmin(
                            users.GetInt32(0),
                            $"{users[1]}",
                            $"{users[2]}",
                            $"{users[3]}",
                            $"{users[4]}",
                            mdp,
                            users.GetDateTime(users.GetOrdinal("actualise_le")),
                            creationDate);

                        ListMAdmins.Add(unAdmin);
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

            return ListMAdmins;
        }

        public SecureString GetSalt(int idUserLogin)
        {
            bddTool.CheckConnexion();

            SecureString salt = new SecureString();

            SqlCommand RequestGetSaltAdmin = bddTool.GetRequest();
            RequestGetSaltAdmin.CommandText = "SELECT sel_admin FROM Admins WHERE id_admin = @id_admin";

            SqlParameter id = RequestGetSaltAdmin.Parameters.Add("@id_admin", SqlDbType.Int);

            id.Value = idUserLogin;

            SqlDataReader reader = RequestGetSaltAdmin.ExecuteReader();

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
        public void Add(MAdmin nouvelAdmin, int adminId)
        {
            bddTool.CheckConnexion();
            // Convertir le SecureString en string
            string mdpString = PasswordManager.ToInsecureString(nouvelAdmin.MdpPersonne);

            // Utiliser PasswordManager pour hacher le mot de passe
            (string hash, string salt) = PasswordManager.HashPassword(mdpString);

            try
            {
                SqlCommand RequestAddAdmin = bddTool.GetRequest();

            RequestAddAdmin.CommandText = "INSERT INTO admins(nom_admin, prenom_admin, pseudo_admin, mail_admin, mdp_admin, actualise_le, date_id, sel_admin) VALUES(@nom_admin, @prenom_admin, @pseudo, @mail_admin, @mdp_admin, @actualise_le, @date_id, @sel_admin); SELECT id_admin From Admins WHERE id_admin= @adminId; EXEC SetAdminID  @adminId= @adminId;";

                SqlParameter nom = RequestAddAdmin.Parameters.Add("@nom_admin", SqlDbType.VarChar);
                SqlParameter prenom = RequestAddAdmin.Parameters.Add("@prenom_admin", SqlDbType.VarChar);
                SqlParameter pseuso = RequestAddAdmin.Parameters.Add("@pseudo", SqlDbType.VarChar);
                SqlParameter mail = RequestAddAdmin.Parameters.Add("@mail_admin", SqlDbType.VarChar);
                SqlParameter mdp = RequestAddAdmin.Parameters.Add("@mdp_admin", SqlDbType.VarChar);
                SqlParameter actualise = RequestAddAdmin.Parameters.Add("@actualise_le", SqlDbType.DateTime);
                SqlParameter cree = RequestAddAdmin.Parameters.Add("@date_id", SqlDbType.DateTime);
                SqlParameter sel = RequestAddAdmin.Parameters.Add("@sel_admin", SqlDbType.VarChar);
                SqlParameter adminIdParam = RequestAddAdmin.Parameters.Add("@adminId", SqlDbType.Int);


                nom.Value = nouvelAdmin.NomPersonne;
                prenom.Value = nouvelAdmin.PrenomPersonne;
                pseuso.Value = nouvelAdmin.PseudoPersonne;
                mail.Value = nouvelAdmin.MailPersonne;
                mdp.Value = hash;
                actualise.Value = DateTime.Now;
                cree.Value = nouvelAdmin.Cree.IdDate;
                sel.Value = salt;
                adminIdParam.Value = adminId;

            int result = RequestAddAdmin.ExecuteNonQuery();
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
            throw new NotImplementedException();
        }
        public void Update(MAdmin MModele)
        {
            throw new NotImplementedException();
        }
    }
}
