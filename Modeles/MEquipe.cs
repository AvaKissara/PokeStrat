using PokeStat.Utilitaires;
using PokeStat.Vues.User.GestionEquipe;
using PokeStat.VuesModeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.Modeles
{
    public class MEquipe
    {
        public ICommand OuvrirPopupCommand { get; private set; }
        public int IdEquipe { get; set; }
        public string NomEquipe { get; set; }
        public MUser User { get; set; }
        public MDate DateCreation { get; }
        public ObservableCollection<MEquipier> Equipiers { get; set; }
        public MEquipier nouvelEquipier { get; set; }

        public MEquipe(int idEquipe, string nomEquipe, MDate dateCreation, MUser user, ObservableCollection<MEquipier> equipiers)
        {
            OuvrirPopupCommand = new RelayCommand(OuvrirPopup);
            this.IdEquipe = idEquipe;
            this.NomEquipe = nomEquipe;
            this.DateCreation = dateCreation;
            this.User = user;
            this.Equipiers = equipiers;
            this.Equipiers = new ObservableCollection<MEquipier>();
        }
        private void OuvrirPopup()
        {
            var popupViewModel = new EquipierTreeViewNode(nouvelEquipier);  
            var popup = new DetailEquipe();  
            popup.DataContext = popupViewModel;
            popup.Show();
        }
    }
}
