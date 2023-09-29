using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MType
    {
        public int IdType { get; set; }
        public string NomType { get; set; }
        public int Emplacement { get; set; }

        private const int MaxTypeId = 18; // À rendre variable

        private readonly double[,] grilleDommage = new double[MaxTypeId, MaxTypeId];

        public MType(string NomType)
        {
            this.NomType = NomType;
        }

        public MType(int IdType, string NomType)
        {
            this.IdType = IdType;
            this.NomType = NomType;
        }

        public MType()
        {

        }

        public void PeuplerGrilleDommage()
        {
            // Initialisation de la grille avec des coefficients de dommage par défaut
            // Coefficient de dommage de 1.0 pour les combinaisons attaquant/cible identiques
            for (int i = 0; i < MaxTypeId; i++)
            {
                for (int j = 0; j < MaxTypeId; j++)
                {
                    if (i == j)
                    {
                        grilleDommage[i, j] = 1.0;
                    }
                    else
                    {
                        grilleDommage[i, j] = 0.0;
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MType other = (MType)obj;
            return IdType == other.IdType && NomType == other.NomType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + IdType.GetHashCode();
                hash = hash * 23 + (NomType != null ? NomType.GetHashCode() : 0);
                return hash;
            }
        }
    }

}
