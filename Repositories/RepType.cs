﻿using PokeStat.Modeles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PokeStat.Repositories
{
    public class RepType
    {
        private SqlConnection activeConnexion;

        public RepType()
        {
            try
            {
                this.DbConnecter();
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple, afficher un message d'erreur ou enregistrer l'erreur dans un journal.
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

        public List<MType> GetTypes()
        {
            CheckConnexion();

            List<MType> ListMTypes = new List<MType>();

            try
            {
                SqlCommand RequestGetTypes = activeConnexion.CreateCommand();
                RequestGetTypes.CommandText = "SELECT * FROM Types";

                using (SqlDataReader types = RequestGetTypes.ExecuteReader())
                {
                    while (types.Read())
                    {
                        MType unType = new MType(
                            types.GetInt32(0),
                            $"{types[1]}"
                        );

                        ListMTypes.Add(unType);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple, afficher un message d'erreur ou enregistrer l'erreur dans un journal.
                Console.WriteLine("Erreur lors de la récupération des types : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMTypes;
        }

        public void AddType(MType nouveauType)
        {
            CheckConnexion();

            try
            {
                SqlCommand RequestAddType = activeConnexion.CreateCommand();
                RequestAddType.CommandText = "INSERT INTO Types (nom_type) VALUES (@nom_type)";

                SqlParameter nom = RequestAddType.Parameters.Add("@nom_type", SqlDbType.VarChar);
                nom.Value = nouveauType.nomType;

                int result = RequestAddType.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple, afficher un message d'erreur ou enregistrer l'erreur dans un journal.
                Console.WriteLine("Erreur lors de l'ajout du type : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();
        }

        public void DeleteType(int idSuppr)
        {
            CheckConnexion();

            try
            {
                SqlCommand RequestDeleteType = activeConnexion.CreateCommand();
                RequestDeleteType.CommandText = "DELETE FROM Types WHERE id_type = @id_type";

                SqlParameter id = RequestDeleteType.Parameters.Add("@id_type", SqlDbType.Int);
                id.Value = idSuppr;

                int result = RequestDeleteType.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple, afficher un message d'erreur ou enregistrer l'erreur dans un journal.
                Console.WriteLine("Erreur lors de la suppression du type : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();
        }

        public void UpdateType(MType modifType)
        {
            CheckConnexion();

            try
            {
                SqlCommand RequestUpdateType = activeConnexion.CreateCommand();
                RequestUpdateType.CommandText = "UPDATE Types SET nom_type= @nomType WHERE id_type = @idType";

                SqlParameter id = RequestUpdateType.Parameters.Add("@idType", SqlDbType.VarChar);
                SqlParameter nom = RequestUpdateType.Parameters.Add("@nomType", SqlDbType.VarChar);

                id.Value = modifType.idType;
                nom.Value = modifType.nomType;

                int result = RequestUpdateType.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple, afficher un message d'erreur ou enregistrer l'erreur dans un journal.
                Console.WriteLine("Erreur lors de la mise à jour du type : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();
        }
    }
}
