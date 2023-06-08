using PokeStrat.VuesModeles;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PokeStrat.Vues.CrudPokemon
{
    /// <summary>
    /// Logique d'interaction pour GestionPokemon.xaml
    /// </summary>
    public partial class GestionPokemon : Page
    {
        public GestionPokemon()
        {
            DataContext = new GestionPokemonVueModel();
            InitializeComponent();
            PokeStrat.Utilitaires.NavigationService.Initialize(Application.Current.MainWindow as NavigationWindow);
        }
       
    }
}
