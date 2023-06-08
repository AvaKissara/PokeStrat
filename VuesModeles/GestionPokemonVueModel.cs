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

namespace PokeStat.VuesModeles
{
    public class GestionPokemonVueModel : INotifyPropertyChanged
    {
        private DataTable pokedex;

        public DataTable Pokedex
        {
            get { return pokedex; }
            set
            {
                if (pokedex != value)
                {
                    pokedex = value;
                    OnPropertyChanged(nameof(Pokedex));
                }
            }
        }

        public GestionPokemonVueModel()
        {
            // Initialisez les membres de votre vue modèle ici si nécessaire
            RepPokemon repPokemon = new RepPokemon();
            List<MPokemon> pokemons = repPokemon.GetPokemons();
            Pokedex = ConvertListToDataTable(pokemons);
        }

        // Convertir une liste de MPokemon en DataTable
        private DataTable ConvertListToDataTable(List<MPokemon> pokemons)
        {
            DataTable Pokedex = new DataTable();

            // Ajouter les colonnes à la DataTable
            Pokedex.Columns.Add("id", typeof(int));
            Pokedex.Columns.Add("Image", typeof(Bitmap));
            Pokedex.Columns.Add("Nom anglais", typeof(string));
            Pokedex.Columns.Add("Nom français", typeof(string));  
            Pokedex.Columns.Add("Numéro", typeof(string));
            Pokedex.Columns.Add("Taille", typeof(string));
            Pokedex.Columns.Add("Poids", typeof(double));
            Pokedex.Columns.Add("Expérience de base", typeof(int));
            Pokedex.Columns.Add("HP de base", typeof(int));
            Pokedex.Columns.Add("Attaque de base", typeof(int));
            Pokedex.Columns.Add("Défense de base", typeof(int));
            Pokedex.Columns.Add("Attaque spéciale de base", typeof(int));
            Pokedex.Columns.Add("Défense spéciale de base", typeof(int));
            Pokedex.Columns.Add("Vitesse de base", typeof(int));
            Pokedex.Columns.Add("Légendaire", typeof(bool));
            Pokedex.Columns.Add("Shiny", typeof(bool));
            Pokedex.Columns.Add("Nom de l'évolution", typeof(string));
            Pokedex.Columns.Add("Niveau d'évolution", typeof(int));
            Pokedex.Columns.Add("Nom de la version", typeof(string));
            Pokedex.Columns[1].ColumnName = "Pokemon";

            // Ajouter les données à la DataTable
            foreach (var pokemon in pokemons)
            {
                Bitmap imgPokemon = new Bitmap(@"Ressources\" + pokemon.cheminImgPokemon);
                DataRow row;
                row = Pokedex.NewRow();
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
                Pokedex.Rows.Add(row);
            }

            return Pokedex;
        }


        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
