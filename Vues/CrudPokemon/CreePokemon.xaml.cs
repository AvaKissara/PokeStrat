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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokeStat.Vues.CrudPokemon
{
    /// <summary>
    /// Logique d'interaction pour CreePokemon.xaml
    /// </summary>
    public partial class CreePokemon : Page
    {
        public Frame MainFrame;
        public GestionPokemonVueModel gestionPokemonVueModel;
        public CreePokemon()
        {
            InitializeComponent();
            gestionPokemonVueModel = new GestionPokemonVueModel();
            DataContext = gestionPokemonVueModel;

            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
            NavigationServices.NavigateToPage(this);
        }
    }
}
