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
    public class RepAdmin : IRepository<MAdmin>
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
                            DateTime.Parse($"{users[6]}"),
                            creationDate,
                            mdpFort);

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
             public void Add(MAdmin MModele)
        {
            throw new NotImplementedException();
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
