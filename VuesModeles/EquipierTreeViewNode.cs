using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokeStat.VuesModeles
{
    public class EquipierTreeViewNode : BaseVueModele
    {
       
        public ICommand DetailPopupCommand { get; set; }

        private WindowManager windowManager = new WindowManager();


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

                    // Vérifiez si PokEquipierSeletion est également modifié
                    if (PokEquipierSeletion != equipierSeletionne)
                    {
                        PokEquipierSeletion = equipierSeletionne;
                    }

                    DetailPopup();
                }
            }
        }

        private MEquipier pokEquipierSeletion;
        public MEquipier PokEquipierSeletion
        {
            get { return pokEquipierSeletion; }
            set
            {
                if (pokEquipierSeletion != value)
                {
                    pokEquipierSeletion = value;
                    OnPropertyChanged(nameof(PokEquipierSeletion));
                }
            }
        }


        private MSpecimen pokSelection;
        public MSpecimen PokSelection
        {
            get { return pokSelection; }
            set
            {
                if (pokSelection != value)
                {
                    pokSelection = value;
                    OnPropertyChanged(nameof(PokSelection));

                    // Mettez à jour l'image du Pokémon sélectionné
                    if (pokSelection != null)
                    {
                        ImagePokemonSelectionne = new BitmapImage(new Uri(pokSelection.CheminImgPokemonAbsolu));
                    }
                    else
                    {
                        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        // Si aucun Pokémon n'est sélectionné, utilisez l'image par défaut
                        ImagePokemonSelectionne = new BitmapImage(new Uri(Path.Combine(appDirectory, "0.png")));
                    }

                    // Mettez à jour EquipierSeletionne avec FromMSpecimen
                    PokEquipierSeletion = FromMSpecimen(pokSelection);
                }
            }
        }

        private MNature natureSelection;
        public MNature NatureSelection
        {
            get { return natureSelection; }
            set
            {
                if (natureSelection != value)
                {
                    natureSelection = value;
                    OnPropertyChanged(nameof(NatureSelection));
                }
            }
        }


        private MTalent talentSelection;
        public MTalent TalentSelection
        {
            get { return talentSelection; }
            set
            {
                if (talentSelection != value)
                {
                    talentSelection = value;
                    OnPropertyChanged(nameof(TalentSelection));
                }
            }
        }

        private MObjet objetSelection;
        public MObjet ObjetSelection
        {
            get { return objetSelection; }
            set
            {
                if (objetSelection != value)
                {
                    objetSelection = value;
                    OnPropertyChanged(nameof(ObjetSelection));
                }
            }
        }

        private ImageSource imagePokemonSelectionne;
        public ImageSource ImagePokemonSelectionne
        {
            get { return imagePokemonSelectionne; }
            set
            {
                if (imagePokemonSelectionne != value)
                {
                    imagePokemonSelectionne = value ?? new BitmapImage(new Uri(PokSelection.CheminImgPokemonAbsolu));
                    OnPropertyChanged(nameof(ImagePokemonSelectionne));
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
            repTalent = new RepTalent();

            this.Equipier = Equipier;
            if (Equipier == null)
            {
                this.Equipier = equipierParDefaut;               
                this.Equipier.TalentPokemon = repTalent.GetAll();
            }
        }
        public MEquipier FromMSpecimen(MSpecimen specimen)
        {
            if (specimen == null)
            {
                return null;
            }
            MEquipier equipierEnSaisie =
                     new MEquipier(
                IdPokemon: PokSelection.IdPokemon,
                CheminImgPokemon: PokSelection.CheminImgPokemonAbsolu,
                NomFraPokemon: PokSelection.NomFraPokemon,
                NomEngPokemon: PokSelection.NomEngPokemon,
                NumPokemon: PokSelection.NumPokemon,
                TaillePokemon: PokSelection.TaillePokemon,
                PoidsPokemon: PokSelection.PoidsPokemon,
                BasePV: PokSelection.BasePV,
                BaseAttaque: PokSelection.BaseAttaque,
                BaseDefense: PokSelection.BaseDefense,
                BaseAttSpe: PokSelection.BaseAttSpe,
                BaseDefSpe: PokSelection.BaseDefSpe,
                BaseVit: PokSelection.BaseVit,
                Legendaire: PokSelection.Legendaire,
                Shiny: PokSelection.Shiny,
                Mega: PokSelection.Mega,
                Giga: PokSelection.Giga,
                Fab: PokSelection.Fab,
                Evolution: PokSelection.Evolution,
                Gen: PokSelection.Gen,
                
                SurnomEquipier: Equipier.SurnomEquipier,
                NiveauEquipier: Equipier.NiveauEquipier             
            ); 
            if (specimen.TalentPokemon == null)
            {
               equipierEnSaisie.TalentPokemon = repTalent.GetTalent(specimen.IdPokemon);
            }
            return equipierEnSaisie;
        }


     
        private void DetailPopup()
        {
            if (PokEquipierSeletion != null)
            {
                PokEquipierSeletion.SurnomEquipier = this.Equipier.SurnomEquipier;
                PokEquipierSeletion.NiveauEquipier = this.Equipier.NiveauEquipier;

            }
            if(NatureSelection!=null) 
            {
                this.PokEquipierSeletion.Nature = NatureSelection;
            }
            if(TalentSelection!=null)
            {
                this.pokEquipierSeletion.TalentEquipier = TalentSelection;
            }
            if(ObjetSelection!=null)
            {
                this.pokEquipierSeletion.ObjetEquipier = ObjetSelection;
            }
            this.Equipier = PokEquipierSeletion;
           
            var equipeNode = new EquipierTreeViewNode(this.Equipier);      
            var detailPopup = new DetailEquipe();
            detailPopup.Owner = MainWindow;
            windowManager.Register(detailPopup);
            detailPopup.DataContext = equipeNode;
            windowManager.ShowWindow("DetailEquipe", detailPopup);
          
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
          
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = new AccueilVueModel();
            windowManager.Register(mainWindow);
            NavigationServices.NavigateToPage(new GestionEquipe());
            windowManager.ShowWindow("Mainwindow", mainWindow);
        }
    }
}
