using PokeStat.Modeles;
using PokeStat.Repositories;
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
        public ICommand AjouteCommand { get; set; }
        private RepEquipe repEquipe { get; set; }

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
        public MEquipe Equipe { get; set; }
        public ObservableCollection<EquipierTreeViewNode> Equipiers { get; }= new ObservableCollection<EquipierTreeViewNode>();
        public EquipeTreeViewNode(MEquipe Equipe)
        {
            repEquipe = new RepEquipe();
            this.Equipe = Equipe;
            AjouteCommand = new RelayCommand(AjouteEquipe);
        }
        public EquipeTreeViewNode(MEquipe Equipe, ObservableCollection<EquipierTreeViewNode> Equipiers)
        {
            repEquipe = new RepEquipe();
            DetailPopupCommand = new RelayCommand(DetailPopup);
            this.Equipe = Equipe;
            this.Equipiers = Equipiers;
            AjouteCommand = new RelayCommand(AjouteEquipe);
        }
        public MainWindow MainWindow { get; set; }

        private void DetailPopup()
        {
            int equipeId = repEquipe.GetLastEquipeId();
            MEquipier equipierOrigineDefaut = null;
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
                       EquipeId: equipeId,
                       equipierOrigineDefaut
                       );

            var equipeNode = new EquipierTreeViewNode(equipierParDefaut);

            var detailPopup = new DetailEquipe();
            detailPopup.Owner = MainWindow;
            detailPopup.DataContext = equipeNode;
            using (var manager = new WindowManager())
            {
                manager.Register(detailPopup);
                manager.ShowWindow("DetailEquipe", detailPopup);
            }       
        }

        public void AjouteEquipe()
        {
            MEquipe equipeAAjouter = new MEquipe(this.Equipe.IdEquipe, this.Equipe.NomEquipe);
            if(this.Equipe.NomEquipe!=null) 
            {
                using (var repository = new RepEquipe())
                {
                    repository.AddEquipe(equipeAAjouter.NomEquipe, SessionManager.Instance.Account.IdPersonne);
                }
               
            }
            DetailPopup();
        }
    }
}