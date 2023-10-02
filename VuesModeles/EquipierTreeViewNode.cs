using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class EquipierTreeViewNode : BaseVueModele
    {
        public ICommand AccueilPageCommand { get; set; }
        public MEquipier Equipier { get; }


        public EquipierTreeViewNode(MEquipier Equipier)
        {
            AccueilPageCommand = new RelayCommand(AccueilPage);
            this.Equipier = Equipier;
        }

        private void AccueilPage()
        {
            NavigationServices.NavigateToPage(new AccueilPage());
        }
    }
}
