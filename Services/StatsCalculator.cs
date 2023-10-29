using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeStat.Modeles;
using PokeStat.Repositories;

namespace PokeStat.Services
{
    public class StatsCalculator
    {
        private readonly RepStat repStat;

        public StatsCalculator()
        {
            repStat = new RepStat();
        }
        public int CalculPV(int basePV, int iv, int ev, int niveau)
        {
            return ((iv + 2 * basePV + (ev / 4)) * niveau / 100) + niveau + 10;
        }

        public int CalculStat(int baseStat, int iv, int ev, int niveau, double natureMultiplier)
        {
            return (int)(((iv + 2 * baseStat + (ev / 4)) * niveau / 100 + 5) * natureMultiplier);
        }

        public int CalculEVPourPV(int pv, int niveau, int baseStat, int iv)
        {
            return (((pv - 10) * 100) / (niveau - 2 * baseStat - 100)) * 4;
        }

        public MEquipier GetNatureStats(MEquipier equipier)
        {
            MEquipier equipierStatNature = equipier;
            int natureId = equipier.Nature.IdNature;
            (MStat statBonus, MStat statMalus) = repStat.GetNatureStats(natureId);
            switch (statBonus.IdStat)
            {
                case 1:
                    double pv = equipier.BasePV * 1.10;
                    equipierStatNature.BasePV = (int)(pv);
                    break;
                case 2:
                    double att = equipier.BaseAttaque * 1.10;
                    equipierStatNature.BaseAttaque = (int)(att);
                    break;
                case 3:
                    double def = equipier.BaseDefense * 1.10;
                    equipierStatNature.BaseDefense = (int)(def);
                    break;
                case 4:
                    double attSpe = equipier.BaseAttSpe * 1.10;
                    equipierStatNature.BaseAttSpe = (int)(attSpe);
                    break;
                case 5:
                    double defSpe = equipier.BaseDefSpe * 1.10;
                    equipierStatNature.BaseDefSpe = (int)(defSpe);
                    break;
                case 6:
                    double vit = equipier.BaseVit * 1.10;
                    equipierStatNature.BaseVit = (int)(vit);
                    break;
                default:
                   break;

            }

            switch (statMalus.IdStat)
            {
                case 1:
                    double pv = equipier.BasePV * 0.90;
                    equipierStatNature.BasePV = (int)(pv);
                    break;
                case 2:
                    double att = equipier.BaseAttaque * 0.90;
                    equipierStatNature.BaseAttaque = (int)(att);
                    break;
                case 3:
                    double def = equipier.BaseDefense * 0.90;
                    equipierStatNature.BaseDefense = (int)(def);
                    break;
                case 4:
                    double attSpe = equipier.BaseAttSpe * 0.90;
                    equipierStatNature.BaseAttSpe = (int)(attSpe);
                    break;
                case 5:
                    double defSpe = equipier.BaseDefSpe * 0.90;
                    equipierStatNature.BaseDefSpe = (int)(defSpe);
                    break;
                case 6:
                    double vit = equipier.BaseVit * 0.90;
                    equipierStatNature.BaseVit = (int)(vit);
                    break;
                default:

                    break;
            }
            return equipierStatNature;
        }

        public int CalculEVPourStat(int stat, double modificateurNature, int niveau, int baseStat, int iv, int evTotal)
        {
            return (int)((((stat / modificateurNature) - 5) * 100) / (niveau - (2 * baseStat) - ((iv / 4) * 4)));
        }
        public bool AjouterEVAStat(MEquipier equipier, MStat stat, int evsAAjouter)
        {
            int MaxTotalEVs = 510;
            int MaxEVsParStat = 252;



            if (evsAAjouter <= 0)
            {
                return false;
            }

            int totalEVs = equipier.Ev;
            if (totalEVs + evsAAjouter > MaxTotalEVs)
            {
                return false;
            }

            int baseStat = 0; 

            // Utilisez les méthodes de calcul pour obtenir le nouveau total d'EVs dans la statistique.
            int nouveauTotalEVsStat = evsAAjouter;
            int niveau = equipier.NiveauEquipier; // Remplacez par la vraie valeur du niveau de l'équipier.
            double modificateurNature = 1.0; // Remplacez par le modificateur de nature réel.

            switch (stat.IdStat)
            {
                case 1:
                    baseStat = equipier.BasePV;
                    nouveauTotalEVsStat = CalculEVPourPV(baseStat, niveau, baseStat, nouveauTotalEVsStat);
                    break;
                case 2:
                    baseStat = equipier.BaseAttaque; // Exemple pour la statistique "Attaque".
                    nouveauTotalEVsStat = CalculEVPourStat(baseStat, modificateurNature, niveau, baseStat, 0, nouveauTotalEVsStat);
                    break;
                case 3:
                    baseStat = equipier.BaseDefense; // Exemple pour la statistique "Attaque".
                    nouveauTotalEVsStat = CalculEVPourStat(baseStat, modificateurNature, niveau, baseStat, 0, nouveauTotalEVsStat);
                    break;
                case 4:
                    baseStat = equipier.BaseAttSpe; // Exemple pour la statistique "Attaque".
                    nouveauTotalEVsStat = CalculEVPourStat(baseStat, modificateurNature, niveau, baseStat, 0, nouveauTotalEVsStat);
                    break;
                case 5:
                    baseStat = equipier.BaseDefSpe; // Exemple pour la statistique "Attaque".
                    nouveauTotalEVsStat = CalculEVPourStat(baseStat, modificateurNature, niveau, baseStat, 0, nouveauTotalEVsStat);
                    break;
                case 6:
                    baseStat = equipier.BaseVit; // Exemple pour la statistique "Attaque".
                    nouveauTotalEVsStat = CalculEVPourStat(baseStat, modificateurNature, niveau, baseStat, 0, nouveauTotalEVsStat);
                    break;
                default:

                    break;
            }
         

            //if (stat.AbbrStat == "PV")
            //{
            //    baseStat = equipier.BasePV;
            //    nouveauTotalEVsStat = CalculEVPourPV(baseStat, niveau, baseStat, nouveauTotalEVsStat);
            //}
            //else
            //{
            //    // Remplacez par la vraie valeur de base de la statistique spécifique.
            //    baseStat = equipier.BaseAttaque; // Exemple pour la statistique "Attaque".
            //    nouveauTotalEVsStat = CalculEVPourStat(baseStat, modificateurNature, niveau, baseStat, 0, nouveauTotalEVsStat);
            //}

            if (nouveauTotalEVsStat > MaxEVsParStat)
            {
                return false; // Dépassement du maximum d'EVs pour une statistique.
            }

            // Mettez à jour le total des EVs.
            equipier.Ev += evsAAjouter;

            // Mettez à jour les EVs pour la statistique spécifiée.
            // Stockez les EVs par statistique dans une structure de données (p. ex., un dictionnaire).
            equipier.EVParStat[stat] = nouveauTotalEVsStat;

            return true; // Les EVs ont été ajoutés avec succès.
        }
    }
}
