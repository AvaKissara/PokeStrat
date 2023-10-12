﻿using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Services;
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
        private StatsCalculator statsCalculator;


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

        public MEquipier EquipierAModId { get; set; }

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
        private string surnomPokEquipier;
        public string SurnomPokEquipier
        {
            get { return surnomPokEquipier; }
            set
            {
                if (surnomPokEquipier != value)
                {
                    surnomPokEquipier = value;
                    OnPropertyChanged(nameof(SurnomPokEquipier));
                }            
            }
        }

        private int nivPokEquipier;
        public int NivPokEquipier
        {
            get { return nivPokEquipier; }
            set
            {
                if (nivPokEquipier != value)
                {
                    nivPokEquipier = value;
                    OnPropertyChanged(nameof(NivPokEquipier));
                }
            }
        }

        private double basePVPercentage;
        public double BasePVPercentage
        {
            get { return basePVPercentage; }
            set
            {
                if (basePVPercentage != value)
                {
                    basePVPercentage = value;
                    OnPropertyChanged(nameof(BasePVPercentage));
                   
                }
                
            }

        }

        private int basePV;
        public int BasePV
        {
            get { return basePV; }
            set
            {
                if (basePV != value)
                {
                    basePV = value;
                    OnPropertyChanged(nameof(BasePV));
                }
            }
        }

        //public void MajPVPourcentage()
        //{
        //    int max= this.Equipier.BasePV + 252;
        //    double calculPourcentage = ((double)this.Equipier.BasePV / max)*100;

        //    if (calculPourcentage > max)
        //    {
        //        calculPourcentage = (int)max;
        //    }
        //    BasePVPercentage = Math.Round(calculPourcentage);

        //}
        public MSpecimen pokemonRef;
        public void MajPourcentageStat(string nomStat, double valeurPourcentage)
        {
            List<MSpecimen> pokemons = this.Equipier.pokemons;
            pokemonRef = pokemons.Find(pokemon => pokemon.IdPokemon == this.Equipier.IdPokemon); 

            int valeurBase = 0;
            int max = 252; 

            switch (nomStat)
            {
                case "PV":
                    valeurBase = pokemonRef.BasePV;
                    max = valeurBase + max;
                    break;
                case "Attaque":
                    valeurBase = this.Equipier.BaseAttaque;
                    max = valeurBase + max;
                    break;
                case "Défense":
                    valeurBase = this.Equipier.BaseDefense;
                    max = valeurBase + max;
                    break;
                case "Att. Spe":
                    valeurBase = this.Equipier.BaseAttSpe;
                    max = valeurBase + max;
                    break;
                case "Def. Spe":
                    valeurBase = this.Equipier.BaseDefSpe;
                    max = valeurBase + max;
                    break;
                case "Vitesse":
                    valeurBase = this.Equipier.BaseVit;
                    max = valeurBase + max;
                    break;
                default:

                    break;
            }

            double calculPourcentage = (valeurPourcentage / max) * 100.0;
           

            if (calculPourcentage > max)
            {
                calculPourcentage = max;
            }
            int nouvelleValeurBase = (int)calculPourcentage;
 
            switch (nomStat)
            {
                case "PV":
                    this.BasePVPercentage = nouvelleValeurBase;
                    //this.BasePV = nouvelleValeurEntier;
                    break;
                case "Attaque":
                    this.Equipier.BaseAttaque = nouvelleValeurBase;
                    break;
                case "Défense":
                    this.Equipier.BaseDefense = nouvelleValeurBase;
                    break;
                case "Att. Spe":
                    this.Equipier.BaseAttSpe = nouvelleValeurBase;
                    break;
                case "Def. Spe":
                    this.Equipier.BaseDefSpe = nouvelleValeurBase;
                    break;
                case "Vitesse":
                    this.Equipier.BaseVit = nouvelleValeurBase;
                    break;
                default:

                    break;
            }
        }

        public EquipierTreeViewNode(MEquipier Equipier)
        {          
            MEquipier equipierParDefaut = new MEquipier(
            IdPokemon: 0,
            CheminImgPokemon: "0.png",
            NomFraPokemon: "",
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
            SurnomEquipier: "",
            NiveauEquipier: 0,
            EsquiveEquipier: 0,
            NiveauBonheur: 0,
            Ev: 0,
            Iv: 0,
            Nature: null,
            TalentEquipier: null,
            ObjetEquipier: null,
            SetCapacites: new ObservableCollection<MCapacite>(),
            EquipeId: Equipier.EquipeId,
            Equipier.equipierOrigine
            );

            DetailPopupCommand = new RelayCommand(DetailPopupMaj);
            EnregistrerEquipierCommand = new RelayCommand(EnregistrerEquipier);
            SupprimerEquipierCommand = new RelayCommand(SupprimerEquipier);

            repTalent = new RepTalent();
            statsCalculator = new StatsCalculator();

            this.Equipier = Equipier;
            if (Equipier == null)
            {
                this.Equipier = equipierParDefaut; 
               
                this.Equipier.TalentPokemon = repTalent.GetAll();
            }
            if (Equipier.equipierOrigine == null)
            {
                this.Equipier.equipierOrigine = equipierParDefaut;
            }
            else
            {              
                    MajStats();
                               
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

                SurnomEquipier: SurnomPokEquipier,
                NiveauEquipier: NivPokEquipier
            ); ; 

            if (specimen.TalentPokemon == null)
            {
                equipierEnSaisie.TalentPokemon = repTalent.GetTalent(specimen.IdPokemon);
            }
            
            return equipierEnSaisie;
        }

        public MEquipier GetEquipierAModId(MEquipier equipierModification)
        {
          
                MEquipier equipierAModId = new MEquipier(
              equipierModification.EquipeId,
              equipierModification.TalentEquipier.IdTalent,
              equipierModification.IdPokemon,
              equipierModification.SetCapacites[0].IdCapacite,
              equipierModification.SetCapacites[1].IdCapacite,
              equipierModification.SetCapacites[2].IdCapacite,
              equipierModification.SetCapacites[3].IdCapacite,
              equipierModification.ObjetEquipier.IdObjet,
              equipierModification.Nature.IdNature
              );
            
            return equipierAModId;
        }

        private void DetailPopup()
        {
            if (this.Equipier.equipierOrigine!=null)
            {
                this.EquipierAModId = Equipier.equipierOrigine;
            }
            if(this.EquipierAModId!=null)
            {
                Equipier.equipierOrigine = this.EquipierAModId;
            }
            if (this.Equipier.equipierOrigine.IsSelected == true)
            {
                this.IsSelected = true;
            }
            else if (this.Equipier.equipierOrigine.IsSelected==false)
            {
                this.IsSelected = false;
            }

            EquipierTreeViewNode equipeNode;

            if (this.Equipier.EquipeId != 0)
            {
                EquipierSaisiEquipeId = this.Equipier.EquipeId;
            }
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
           
            if (this.Equipier != null)
            {
                equipierCopy = this.Equipier.Clone();
                if (Capacite1Selection != null)
                {
                    equipierCopy.SetCapacites[0] = Capacite1Selection;
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
                if(this.surnomPokEquipier!=null)
                {
                    this.Equipier.SurnomEquipier = this.surnomPokEquipier;
                }
                if(this.NivPokEquipier!=0)
                {
                    this.Equipier.NiveauEquipier = this.nivPokEquipier;
                }
               if(Capacite2Selection==null && this.Equipier.SetCapacites !=null) 
               {
                    if (this.Equipier.SetCapacites.Count() > 1)
                    {
                        this.Equipier.SetCapacites.Remove(this.Equipier.SetCapacites[1]);
                    }
               }
               if (Capacite3Selection == null && this.Equipier.SetCapacites != null)
               {
                    if(this.Equipier.SetCapacites.Count() >2)
                    {
                        this.Equipier.SetCapacites.Remove(this.Equipier.SetCapacites[2]);
                    }                 
               }
                if (Capacite4Selection == null && this.Equipier.SetCapacites != null)
                {
                    if (this.Equipier.SetCapacites.Count() > 3)
                    {
                        this.Equipier.SetCapacites.Remove(this.Equipier.SetCapacites[3]);
                    }
                        
                }
                if(this.BasePV!=0)
                {
                    this.Equipier.BasePV = this.BasePV; 
                }
                this.EquipierSelectionne = this.Equipier;
            }

        }

        public void MajStats()
        {
            this.BasePV = this.Equipier.BasePV;
            this.BasePVPercentage = this.BasePV;
            if (BasePVPercentage != 0)
            {
                this.MajPourcentageStat("PV", BasePVPercentage);
            }
        }
        private void EnregistrerEquipier()
        {
            if(this.Equipier != null)
            {
                repEquipe = new RepEquipe();
           
                if (this.Equipier.equipierOrigine.IsSelected == true)
                {
               
                    MEquipier equipierEnModification = GetEquipierAModId(this.Equipier.equipierOrigine);
                    repEquipe.Update(this.Equipier, equipierEnModification);
                }
                else
                {
                    repEquipe.Add(this.Equipier);
                }
            }
            this.Close();
        }

        private void SupprimerEquipier()
        {
            if (this.Equipier != null)
            {
                MEquipier equipierSupprimer = new MEquipier(this.Equipier.EquipeId, this.Equipier.TalentEquipier.IdTalent, this.Equipier.IdPokemon, this.Equipier.SetCapacites[0].IdCapacite, this.Equipier.SetCapacites[1].IdCapacite, this.Equipier.SetCapacites[2].IdCapacite, this.Equipier.SetCapacites[3].IdCapacite, this.Equipier.ObjetEquipier.IdObjet, this.Equipier.Nature.IdNature);
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

        MainWindow mainWindow;
        private void Close()
        {
            mainWindow = new MainWindow();
            mainWindow.DataContext = new AccueilVueModel();
            windowManager.Register(mainWindow);
            NavigationServices.NavigateToPage(new GestionEquipe());
            windowManager.ShowWindow("Mainwindow", mainWindow);
        }

        //private void CloseCap()
        //{
        //    var detailPopup = new DetailEquipe();
        //    detailPopup.Owner = MainWindow;
        //    windowManager.Register(detailPopup);
        //    detailPopup.DataContext = this;
        //    windowManager.ShowWindow("DetailEquipe", detailPopup);
        //}
    }
}
