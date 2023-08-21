using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.CrudPokemon;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Linq;
using PokeStat.Vues.CrudType;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using PokeStat.Properties;
using System.Xml.Linq;
using System.Windows.Markup;
using System.Globalization;

namespace PokeStat.VuesModeles
{
    public class GestionPokemonVueModel : IVueModele<MSpecimen, DataTable>
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand CreeCommand { get; set; }
        public ICommand AjouteCommand { get; set; }
        public ICommand ModifieCommand { get; set; }
        public ICommand MajCommand { get; set; }
        public ICommand EffaceCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand GestionCommand { get; set; }
        public ICommand CloseCommand { get; }
        public ICommand OpenFileCommand { get; }


        private readonly RepPokemon repPokemon;
        private readonly RepGeneration repGen;
        private readonly RepType repType;

        //Propriété de type MSpecimen corrrespondant à l'élément actuellement sélectionné dans la liste des types.
        private MSpecimen _ligneSelection;
        public MSpecimen LigneSelection
        {
            get { return _ligneSelection; }
            set
            {
                if (_ligneSelection != value)
                {
                    _ligneSelection = value;
                    OnPropertyChanged(nameof(LigneSelection));
                    // indique si un élément est sélectionné ou non
                    OnPropertyChanged(nameof(IsSelectionne));
                }
            }
        }
        public bool IsSelectionne => LigneSelection != null;


        private string selectedImagePath;
        public string SelectedImagePath
        {
            get { return selectedImagePath; }
            set
            {
                if (selectedImagePath != value)
                {
                    selectedImagePath = value;
                    OnPropertyChanged(nameof(SelectedImagePath));
                    OnPropertyChanged(nameof(RelativeSelectedImagePath));
                }
            }
        }


        private System.Drawing.Image imgPokemon;
        public System.Drawing.Image ImgPokemon
        {
            get { return imgPokemon; }
            set
            {
                if (imgPokemon != value)
                {
                    imgPokemon = value;
                    OnPropertyChanged(nameof(ImgPokemon));
                }
            }
        }

        public string RelativeSelectedImagePath
        {
            get
            {
                if (!string.IsNullOrEmpty(SelectedImagePath))
                {
                    string fileName = System.IO.Path.GetFileName(SelectedImagePath);
                    return fileName;
                }

                return string.Empty;
            }
        }

        private string relativeDtImagePath;
        public string RelativeDtImagePath
        {
            get { return relativeDtImagePath; }
            set
            {
                if (relativeDtImagePath != value)
                {
                    relativeDtImagePath = value;
                    OnPropertyChanged(nameof(RelativeDtImagePath));
                }
            }
        }


        string absoluDtImagePath;
        public string AbsoluDtImagePath
        {
            get { return absoluDtImagePath; }
            set
            {
                if (absoluDtImagePath != value)
                {
                    absoluDtImagePath = value;
                    OnPropertyChanged(nameof(AbsoluDtImagePath));
                }
            }
        }

        private DataTable dtData;

        public DataTable DtData
        {
            get { return dtData; }
            set
            {
                if (dtData != value)
                {
                    dtData = value;
                    OnPropertyChanged(nameof(DtData));
                }
            }
        }


        private List<MGeneration> cmbGen;
        public List<MGeneration> CmbGen
        {
            get { return cmbGen; }
            set
            {
                if (cmbGen != value)
                {
                    cmbGen = value;
                    OnPropertyChanged(nameof(CmbGen));
                }
            }
        }
       
        private List<MType> cmbType;
        public List<MType> CmbType
        {
            get { return cmbType; }
            set
            {
                if (cmbType != value)
                {
                    cmbType = value;
                    OnPropertyChanged(nameof(CmbType));
                }
            }
        }

        private List<MType> cmbType2;
        public List<MType> CmbType2
        {
            get { return cmbType2; }
            set
            {
                if (cmbType2 != value)
                {
                    cmbType2 = value;
                    OnPropertyChanged(nameof(CmbType2));
                }
            }
        }

        private MType _selectedCmbTypeValue;

        public MType SelectedCmbTypeValue
        {
            get { return _selectedCmbTypeValue; }
            set
            {
                if (_selectedCmbTypeValue != value)
                {
                    _selectedCmbTypeValue = value;
                    OnPropertyChanged(nameof(SelectedCmbTypeValue));
                }
            }
        }

        private List<MSpecimen> cmbEvo;
        public List<MSpecimen> CmbEvo
        {
            get { return cmbEvo; }
            set
            {
                if (cmbEvo != value)
                {
                    cmbEvo = value;
                    OnPropertyChanged(nameof(CmbEvo));
                }
            }
        }

        public int IdPok;

        private string cheminImgPokemon;
        public string CheminImgPokemon
        {
            get { return cheminImgPokemon; }
            set
            {
                if (cheminImgPokemon != value)
                {
                    cheminImgPokemon = value;
                    OnPropertyChanged(nameof(CheminImgPokemon));
                }
            }
        }

        private string nomFraPokemon;
        public string NomFraPokemon
        {
            get { return nomFraPokemon; }
            set
            {
                if (nomFraPokemon != value)
                {
                    nomFraPokemon = value;
                    OnPropertyChanged(nameof(NomFraPokemon));
                }
            }
        }

        private string nomEngPokemon;
        public string NomEngPokemon
        {
            get { return nomEngPokemon; }
            set
            {
                if (nomEngPokemon != value)
                {
                    nomEngPokemon = value;
                    OnPropertyChanged(nameof(NomEngPokemon));
                }
            }
        }

        private string numPokemon;
        public string NumPokemon
        {
            get { return numPokemon; }
            set
            {
                if (numPokemon != value)
                {
                    numPokemon = value;
                    OnPropertyChanged(nameof(NumPokemon));
                }
            }
        }

        private decimal taillePokemon;
        public decimal TaillePokemon
        {
            get { return taillePokemon; }
            set
            {
                if (taillePokemon != value)
                {
                    taillePokemon = value;
                    OnPropertyChanged(nameof(TaillePokemon));
                }
            }
        }

        private decimal poidsPokemon;
        public decimal PoidsPokemon
        {
            get { return poidsPokemon; }
            set
            {
                if (poidsPokemon != value)
                {
                    poidsPokemon = value;
                    OnPropertyChanged(nameof(PoidsPokemon));
                }
            }
        }

        private int niveau;
        public int Niveau
        {
            get { return niveau; }
            set
            {
                if (niveau != value)
                {
                    niveau = value;
                    OnPropertyChanged(nameof(Niveau));
                }
            }
        }

        private int pv;
        public int PV
        {
            get { return pv; }
            set
            {
                if (pv != value)
                {
                    pv = value;
                    OnPropertyChanged(nameof(PV));
                }
            }
        }

        private int attaque;
        public int Attaque
        {
            get { return attaque; }
            set
            {
                if (attaque != value)
                {
                    attaque = value;
                    OnPropertyChanged(nameof(Attaque));
                }
            }
        }

        private int defense;
        public int Defense
        {
            get { return defense; }
            set
            {
                if (defense != value)
                {
                    defense = value;
                    OnPropertyChanged(nameof(Defense));
                }
            }
        }

        private int attSpe;
        public int AttSpe
        {
            get { return attSpe; }
            set
            {
                if (attSpe != value)
                {
                    attSpe = value;
                    OnPropertyChanged(nameof(AttSpe));
                }
            }
        }

        private int defSpe;
        public int DefSpe
        {
            get { return defSpe; }
            set
            {
                if (defSpe != value)
                {
                    defSpe = value;
                    OnPropertyChanged(nameof(DefSpe));
                }
            }
        }

        private int vitesse;
        public int Vitesse
        {
            get { return vitesse; }
            set
            {
                if (vitesse != value)
                {
                    vitesse = value;
                    OnPropertyChanged(nameof(Vitesse));
                }
            }
        }

        private bool legendaire;
        public bool Legendaire
        {
            get { return legendaire; }
            set
            {
                if (legendaire != value)
                {
                    legendaire = value;
                    OnPropertyChanged(nameof(Legendaire));
                }
            }
        }

        private bool shiny;
        public bool Shiny
        {
            get { return shiny; }
            set
            {
                if (shiny != value)
                {
                    shiny = value;
                    OnPropertyChanged(nameof(Shiny));
                }
            }
        }

        private bool mega;
        public bool Mega    
        {
            get { return mega; }
            set
            {
                if (mega != value)
                {
                    mega = value;
                    OnPropertyChanged(nameof(Mega));
                }
            }
        }

        private bool giga;
        public bool Giga
        {
            get { return giga; }
            set
            {
                if (giga != value)
                {
                    giga = value;
                    OnPropertyChanged(nameof(Mega));
                }
            }
        }

        private bool fab;
        public bool Fab
        {
            get { return fab; }
            set
            {
                if (fab != value)
                {
                    fab = value;
                    OnPropertyChanged(nameof(Fab));
                }
            }
        }

        private MSpecimen evolution;
        public MSpecimen Evolution
        {
            get { return evolution; }
            set
            {
                if (evolution != value)
                {
                    evolution = value;
                    OnPropertyChanged(nameof(Evolution));
                }
            }
        }


        private MGeneration gen;
        public MGeneration Gen
        {
            get { return gen; }
            set
            {
                if (gen != value)
                {
                    gen = value;
                    OnPropertyChanged(nameof(Gen));
                }
            }
        }


        //Propriété de type booléen utilisée pour contrôler la saisie d'un type
        private bool isSaisieValide;
        public bool IsSaisieValide
        {
            get { return isSaisieValide; }
            set
            {
                isSaisieValide = value;
                OnPropertyChanged(nameof(IsSaisieValide));
            }
        }

        //Propriété de type string utilisée pour stocker un message d'erreur relatif à la saisie d'un type
        private string erreurSaisie;
        public string ErreurSaisie
        {
            get { return erreurSaisie; }
            set
            {
                erreurSaisie = value;
                OnPropertyChanged(nameof(ErreurSaisie));
            }
        }

        public GestionPokemonVueModel()
        {
            CreeCommand = new RelayCommand(CreePokemon);
            AjouteCommand = new RelayCommand(AjoutePokemon);
            ModifieCommand = new RelayCommand(ModifiePokemon);
            EffaceCommand = new RelayCommand(EffacePokemon);
            GestionCommand = new RelayCommand(GestionPokemon);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);
            OpenFileCommand = new RelayCommand(OpenFile);

            repPokemon = new RepPokemon();
            repGen = new RepGeneration();
            repType = new RepType();
            List<MSpecimen> pokemons = repPokemon.GetAll();
            DtData = ConvertListToDataTable(pokemons);
            repGen = new RepGeneration();
            repType = new RepType();
            List<MGeneration> generations = repGen.GetAll();
            CmbGen = generations;
            List<MType> types = repType.GetAll();
            CmbType = types;
            CmbType2 = types;
            CmbEvo = pokemons;
        }

        private void CreePokemon()
        {
            NavigationServices.NavigateToPage(new CreePokemon());
        }

        private void AjoutePokemon()
        {
            int idPok;
            int idType;
            CheminImgPokemon = RelativeSelectedImagePath;

            MSpecimen nouveauPokemon = new MSpecimen(IdPok,CheminImgPokemon, NomFraPokemon, NomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, PV, Attaque, Defense, AttSpe, DefSpe, Vitesse, Legendaire, Shiny, Mega, Giga, Fab, Evolution, Gen);
            List<MGeneration> generations = repGen.GetAll();
            List<MType> types = repType.GetAll();      
            List<MSpecimen> pokemons = repPokemon.GetAll();

            bool pokExiste = pokemons.Any(p => p.nomFraPokemon.Equals(nouveauPokemon.nomFraPokemon, StringComparison.OrdinalIgnoreCase));

            if (pokExiste)
            {
                System.Windows.MessageBox.Show("Ce pokémon existe déjà !");
            }
            else
            {
                
                foreach (MGeneration gen in generations)
                {
                    if (gen.Equals(Gen))
                    {
                        repPokemon.Add(nouveauPokemon);
                    }
                }
               
                // Actualisation de la liste des pokémons
                pokemons = repPokemon.GetAll();
                DtData = ConvertListToDataTable(pokemons);

                System.Windows.MessageBox.Show("Le pokémon a bien été ajouté !");
            }             

            foreach (MType type in types)
            {
                if (type.Equals(SelectedCmbTypeValue))
                {                   
                    idType = type.idType;
                    idPok = pokemons[pokemons.Count - 1].idPokemon;
                    repPokemon.AddTypePokemon(idType, idPok);                   
                }
            } 
            
            NavigationServices.NavigateToPage(new GestionPokemon());
        }

        private void ModifiePokemon()
        {

        }

        private void EffacePokemon()
        {

        }

        private void GestionPokemon() 
        {
            NavigationServices.NavigateToPage(new GestionPokemon());
        }

        private void AccueilPage()
        {
            // Vers la page d'accueil
            Page accueilPage = new AccueilPage();
            NavigationServices.NavigateToPage(accueilPage);
        }

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg; *.jpeg; *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                // Faites quelque chose avec le chemin d'accès au fichier sélectionné, par exemple, affectez-le à une propriété de votre ViewModel
                SelectedImagePath = selectedImagePath;
            }
        }

        // Convertir une liste de MSpecimen en DataTable
        private DataTable ConvertListToDataTable(List<MSpecimen> pokemons)
        {
            DataTable dtData = new DataTable();


            // Ajouter les colonnes à la DataTable
            dtData.Columns.Add("id", typeof(int));
            dtData.Columns.Add("Image", typeof(string));
            dtData.Columns.Add("Nom", typeof(string));
            dtData.Columns.Add("Name", typeof(string));
            dtData.Columns.Add("Num", typeof(string));
            dtData.Columns.Add("Taille", typeof(string));
            dtData.Columns.Add("Poids", typeof(double));
            dtData.Columns.Add("HP", typeof(int));
            dtData.Columns.Add("Attaque", typeof(int));
            dtData.Columns.Add("Défense", typeof(int));
            dtData.Columns.Add("Att. spé", typeof(int));
            dtData.Columns.Add("Déf. spé", typeof(int));
            dtData.Columns.Add("Vitesse", typeof(int));
            dtData.Columns.Add("Légendaire", typeof(bool));
            dtData.Columns.Add("Shiny", typeof(bool));
            dtData.Columns.Add("Mega", typeof (bool));
            dtData.Columns.Add("Giga", typeof(bool));
            dtData.Columns.Add("Fab", typeof(bool));
            dtData.Columns.Add("Evolution", typeof(string));
            dtData.Columns.Add("Gen", typeof(string));
            dtData.Columns[1].ColumnName = "Pokemon";

            // Ajouter les données à la DataTable
            foreach (var pokemon in pokemons)
            {

                //string absoluteImagePath = GetFullPath(rela);
               //absoluDtImagePath = Environment.CurrentDirectory;
                RelativeDtImagePath = pokemon.cheminImgPokemon;
                //Environment.CurrentDirectory = "C:/Users/Lila%20Badi/Documents/GitHub/AvaKissara/PokeStat/Ressources/";
                string basePath = @"C:/Users/AvaKissara/source/repos/PokeStat/Ressources/";
                //string fullPath = System.IO.Path.GetFullPath("C:\Users/Lila%20Badi/Documents/GitHub/AvaKissara/PokeStat/Ressources/" + relativeDtImagePath);
                AbsoluDtImagePath = System.IO.Path.Combine(basePath, pokemon.cheminImgPokemon);
                
             


                // Créez un objet BitmapImage à partir du chemin relatif
                System.Drawing.Image ImgPokemon = System.Drawing.Image.FromFile(AbsoluDtImagePath);
                

                DataRow row;
                row = dtData.NewRow();
                row[0] = pokemon.idPokemon;
                row[1] = AbsoluDtImagePath;
                row[2] = pokemon.nomFraPokemon;
                row[3] = pokemon.nomEngPokemon;
                row[4] = pokemon.numPokemon;
                row[5] = pokemon.taillePokemon;
                row[6] = pokemon.poidsPokemon;
                row[7] = pokemon?.basePv;
                row[8] = pokemon?.baseAttaque;
                row[9] = pokemon?.baseDefense;
                row[10] = pokemon?.baseAttSpe;
                row[11] = pokemon?.baseDefSpe;
                row[12] = pokemon?.baseVit;
                row[13] = pokemon.legendaire;
                row[14] = pokemon.shiny;
                row[15] = pokemon.mega;
                row[16] = pokemon.giga;
                row[17] = pokemon.fab;
                row[18] = pokemon.evolution?.nomFraPokemon;
                row[19] = pokemon.gen.nomGen;
                dtData.Rows.Add(row);
            }

            return dtData;
        }

  

        /// <summary>
        /// Contrôle de la saisie
        /// </summary>
        private void ValidateNomPok()
        {
            if (string.IsNullOrEmpty(NomFraPokemon))
            {
                ErreurSaisie = "Le champ Nom est requis.";
                IsSaisieValide = false;
            }
            else if (!NomFraPokemon.All(char.IsLetter))
            {
                ErreurSaisie = "Le champ Nom ne peut contenir que des lettres.";
                IsSaisieValide = false;
            }
            else
            {
                ErreurSaisie = string.Empty;
                IsSaisieValide = true;
            }
        }

        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
