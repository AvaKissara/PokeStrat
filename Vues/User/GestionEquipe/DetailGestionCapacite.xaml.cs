using PokeStat.Modeles;
using PokeStat.Repositories;
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
    /// Logique d'interaction pour DetailGestionCapacite.xaml
    /// </summary>
    public partial class DetailGestionCapacite : Window
    {
   
        public DetailGestionCapacite(EquipierTreeViewNode equipier)
        {
            InitializeComponent();
            DataContext = equipier;
        }

        private int currentPage = 1;
        private int pageSize = 50;

        private void comboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (comboBox.Items.Count < pageSize * currentPage)
                {
                    LoadNextPage();
                }
            }
        }

        private void LoadNextPage()
        {
            currentPage++;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EquipierTreeViewNode equipier = (DataContext as EquipierTreeViewNode);

            if (equipier != null)
            {
                string searchText = searchBox.Text;

                if (searchText.Length >= 1)
                {
                    List<MCapacite> items = equipier.Equipier.PoolCapacites;
                    var filteredItems = BarreRechercheCapacite(searchText, items); 
                    comboBox.ItemsSource = filteredItems;
                }
                else
                {
                    comboBox.ItemsSource = null;
                }
            }
        }


        public List<MCapacite> BarreRechercheCapacite(string searchText, List<MCapacite> items)
        {
            var filteredItems = items.Where(capacite => capacite.NomCapacite.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredItems;
        }


        private bool isComboBoxLoaded = false;

        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (!isComboBoxLoaded)
            {
                isComboBoxLoaded = true;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
