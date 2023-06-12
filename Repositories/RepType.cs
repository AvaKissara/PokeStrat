﻿using PokeStat.Modeles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepType
    {
        private SqlConnection activeConnexion;

        public RepType()
        {
            this.dbConnecter();
        }

        private void dbConnecter()
        {
            Connexion con = new Connexion();
            this.activeConnexion = con.GetConnexion();
        }
        public List<MType> GetTypes()
        {
            List<MType> ListMTypes = new List<MType>();

            SqlCommand RequestGetTypes = activeConnexion.CreateCommand();
            RequestGetTypes.CommandText = "SELECT * FROM Types";

            using (SqlDataReader types = RequestGetTypes.ExecuteReader())
            {
                while (types.Read())
                {
                    //var emplacement = types.IsDBNull(2) ? 0 : types.GetByte(2);

                    MType unType = new MType(
                        types.GetInt32(0),
                        $"{types[1]}"
                    );

                    ListMTypes.Add(unType);
                }
            }

            // Fermeture de la connexion
            this.activeConnexion.Close();

            return ListMTypes;
        }
        public void AddType(MType nouveauType)
        {
            SqlCommand RequestAddType = activeConnexion.CreateCommand();
            RequestAddType.CommandText = "INSERT INTO Types (nom_type) VALUES (@nom_type)";

            SqlParameter nom = RequestAddType.Parameters.Add("@nom_type", SqlDbType.VarChar);

            nom.Value = nouveauType.nomType;

            int result = RequestAddType.ExecuteNonQuery();

        }

        //public void deleteType(int idSuppr)
        //{
        //    SqlCommand RequestDeleteType = activeConnexion.CreateCommand();
        //    RequestDeleteType.CommandText = "DELETE FROM personne WHERE idType = @idType";

        //    SqlParameter id = RequestDeleteType.Parameters.Add("@idType", SqlDbType.VarChar);

        //    id.Value = idSuppr;

        //    int result = RequestDeleteType.ExecuteNonQuery();
        //}

        //public void updateType(MType modifType)
        //{
        //    SqlCommand RequestUpdateType = activeConnexion.CreateCommand();
        //    RequestUpdateType.CommandText = "UPDATE personne SET nomType= @nomType, prenomType= @prenomType, mdp = @mdp WHERE idType = @idType";


        //    SqlParameter id = RequestUpdateType.Parameters.Add("@idType", SqlDbType.VarChar);
        //    SqlParameter nom = RequestUpdateType.Parameters.Add("@nomType", SqlDbType.VarChar);
        //    SqlParameter pnom = RequestUpdateType.Parameters.Add("@prenomType", SqlDbType.VarChar);
        //    SqlParameter mdp = RequestUpdateType.Parameters.Add("@mdp", SqlDbType.VarBinary);



        //    id.Value = modifType.idType;
        //    nom.Value = modifType.nomType;
        //    pnom.Value = modifType.prenomType;
        //    mdp.Value = modifType.mdp;

        //    int result = RequestUpdateType.ExecuteNonQuery();
        //}
    }
}
