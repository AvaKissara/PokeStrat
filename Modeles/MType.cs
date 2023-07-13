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
        public int idType { get; set; }

        public string nomType { get; set; }

        public int emplacement { get; set; }

        const int MaxTypeId = 18; //à rendre variable

        double[,] grilleDommage = new double[MaxTypeId, MaxTypeId];

        public MType(string NomType)
        {
            this.nomType = NomType;
        }
        public MType(int IdType, string NomType)
        {
            this.idType = IdType;
            this.nomType = NomType;
        }

        public MType()
        {
         
        }

        public void PeuplerDicoType()
        {
            // Initialisation de la grille avec des coefficients de dommage par défaut
            // coefficient de dommage de 1.0 pour les combinaisons attaquant/cible identiques
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
            return idType == other.idType && nomType == other.nomType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + idType.GetHashCode();
                hash = hash * 23 + (nomType != null ? nomType.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
