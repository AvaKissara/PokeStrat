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
        public ICommand EnregistrerEquipierCommand {  get; set; }
        public ICommand SupprimerEquipierCommand { get; set; }
        private RelayCommand closeCommand;

        private WindowManager windowManager = new WindowManager();
        public MEquipier Equipier { get; set; }
        public MSpecimen Pokemon { get; set; }
        public MainWindow MainWindow { get; set; }

        private readonly RepTalent repTalent;
        private readonly RepCapacite repCapacite;
        private RepEquipe repEquipe;

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
        private MEquipier equipierSelectionne;
        public MEquipier EquipierSelectionne
        {
            get { return equipierSelectionne; }
            set
            {
                if (equipierSelectionne != value)
                {
                    if (equipierSelectionne != null)
                    {
                        equipierSelectionne.IsSelected = false;
                    }

                    equipierSelectionne = value;

                    if (equipierSelectionne != null)
                    {
                        equipierSelectionne.IsSelected = true;
                    }

                    OnPropertyChanged(nameof(EquipierSelectionne));

                    if (PokEquipierSelection != equipierSelectionne)
                    {
                        PokEquipierSelection = equipierSelectionne;
                    }

                    DetailPopup();
                }
            }
        }

        private MEquipier pokEquipierSelection;
        public MEquipier PokEquipierSelection
        {
            get { return pokEquipierSelection; }
            set
            {
                if (pokEquipierSelection != value)
                {
                    pokEquipierSelection = value;
                    OnPropertyChanged(nameof(PokEquipierSelection));

                    // Mettez à jour this.Equipier avec les modifications
                    this.Equipier = pokEquipierSelection;
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

                    if (pokSelection != null)
                    {
                        ImagePokemonSelectionne = new BitmapImage(new Uri(pokSelection.CheminImgPokemonAbsolu));
                    }
                    else
                    {
                        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        ImagePokemonSelectionne = new BitmapImage(new Uri(Path.Combine(appDirectory, "0.png")));
                    }
                    PokEquipierSelection = FromMSpecimen(pokSelection);
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

        private MCapacite capacite1Selection;
        public MCapacite Capacite1Selection
        {
            get { return capacite1Selection; }
            set
            {
                if (capacite1Selection != value)
                {
                    capacite1Selection = value;
                    OnPropertyChanged(nameof(Capacite1Selection));
                }
            }
        }

        private MCapacite capacite2Selection;
        public MCapacite Capacite2Selection
        {
            get { return capacite2Selection; }
            set
            {
                if (capacite2Selection != value)
                {
                    capacite2Selection = value;
                    OnPropertyChanged(nameof(Capacite2Selection));
                }
            }
        }

        private MCapacite capacite3Selection;
        public MCapacite Capacite3Selection
        {
            get { return capacite3Selection; }
            set
            {
                if (capacite3Selection != value)
                {
                    capacite3Selection = value;
                    OnPropertyChanged(nameof(Capacite3Selection));
                }
            }
        }

        private MCapacite capacite4Selection;
        public MCapacite Capacite4Selection
        {
            get { return capacite4Selection; }
            set
            {
                if (capacite4Selection != value)
                {
                    capacite4Selection = value;
                    OnPropertyChanged(nameof(Capacite4Selection));
                }
            }
        }

        MEquipier equipierCopy;

        public int EquipierSaisiEquipeId;

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
            EquipeId: EquipierSaisiEquipeId
            );

            DetailPopupCommand = new RelayCommand(DetailPopupMaj);
            EnregistrerEquipierCommand = new RelayCommand(EnregistrerEquipier);
            SupprimerEquipierCommand = new RelayCommand(SupprimerEquipier);
            repTalent = new RepTalent();

            this.Equipier = Equipier;
            if (Equipier == null)
            {
                this.Equipier = equipierParDefaut;               
                this.Equipier.TalentPokemon = repTalent.GetAll();
            }
   
            if (ImagePokemonSelectionne == null)
            {
                ImagePokemonSelectionne = new BitmapImage(new Uri(this.Equipier.CheminImgPokemonAbsolu));
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
            ); ; 

            if (specimen.TalentPokemon == null)
            {
                equipierEnSaisie.TalentPokemon = repTalent.GetTalent(specimen.IdPokemon);
            }
            equipierEnSaisie.EquipeId = EquipierSaisiEquipeId;

            return equipierEnSaisie;
        }

        
        private void DetailPopup()
        {
            if (this.Equipier.EquipeId != 0)
            {
                EquipierSaisiEquipeId = this.Equipier.EquipeId;
            }

            EquipierTreeViewNode equipeNode;

            if (NatureSelection != null)
            {
                this.PokEquipierSelection.Nature = NatureSelection;
            }
            if (TalentSelection != null)
            {
                this.PokEquipierSelection.TalentEquipier = TalentSelection;
            }
            if (ObjetSelection != null)
            {
                this.PokEquipierSelection.ObjetEquipier = ObjetSelection;
            }

            this.Equipier = PokEquipierSelection;
            if(this.Equipier != null)
            {
                equipierCopy = this.Equipier.Clone();
                if (Capacite1Selection != null)
                {
                    equipierCopy.SetCapacites[0].NomCapacite = Capacite1Selection.NomCapacite;
                }
                if (Capacite2Selection != null)
                {
                    if (equipierCopy.SetCapacites.Count>=2)
                    {
                        equipierCopy.SetCapacites[1] = Capacite2Selection;
                    }
                    else
                    {
                        equipierCopy.SetCapacites.Add(Capacite2Selection);
                    }
                    
                }
                if (Capacite3Selection != null)
                {
                    if (equipierCopy.SetCapacites.Count >= 3)
                    {
                        equipierCopy.SetCapacites[2] = Capacite3Selection;
                    }
                    else
                    {
                        equipierCopy.SetCapacites.Add(Capacite3Selection);
                    }
                    
                }
                if (Capacite4Selection != null)
                {
                    if (equipierCopy.SetCapacites.Count >= 4)
                    {
                        equipierCopy.SetCapacites[3] = Capacite4Selection;
                    }
                    else
                    {
                        equipierCopy.SetCapacites.Add(Capacite4Selection);
                    }
                   
                }
                equipeNode = new EquipierTreeViewNode(equipierCopy);
            }
            else
            {
                equipeNode = new EquipierTreeViewNode(this.Equipier);
            }
        
            var detailPopup = new DetailEquipe();
            detailPopup.Owner = MainWindow;
            windowManager.Register(detailPopup);
            detailPopup.DataContext = equipeNode;
            windowManager.ShowWindow("DetailEquipe", detailPopup);
        }

        private void DetailPopupMaj()
        {
            if(this.Equipier !=null)
            {
                this.EquipierSelectionne = this.Equipier;
            }
        }

        private void EnregistrerEquipier()
        {
            if(this.Equipier != null)
            {
                repEquipe = new RepEquipe();
                repEquipe.Add(this.Equipier);
            }
            this.Close();
        }

        private void SupprimerEquipier()
        {
            if (this.Equipier != null)
            {
                MEquipier equipierSupprimer = new MEquipier(this.Equipier.IdPokemon,this.Equipier.EquipeId, this.Equipier.TalentEquipier.IdTalent, this.Equipier.SetCapacites[0].IdCapacite, this.Equipier.SetCapacites[1].IdCapacite, this.Equipier.SetCapacites[2].IdCapacite, this.Equipier.SetCapacites[3].IdCapacite, this.Equipier.ObjetEquipier.IdObjet, this.Equipier.Nature.IdNature);
                repEquipe = new RepEquipe();
                repEquipe.Delete(equipierSupprimer);
            }
            this.Close();
        }

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
