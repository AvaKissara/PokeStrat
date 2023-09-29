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
        public int IdEquipe { get; set; }
        public string NomEquipe { get; set; }
        public MUser User { get; set; }
        public MDate DateCreation { get; }
        public ObservableCollection<MEquipier> Equipiers { get; set; }

        public MEquipe(int idEquipe, string nomEquipe, MDate dateCreation, MUser user, ObservableCollection<MEquipier> equipiers)
        {
            this.IdEquipe = idEquipe;
            this.NomEquipe = nomEquipe;
            this.DateCreation = dateCreation;
            this.User = user;
            this.Equipiers = equipiers;
            this.Equipiers = new ObservableCollection<MEquipier>();
        }
    }
}
