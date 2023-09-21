using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MEquipe
    {
        public int idEquipe { get; set; }
        public string nomEquipe { get; set; }
        public MUser user { get; set; }
        public MDate dateCreation { get; }
        public ObservableCollection<MEquipier> equipiers { get; set; }

        public MEquipe(int IdEquipe, string NomEquipe, MUser User, MDate DateCreation, ObservableCollection<MEquipier> Equipiers)
        {
            this.idEquipe = IdEquipe;
            this.nomEquipe = NomEquipe;
            this.user = User;
            this.dateCreation = DateCreation;
            this.equipiers = Equipiers;
            this.equipiers = new ObservableCollection<MEquipier>();
        }
    }
}
