using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class EquipierTreeViewNode : BaseVueModele
    {
        public ICommand DetailPopupCommand { get; set; }
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        private MEquipier selectedEquipeNode;
        public MEquipier SelectedEquipeNode
        {
            get { return selectedEquipeNode; }
            set
            {
                if (selectedEquipeNode != value)
                {
                    if (selectedEquipeNode != null)
                    {
                        selectedEquipeNode.IsSelected = false;
                    }

                    selectedEquipeNode = value;

                    if (selectedEquipeNode != null)
                    {
                        selectedEquipeNode.IsSelected = true;
                    }

                    OnPropertyChanged(nameof(SelectedEquipeNode));

                    // Appelez la méthode DetailPopup ici
                    DetailPopup();
                }
            }
        }
        public MEquipier Equipier { get; }
        public MainWindow MainWindow { get; private set; }

        public EquipierTreeViewNode(MEquipier Equipier)
        {
            DetailPopupCommand = new RelayCommand(DetailPopup);
            this.Equipier = Equipier;
        }

  

        private void DetailPopup()
        {

            if (SelectedEquipeNode != null)
            {
                // Créez une nouvelle instance de EquipeTreeViewNode pour l'équipe sélectionnée
                var equipeNode = new EquipierTreeViewNode(SelectedEquipeNode);

                // Créez une nouvelle instance de DetailEquipe en passant l'équipeNode comme DataContext
                var detailPopup = new DetailEquipe();
                detailPopup.DataContext = equipeNode;

                detailPopup.ShowDialog();
            }
     

        }

        private RelayCommand closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(Close);
                }

                return closeCommand;
            }
        }

        private void Close()
        {

            var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            activeWindow?.Close();

            NavigationServices.NavigateToPage(new GestionEquipe());
        }
    }
}
