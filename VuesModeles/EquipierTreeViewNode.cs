using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

                    DetailPopup();
                }
            }
        }
        public MEquipier Equipier { get; }
        public MainWindow MainWindow { get; private set; }

        public EquipierTreeViewNode(MEquipier Equipier)
        {
            MEquipier equipierParDefaut = new MEquipier(
            IdPokemon: 0,
            CheminImgPokemon: "0.png", 
            NomFraPokemon: "Nouveau", 
            BasePV: 0, 
            BaseAttaque: 0, 
            BaseDefense: 0, 
            BaseAttSpe: 0, 
            BaseDefSpe: 0, 
            BaseVit: 0, 
            Legendaire: false, 
            Shiny: false, 
            Mega: false, 
            Giga: false, 
            Fab: false, 
            SurnomEquipier: "Nouveau", 
            NiveauEquipier: 0, 
            EsquiveEquipier: 0, 
            NiveauBonheur: 0, 
            Ev: 0, 
            Iv: 0, 
            Nature: null,  
            TalentEquipier: null, 
            ObjetEquipier: null,  
            SetCapacites: new ObservableCollection<MCapacite>(), 
            EquipeId: 0 
            );

            DetailPopupCommand = new RelayCommand(DetailPopup);
           
            this.Equipier = Equipier;
            if(Equipier==null)
            {
                this.Equipier = equipierParDefaut;
            }
        }

        private void DetailPopup()
        {
            if (SelectedEquipeNode != null)
            {
                var equipeNode = new EquipierTreeViewNode(SelectedEquipeNode);

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
            MainWindow mainWindow = new MainWindow();
      
            NavigationServices.NavigateToPage(new GestionEquipe());
            mainWindow.ShowDialog();

        }
    }
}
