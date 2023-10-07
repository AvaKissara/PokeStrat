using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private MEquipier equipierSeletionne;
        public MEquipier EquipierSeletionne
        {
            get { return equipierSeletionne; }
            set
            {
                if (equipierSeletionne != value)
                {
                    if (equipierSeletionne != null)
                    {
                        equipierSeletionne.IsSelected = false;
                    }

                    equipierSeletionne = value;

                    if (equipierSeletionne != null)
                    {
                        equipierSeletionne.IsSelected = true;
                    }

                    OnPropertyChanged(nameof(EquipierSeletionne));

                    DetailPopup();
                }
            }
        }
        public MEquipier Equipier { get; set; }
        public MSpecimen Pokemon { get; set; }  
        public MainWindow MainWindow { get; set; }
        private readonly RepTalent repTalent;

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
                repTalent = new RepTalent();
                this.Equipier.TalentPokemon = repTalent.GetAll();
            }
           
        }
        public EquipierTreeViewNode(MSpecimen PokemonSaisi)
        {
            this.Pokemon = PokemonSaisi;
            this.Equipier = FromMSpecimen(PokemonSaisi);
            DetailPopupCommand = new RelayCommand(DetailPopup);
        }
        public MEquipier FromMSpecimen(MSpecimen specimen)
        {
            this.Pokemon = specimen;
            
            if (specimen == null)
            {
                return null;
            }
            MEquipier equipierEnSaisie =
                     new MEquipier(
                IdPokemon: Pokemon.IdPokemon,
                CheminImgPokemon: Pokemon.CheminImgPokemon,
                NomFraPokemon: Pokemon.NomFraPokemon,
                NomEngPokemon: Pokemon.NomEngPokemon,
                NumPokemon: Pokemon.NumPokemon,
                TaillePokemon: Pokemon.TaillePokemon,
                PoidsPokemon: Pokemon.PoidsPokemon,
                BasePV: Pokemon.BasePV,
                BaseAttaque: Pokemon.BaseAttaque,
                BaseDefense: Pokemon.BaseDefense,
                BaseAttSpe: Pokemon.BaseAttSpe,
                BaseDefSpe: Pokemon.BaseDefSpe,
                BaseVit: Pokemon.BaseVit,
                Legendaire: Pokemon.Legendaire,
                Shiny: Pokemon.Shiny,
                Mega: Pokemon.Mega,
                Giga: Pokemon.Giga,
                Fab: Pokemon.Fab,
                Evolution: Pokemon.Evolution,
                Gen: Pokemon.Gen,
                SurnomEquipier: "test",
                NiveauEquipier: 1
            );
            return equipierEnSaisie;
        }
        private void DetailPopup()
        {
            if (EquipierSeletionne != null)
            {
                var equipeNode = new EquipierTreeViewNode(EquipierSeletionne);

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
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = new AccueilVueModel();
            NavigationServices.NavigateToPage(new GestionEquipe());
            mainWindow.ShowDialog();
            activeWindow?.Close();

        }
    }
}
