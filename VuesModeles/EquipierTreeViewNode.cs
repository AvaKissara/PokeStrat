using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Services;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.Authentification;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
  
        private StatsCalculator statsCalculator;
        public MSpecimen pokemonRef;


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

        private double basePVPourcentage;
        public double BasePVPourcentage
        {
            get { return basePVPourcentage; }
            set
            {
                if (basePVPourcentage != value)
                {
                    basePVPourcentage = value;
                    OnPropertyChanged(nameof(BasePVPourcentage));  
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

        private double baseAttaquePourcentage;
        public double BaseAttaquePourcentage
        {
            get { return baseAttaquePourcentage; }
            set
            {
                if (baseAttaquePourcentage != value)
                {
                    baseAttaquePourcentage = value;
                    OnPropertyChanged(nameof(BaseAttaquePourcentage));
                }
            }
        }

        private int baseAttaque;
        public int BaseAttaque
        {
            get { return baseAttaque; }
            set
            {
                if (baseAttaque != value)
                {
                    baseAttaque = value;
                    OnPropertyChanged(nameof(BaseAttaque));
                }
            }
        }

        private double baseDefensePourcentage;
        public double BaseDefensePourcentage
        {
            get { return baseDefensePourcentage; }
            set
            {
                if (baseDefensePourcentage != value)
                {
                    baseDefensePourcentage = value;
                    OnPropertyChanged(nameof(BaseDefensePourcentage));
                }
            }
        }

        private int baseDefense;
        public int BaseDefense
        {
            get { return baseDefense; }
            set
            {
                if (baseDefense != value)
                {
                    baseDefense = value;
                    OnPropertyChanged(nameof(BaseDefense));
                }
            }
        }

        private double baseAttSpePourcentage;
        public double BaseAttSpePourcentage
        {
            get { return baseAttSpePourcentage; }
            set
            {
                if (baseAttSpePourcentage != value)
                {
                    baseAttSpePourcentage = value;
                    OnPropertyChanged(nameof(BaseAttSpePourcentage));
                }
            }
        }

        private int baseAttSpe;
        public int BaseAttSpe
        {
            get { return baseAttSpe; }
            set
            {
                if (baseAttSpe != value)
                {
                    baseAttSpe = value;
                    OnPropertyChanged(nameof(BaseAttSpe));
                }
            }
        }

        private double baseDefSpePourcentage;
        public double BaseDefSpePourcentage
        {
            get { return baseDefSpePourcentage; }
            set
            {
                if (baseDefSpePourcentage != value)
                {
                    baseDefSpePourcentage = value;
                    OnPropertyChanged(nameof(BaseDefSpePourcentage));
                }
            }
        }

        private int baseDefSpe;
        public int BaseDefSpe
        {
            get { return baseDefSpe; }
            set
            {
                if (baseDefSpe != value)
                {
                    baseDefSpe = value;
                    OnPropertyChanged(nameof(BaseDefSpe));
                }
            }
        }
        private double baseVitPourcentage;
        public double BaseVitPourcentage
        {
            get { return baseVitPourcentage; }
            set
            {
                if (baseVitPourcentage != value)
                {
                    baseVitPourcentage = value;
                    OnPropertyChanged(nameof(BaseVitPourcentage));
                }
            }
        }

        private int baseVit;
        public int BaseVit
        {
            get { return baseVit; }
            set
            {
                if (baseVit != value)
                {
                    baseVit = value;
                    OnPropertyChanged(nameof(BaseVit));
                }
            }
        }

        private int evTotal;
        public int EvTotal
        {
            get { return evTotal; }
            set
            {
                if (evTotal != value)
                {
                    evTotal = value;
                    OnPropertyChanged(nameof(EvTotal));
                }
            }
        }

        private int evStat;
        public int EvStat
        {
            get { return evStat; }
            set
            {
                if (evStat != value)
                {
                    evStat = value;
                    OnPropertyChanged(nameof(EvStat));
                }
            }
        }

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
                    valeurBase = pokemonRef.BaseAttaque;
                    max = valeurBase + max;
                    break;
                case "Défense":
                    valeurBase = pokemonRef.BaseDefense;
                    max = valeurBase + max;
                    break;
                case "Att Spe":
                    valeurBase = pokemonRef.BaseAttSpe;
                    max = valeurBase + max;
                    break;
                case "Def Spe":
                    valeurBase = pokemonRef.BaseDefSpe;
                    max = valeurBase + max;
                    break;
                case "Vitesse":
                    valeurBase = pokemonRef.BaseVit;
                    max = valeurBase + max;
                    break;
                default:

                    break;
            }

            double calculPourcentage = (valeurPourcentage / max) * 100;
           

            if (calculPourcentage > max)
            {
                calculPourcentage = max;
            }
            int nouvelleValeurBase = (int)calculPourcentage;
 
            switch (nomStat)
            {
                case "PV":
                    this.BasePVPourcentage = nouvelleValeurBase;
         
                    break;
                case "Attaque":
                    this.BaseAttaquePourcentage = nouvelleValeurBase;
                    break;
                case "Défense":
                    this.BaseDefensePourcentage = nouvelleValeurBase;
                    break;
                case "Att Spe":
                    this.BaseAttSpePourcentage = nouvelleValeurBase;
                    break;
                case "Def Spe":
                    this.BaseDefSpePourcentage = nouvelleValeurBase;
                    break;
                case "Vitesse":
                    this.BaseVitPourcentage = nouvelleValeurBase;
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
            if(this.Equipier.Ev==0 && this.Equipier.Iv==0) 
            {
                equipierEnSaisie.Ev = 510;
                equipierEnSaisie.Iv = 31;
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
            detailPopup.DataContext = equipeNode;
            using (var manager = new WindowManager())
            {
                manager.Register(detailPopup);
                manager.ShowWindow("DetailEquipe", detailPopup);
            }
                                   
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
                if(Capacite1Selection!=null)
                {
                    if (Capacite2Selection == null && this.Equipier.SetCapacites != null)
                    {
                        if (this.Equipier.SetCapacites.Count() > 1)
                        {
                            this.Equipier.SetCapacites.Remove(this.Equipier.SetCapacites[1]);
                        }
                    }
                    if (Capacite3Selection == null && this.Equipier.SetCapacites != null)
                    {
                        if (this.Equipier.SetCapacites.Count() > 2)
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
                }
               
                if(this.BasePV!=0)
                {
                    //if (pokemonRef != null)
                    //{
                    //    this.Equipier.Ev = this.EvTotal;
                        
                    //    this.BasePV = statsCalculator.CalculPV(pokemonRef.BasePV, this.Equipier.Iv, this.EvStat, this.Equipier.NiveauEquipier);
                    //    int ev = statsCalculator.CalculEVPourPV(this.Equipier.BasePV, this.Equipier.NiveauEquipier, pokemonRef.BasePV, this.Equipier.Iv);
                    //}
                    this.Equipier.BasePV = this.BasePV; 
                }
                if (this.BaseAttaque != 0)
                {
                    this.Equipier.BaseAttaque = this.BaseAttaque;
                }
                if (this.BaseDefense != 0)
                {
                    this.Equipier.BaseDefense = this.BaseDefense;
                }
                if (this.BaseAttSpe != 0)
                {
                    this.Equipier.BaseAttSpe = this.BaseAttSpe;
                }
                if (this.BaseDefSpe != 0)
                {
                    this.Equipier.BaseDefSpe = this.BaseDefSpe;
                }
                if (this.BaseVit != 0)
                {
                    this.Equipier.BaseVit = this.BaseVit;
                }
                if(this.Equipier.Nature!=null) 
                {
                    this.Equipier = statsCalculator.GetNatureStats(this.Equipier);
                }
             
                this.EquipierSelectionne = this.Equipier;
            }
        }

        public void MajStats()
        {
            this.BasePV = this.Equipier.BasePV;
            this.BasePVPourcentage = this.BasePV;
            if (BasePVPourcentage != 0)
            {
             
                
                this.MajPourcentageStat("PV", BasePVPourcentage);
               
            }

            this.BaseAttaque = this.Equipier.BaseAttaque;
            this.BaseAttaquePourcentage = this.BaseAttaque;

            if (BaseAttaquePourcentage != 0)
            {
                this.MajPourcentageStat("Attaque", BaseAttaquePourcentage);
            }

            this.BaseDefense = this.Equipier.BaseDefense;
            this.BaseDefensePourcentage = this.BaseDefense;

            if (BaseDefensePourcentage != 0)
            {
                this.MajPourcentageStat("Défense", BaseDefensePourcentage);
            }

            this.BaseAttSpe = this.Equipier.BaseAttSpe;
            this.BaseAttSpePourcentage = this.BaseAttSpe;

            if (BaseAttSpePourcentage != 0)
            {
                this.MajPourcentageStat("Att Spe", BaseAttSpePourcentage);
            }

            this.BaseDefSpe = this.Equipier.BaseDefSpe;
            this.BaseDefSpePourcentage = this.BaseDefSpe;

            if (BaseDefSpePourcentage != 0)
            {
                this.MajPourcentageStat("Def Spe", BaseDefSpePourcentage);
            }

            this.BaseVit = this.Equipier.BaseVit;
            this.BaseVitPourcentage = this.BaseVit;

            if (BaseVitPourcentage != 0)
            {
                this.MajPourcentageStat("Vitesse", BaseVitPourcentage);
            }
        }

        private void EnregistrerEquipier()
        {
            if(this.Equipier != null)
            {
               
           
                if (this.Equipier.equipierOrigine.IsSelected == true)
                {
                    MEquipier equipierEnModification = GetEquipierAModId(this.Equipier.equipierOrigine);
                    using (var repository = new RepEquipe())
                    {
                        repository.Update(this.Equipier, equipierEnModification);
                    }                                        
                    this.Close();
                }
                else
                {
                    if(this.Equipier.EquipeId==0)
                    {
                        this.Equipier.EquipeId = this.Equipier.equipierOrigine.EquipeId;
                    }
                    
                    this.Equipier.Ev = 510;
                    this.Equipier.Iv = 31;
                    using (var repository = new RepEquipe())
                    {
                        repository.Add(this.Equipier);
                    }
                  
                    this.Close();
                }
            }

        }

        private void SupprimerEquipier()
        {
            if (this.Equipier != null)
            {
                MEquipier equipierSupprimer = new MEquipier(this.Equipier.EquipeId, this.Equipier.TalentEquipier.IdTalent, this.Equipier.IdPokemon, this.Equipier.SetCapacites[0].IdCapacite, this.Equipier.SetCapacites[1].IdCapacite, this.Equipier.SetCapacites[2].IdCapacite, this.Equipier.SetCapacites[3].IdCapacite, this.Equipier.ObjetEquipier.IdObjet, this.Equipier.Nature.IdNature);
                //repEquipe = new RepEquipe();
                using (var repository = new RepEquipe())
                {
                    repository.Delete(equipierSupprimer);
                }
                
                this.Close();
            }
         
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
            //mainWindow = new MainWindow();
            //mainWindow.DataContext = new GestionEquipeVueModele();
            //windowManager.Register(mainWindow);
            //NavigationServices.NavigateToPage(new GestionEquipe());
            //windowManager.ShowWindow("Mainwindow", mainWindow);
            //var activeWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            //activeWindow?.Close();

            //windowManager = new WindowManager();
            //MainWindow mainWindow = new MainWindow();
            //windowManager.Register(mainWindow);
            //NavigationServices.NavigateToPage(new GestionEquipe());
            //windowManager.ShowWindow("MainWindow", mainWindow);

            //var gestionEquipePopup = new GestionEquipe();
            //gestionEquipePopup.DataContext = new GestionEquipeVueModele();

            NavigationServices.NavigateToPage(new GestionEquipe());
        }
       

    }
}
