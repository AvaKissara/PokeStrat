using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MDate
    {
        public DateTime IdDate { get; set; }

        public MDate(DateTime idDate)
        {
            this.IdDate = idDate;
        }
    }
}
