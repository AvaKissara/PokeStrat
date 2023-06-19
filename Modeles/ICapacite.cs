using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public interface ICapacite
    {
        int IdCapacite { get; }
        string NomCapacite { get; set; }
        MType TypeCapacite { get; set; }
        List<MCategorie> CatCapacite { get; set; }
    }
}
