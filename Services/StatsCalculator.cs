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

        //public double CalculPourcentage(int valeurDeBase)
        //{
        //    double maxPV = valeurDeBase + 252;
        //    return (valeurDeBase / maxPV) * 100.0;
        //}

        //public int PourcentageVersEntier(double pourcentage, int maxPV)
        //{
        //    double valeurTotale = (pourcentage / 100.0) * maxPV;
        //    return (int)valeurTotale;
        //}

    }
}
