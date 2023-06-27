﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeStat.Modeles;

namespace PokeStat.Repositories
{
    public class RepGeneration
    {
        private SqlConnection activeConnexion;

        public RepGeneration()
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

        public List<MGeneration> GetGenerations()
        {
            CheckConnexion();

            List<MGeneration> ListMGenerations = new List<MGeneration>();

            try
            {
                SqlCommand RequestGetGenerations = activeConnexion.CreateCommand();
                RequestGetGenerations.CommandText = "SELECT * FROM Generations";

                using (SqlDataReader generations = RequestGetGenerations.ExecuteReader())
                {
                    while (generations.Read())
                    {
                        MGeneration uneGeneration = new MGeneration(
                            generations.GetInt32(0),
                            $"{generations[1]}"
                        );

                        ListMGenerations.Add(uneGeneration);
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

            return ListMGenerations;
        }

        public void AddGeneration(MGeneration nouvelleGeneration)
        {
            CheckConnexion();

            try
            {
                SqlCommand RequestAddGeneration = activeConnexion.CreateCommand();
                RequestAddGeneration.CommandText = "INSERT INTO Generations (nom_generation) VALUES (@nom_generation)";

                SqlParameter nom = RequestAddGeneration.Parameters.Add("@nom_generation", SqlDbType.VarChar);
                nom.Value = nouvelleGeneration.nomGen;


                int result = RequestAddGeneration.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de l'ajout du type : " + ex.Message);
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();
        }

    }
}
