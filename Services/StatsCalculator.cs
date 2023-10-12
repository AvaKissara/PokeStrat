using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Services
{
    public class StatsCalculator
    {
        public int CalculPV(int basePV, int iv, int ev, int niveau)
        {
            return ((iv + 2 * basePV + (ev / 4)) * niveau / 100) + niveau + 10;
        }

        public int CalculStat(int baseStat, int iv, int ev, int niveau, double natureMultiplier)
        {
            return (int)(((iv + 2 * baseStat + (ev / 4)) * niveau / 100 + 5) * natureMultiplier);
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
