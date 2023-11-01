using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.VuesModeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokeStat.Vues.User.GestionEquipe
{
    /// <summary>
    /// Logique d'interaction pour DetailGestionPokemon.xaml
    /// </summary>
    public partial class DetailGestionPokemon : Window
    {
        private bool isComboBoxLoaded = false;

        public DetailGestionPokemon(EquipierTreeViewNode equipier)
        {
            InitializeComponent();
            DataContext = equipier;
        }

        /// <summary>
        /// Méthode pour filtrer les capacités via la barre recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EquipierTreeViewNode equipier = (DataContext as EquipierTreeViewNode);

            if (equipier != null)
            {
                string searchText = searchBox.Text;

                if (searchText.Length >= 1)
                {
                    List<MSpecimen> items = equipier.Equipier.pokemons;
                    var filteredItems = BarreRechercheCapacite(searchText, items);
                    comboBox.ItemsSource = filteredItems;
                }
                else
                {
                    comboBox.ItemsSource = null;
                }
            }
        }


        public List<MSpecimen> BarreRechercheCapacite(string searchText, List<MSpecimen> items)
        {
            var filteredItems = items.Where(pokemon => pokemon.NomFraPokemon.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredItems;
        }


        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (!isComboBoxLoaded)
            {
                isComboBoxLoaded = true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
