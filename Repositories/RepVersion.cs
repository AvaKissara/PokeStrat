﻿using PokeStat.Modeles;
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
    public class RepVersion : IRepository<MVersion>
    {
        public BddTool bddTool;

        public RepVersion()
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

        public List<MVersion> GetAll()
        {
            bddTool.CheckConnexion();

            List<MVersion> ListMVersions = new List<MVersion>();

            try
            {
                SqlCommand RequestGetVersions = bddTool.GetRequest();
                RequestGetVersions.CommandText = "SELECT id_version, nom_version, gen_id, nom_gen FROM Versions as V LEFT JOIN Generations as G ON V.gen_id = G.id_gen";

                using (SqlDataReader versions = RequestGetVersions.ExecuteReader())
                {
                    while (versions.Read())
                    {
                        MGeneration gen = null;
                        if (!versions.IsDBNull(2))
                        {
                            int idGen = versions.IsDBNull(2) ? 0 : versions.GetInt32(2);
                            string nomGen = nomGen = versions.IsDBNull(3) ? "" : $"{versions[3]}";
                            gen = new MGeneration(idGen, nomGen);
                        }
                        MVersion uneVersion = new MVersion(
                            versions.GetInt32(0),
                            $"{versions[1]}",
                            gen
                        );

                        ListMVersions.Add(uneVersion);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la récupération des versions : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();

            return ListMVersions;
        }
        public void Add(MVersion entreeVersion)
        {
            if (entreeVersion is MVersion nouvelleVersion)
            {
                int idGen = nouvelleVersion.Gen.IdGen;

                Add(nouvelleVersion, idGen);
            }
            else
            {
                throw new ArgumentException("L'entrée n'est pas de type MVersion");
            }
        }

        public void Add(MVersion nouvelleVersion, int idGen)
        {
            bddTool.CheckConnexion();

            try
            {
                SqlCommand RequestAddVersion = bddTool.GetRequest();
                RequestAddVersion.CommandText = "INSERT INTO Versions (nom_version, gen_id) VALUES (@nom_version, @gen_id)";

                SqlParameter nom = RequestAddVersion.Parameters.Add("@nom_version", SqlDbType.VarChar);
                SqlParameter gen = RequestAddVersion.Parameters.Add("@gen_id", SqlDbType.Int);
                nom.Value = nouvelleVersion.NomVersion;
                gen.Value = idGen;

                int result = RequestAddVersion.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de l'ajout de la version : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }

        public void Update(MVersion MModele)
        {

        }

        public void Delete(int idSuppr)
        {
            bddTool.CheckConnexion();

            try
            {
                SqlCommand RequestDeleteVersion = bddTool.GetRequest();
                RequestDeleteVersion.CommandText = "DELETE FROM Versions WHERE id_version = @id_version";

                SqlParameter id = RequestDeleteVersion.Parameters.Add("@id_version", SqlDbType.Int);
                id.Value = idSuppr;

                int result = RequestDeleteVersion.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la suppression du type : " + ex.Message);
            }

            // Fermeture de la connexion
            bddTool.CloseConnexion();
        }
    }
}
