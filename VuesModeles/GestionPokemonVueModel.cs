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


namespace PokeStat.VuesModeles
{
    public class GestionPokemonVueModel : INotifyPropertyChanged
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand CreePokemonCommand { get; set; }
        public ICommand AjoutePokemonCommand { get; set; }
        public ICommand ModifiePokemonCommand { get; set; }
        public ICommand EffacePokemonCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }

        private DataTable dtPokedex;

        public DataTable DtPokedex
        {
            get { return dtPokedex; }
            set
            {
                if (dtPokedex != value)
                {
                    dtPokedex = value;
                    OnPropertyChanged(nameof(DtPokedex));
                }
            }
        }

      

        RepPokemon repPokemon;

        RepVersion repVersion;

        RepType repType;


        private List<MVersion> cmbVersion;
        public List<MVersion> CmbVersion
        {
            get { return cmbVersion; }
            set
            {
                if (cmbVersion != value)
                {
                    cmbVersion = value;
                    OnPropertyChanged(nameof(CmbVersion));
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


        public GestionPokemonVueModel()
        {
            CreePokemonCommand = new RelayCommand(CreePokemon);
            AjoutePokemonCommand = new RelayCommand(AjoutePokemon);
            ModifiePokemonCommand = new RelayCommand(ModifiePokemon);
            EffacePokemonCommand = new RelayCommand(EffacePokemon);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);

            repPokemon = new RepPokemon();
            repVersion = new RepVersion();
            repType = new RepType();
            List<MPokemon> pokemons = repPokemon.GetPokemons();
            DtPokedex = ConvertListToDataTable(pokemons);
            repVersion = new RepVersion();
            repType = new RepType();
            List<MVersion> versions = repVersion.GetVersions();
            CmbVersion = versions;
            List<MType> types = repType.GetTypes();
            CmbType = types;
        }

        private void CreePokemon()
        {
            NavigationServices.NavigateToPage(new CreePokemon());
        }

        private void AjoutePokemon()
        {
            NavigationServices.NavigateToPage(new CreePokemon());
        }

        private void ModifiePokemon()
        {

        }

        private void EffacePokemon()
        {

        }


        //private ComboBox ConvertListMVersionToCombobox(List<MVersion> versions)
        //{
        //    ComboBox cmbVersion = new ComboBox();
        //    foreach(MVersion version in versions)
        //    {
        //        cmbVersion.Items.Add(version);
        //    }

        //    return cmbVersion;
        //}
        //private ComboBox ConvertListMTypeToCombobox(List<MType> types)
        //{
        //    ComboBox cmbType = new ComboBox();
        //    foreach (MType type in types)
        //    {
        //        cmbType.Items.Add(type.nomType);
        //    }

        //    return cmbType;
        //}


        // Convertir une liste de MPokemon en DataTable
        private DataTable ConvertListToDataTable(List<MPokemon> pokemons)
        {
            DataTable dtPokedex = new DataTable();

            // Ajouter les colonnes à la DataTable
            dtPokedex.Columns.Add("id", typeof(int));
            dtPokedex.Columns.Add("Image", typeof(Bitmap));
            dtPokedex.Columns.Add("Name", typeof(string));
            dtPokedex.Columns.Add("Nom", typeof(string));
            dtPokedex.Columns.Add("Num", typeof(string));
            dtPokedex.Columns.Add("Taille", typeof(string));
            dtPokedex.Columns.Add("Poids", typeof(double));
            dtPokedex.Columns.Add("Xp", typeof(int));
            dtPokedex.Columns.Add("HP", typeof(int));
            dtPokedex.Columns.Add("Attaque", typeof(int));
            dtPokedex.Columns.Add("Défense", typeof(int));
            dtPokedex.Columns.Add("Attaque spé", typeof(int));
            dtPokedex.Columns.Add("Défense spé", typeof(int));
            dtPokedex.Columns.Add("Vitesse", typeof(int));
            dtPokedex.Columns.Add("Légendaire", typeof(bool));
            dtPokedex.Columns.Add("Shiny", typeof(bool));
            dtPokedex.Columns.Add("Evolution", typeof(string));
            dtPokedex.Columns.Add("Niveau évo", typeof(int));
            dtPokedex.Columns.Add("Version", typeof(string));
            dtPokedex.Columns[1].ColumnName = "Pokemon";

            // Ajouter les données à la DataTable
            foreach (var pokemon in pokemons)
            {
                Bitmap imgPokemon = new Bitmap(@"Ressources\" + pokemon.cheminImgPokemon);
                DataRow row;
                row = dtPokedex.NewRow();
                row[0] = pokemon.idPokemon;
                row[1] = imgPokemon;
                row[2] = pokemon.nomEngPokemon;
                row[3] = pokemon.nomFraPokemon;
                row[4] = pokemon.numPokemon;
                row[5] = pokemon.taillePokemon;
                row[6] = pokemon.poidsPokemon;
                row[7] = pokemon.baseXp;
                row[8] = pokemon.pv;
                row[9] = pokemon.attaque;
                row[10] = pokemon.defense;
                row[11] = pokemon.attSpe;
                row[12] = pokemon.defSpe;
                row[13] = pokemon.vitesse;
                row[14] = pokemon.legendaire;
                row[15] = pokemon.shiny;
                row[16] = pokemon.evolution?.nomFraPokemon;
                row[17] = pokemon?.nivEvolution;
                row[18] = pokemon.gen.nomVersion;
                dtPokedex.Rows.Add(row);
            }

            return dtPokedex;
        }
        private void AccueilPage()
        {
            // Vers la page d'accueil
            Page accueilPage = new AccueilPage();
            NavigationServices.NavigateToPage(accueilPage);
        }

        private void Close()
        {
            Application.Current.Shutdown();
        }


        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
