using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues.CrudPokemon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace PokeStat.VuesModeles
{
    public class GestionPokemonVueModel : INotifyPropertyChanged
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand AjoutePokemonCommand { get; set; }
        public ICommand ModifiePokemonCommand { get; set; }
        public ICommand EffacePokemonCommand { get; set; }
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

        public GestionPokemonVueModel()
        {
            AjoutePokemonCommand = new RelayCommand(AjoutePokemon);
            ModifiePokemonCommand = new RelayCommand(ModifiePokemon);
            EffacePokemonCommand = new RelayCommand(EffacePokemon);
            CloseCommand = new RelayCommand(Close);


            RepPokemon repPokemon = new RepPokemon();
            List<MPokemon> pokemons = repPokemon.GetPokemons();
            DtPokedex = ConvertListToDataTable(pokemons);
        }

        private void AjoutePokemon()
        {
            NavigationServices.NavigateToPage(new AjoutePokemon());
        }

        private void ModifiePokemon()
        {

        }

        private void EffacePokemon()
        {

        }


        // Convertir une liste de MPokemon en DataTable
        private DataTable ConvertListToDataTable(List<MPokemon> pokemons)
        {
            DataTable dtPokedex = new DataTable();

            // Ajouter les colonnes à la DataTable
            dtPokedex.Columns.Add("id", typeof(int));
            dtPokedex.Columns.Add("Image", typeof(Bitmap));
            dtPokedex.Columns.Add("Nom anglais", typeof(string));
            dtPokedex.Columns.Add("Nom français", typeof(string));  
            dtPokedex.Columns.Add("Numéro", typeof(string));
            dtPokedex.Columns.Add("Taille", typeof(string));
            dtPokedex.Columns.Add("Poids", typeof(double));
            dtPokedex.Columns.Add("Expérience de base", typeof(int));
            dtPokedex.Columns.Add("HP de base", typeof(int));
            dtPokedex.Columns.Add("Attaque de base", typeof(int));
            dtPokedex.Columns.Add("Défense de base", typeof(int));
            dtPokedex.Columns.Add("Attaque spéciale de base", typeof(int));
            dtPokedex.Columns.Add("Défense spéciale de base", typeof(int));
            dtPokedex.Columns.Add("Vitesse de base", typeof(int));
            dtPokedex.Columns.Add("Légendaire", typeof(bool));
            dtPokedex.Columns.Add("Shiny", typeof(bool));
            dtPokedex.Columns.Add("Nom de l'évolution", typeof(string));
            dtPokedex.Columns.Add("Niveau d'évolution", typeof(int));
            dtPokedex.Columns.Add("Nom de la version", typeof(string));
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
