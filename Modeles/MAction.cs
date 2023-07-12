using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MAction
    {
        public int idAction { get; }
        public bool baisse { get; set; }
        public bool augmente { get; set; }
        public double val { get; set; }
        public int tour { get; set; }
        public MMatch match { get; set; }
        public MStat statCible { get; set; }
    }
}
