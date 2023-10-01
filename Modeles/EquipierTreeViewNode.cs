using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class EquipierTreeViewNode
    {
        public MEquipier Equipier { get; }

        
        public EquipierTreeViewNode(MEquipier Equipier)
        {
            this.Equipier = Equipier;
        }
    }
}
