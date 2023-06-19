using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public interface ITalent
    {
        int IdTalent { get; }
        string NomTalent { get; set; }
        MVersion VersionTalent { get; set; }
        string DescTalent { get; set; }
    }
}
