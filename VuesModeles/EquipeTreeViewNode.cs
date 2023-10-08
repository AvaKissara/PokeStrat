using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class EquipeTreeViewNode : BaseVueModele
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
        public MEquipe Equipe { get; }
        public ObservableCollection<EquipierTreeViewNode> Equipiers { get; }= new ObservableCollection<EquipierTreeViewNode>();
        public EquipeTreeViewNode(MEquipe Equipe)
        {
            this.Equipe = Equipe;
        }
        public EquipeTreeViewNode(MEquipe Equipe, ObservableCollection<EquipierTreeViewNode> Equipiers)
        {
            DetailPopupCommand = new RelayCommand(DetailPopup);
            this.Equipe = Equipe;
            this.Equipiers = Equipiers;

        }
        public MainWindow MainWindow { get; set; }
        private WindowManager windowManager = new WindowManager();

        private void DetailPopup()
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

            var equipeNode = new EquipierTreeViewNode(equipierParDefaut);
            //var activeWindow2 = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            var detailPopup = new DetailEquipe();
            detailPopup.Owner = MainWindow;
            windowManager.Register(detailPopup);
            detailPopup.DataContext = equipeNode;

            //var equipeNode = new EquipierTreeViewNode(EquipierSelectionne);

            //var detailPopup = new DetailEquipe();
            //detailPopup.DataContext = equipeNode;
            windowManager.ShowWindow("DetailEquipe", detailPopup);

        }

    }
}