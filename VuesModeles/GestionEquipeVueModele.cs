using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.User.GestionEquipe;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class GestionEquipeVueModele : BaseVueModele
    {
        public ICommand CreeCommand { get; set; }
        public ICommand AjouteCommand { get; set; }
        public ICommand ModifieCommand { get; set; }
        public ICommand MajCommand { get; set; }
        public ICommand EffaceCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand GestionCommand { get; set; }
        public ICommand CloseCommand { get; }

        private readonly RepEquipe repEquipe;
        private readonly ObservableCollection<MEquipe> _equipes;
        public IEnumerable<MEquipe> Equipes => _equipes;

        private ObservableCollection<MEquipier> _equipiers;

        public IEnumerable<MEquipier> Equipiers => _equipiers;
        //public ObservableCollection<MEquipier> Equipiers
        //{
        //    get { return _equipiers; }
        //    set
        //    {
        //        _equipiers = value;
        //        OnPropertyChanged(nameof(Equipiers));
        //    }
        //}

        private MEquipe selectedEquipe;
        public MEquipe SelectedEquipe
        {
            get { return selectedEquipe; }
            set
            {
                selectedEquipe = value;
                OnPropertyChanged(nameof(SelectedEquipe));

                // Mettez à jour la liste d'équipiers en fonction de l'équipe sélectionnée
                _equipiers = selectedEquipe?.Equipiers;
            }
        }


        private int idEquipe;
        public int IdEquipe
        {
            get { return idEquipe; }
            set
            {
                idEquipe = value;
                OnPropertyChanged(nameof(IdEquipe));
            }
        }

        public string NomEquipe { get; set; }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private ObservableCollection<EquipeTreeViewNode> _equipesTreeView;

        public ObservableCollection<EquipeTreeViewNode> EquipesTreeView
        {
            get { return _equipesTreeView; }
            set
            {
                _equipesTreeView = value;
                OnPropertyChanged(nameof(EquipesTreeView));
            }
        }
        public GestionEquipeVueModele()
        {
            GestionCommand = new RelayCommand(GestionEquipe);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);
            repEquipe = new RepEquipe();
            _equipes = repEquipe.GetAllEquipes();
            _equipiers = repEquipe.GetEquipiers();
            _equipesTreeView = InitializeTreeViewData();
        }

        private ObservableCollection<EquipeTreeViewNode> InitializeTreeViewData()
        {
            ObservableCollection<EquipeTreeViewNode> equipeNodes = new ObservableCollection<EquipeTreeViewNode>();

            ObservableCollection<MEquipe> equipes = _equipes;

            foreach (var equipe in equipes)
            {
                var equipeNode = new EquipeTreeViewNode(equipe);

                // Récupérez les équipiers correspondant à cette équipe
                var equipeEquipiers = _equipiers.Where(e => e.EquipeId == equipe.IdEquipe).ToList();



                foreach (var equipier in equipeEquipiers)
                {
                    equipeNode.Equipiers.Add(new EquipierTreeViewNode(equipier));
                }

                equipeNodes.Add(equipeNode);
            }

            return equipeNodes;
        }



        private void GestionEquipe()
        {
            NavigationServices.NavigateToPage(new GestionEquipe());
        }

        private void AccueilPage()
        {
            NavigationServices.NavigateToPage(new AccueilPage());
        }

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
