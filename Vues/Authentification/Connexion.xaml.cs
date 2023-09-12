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

namespace PokeStat.Vues.Authentification
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        private GestionAuthVueModele gestionAuthVueModele;

        private MainWindow MainWindow;
        public Connexion()
        {
            InitializeComponent();
            gestionAuthVueModele = new GestionAuthVueModele();
            DataContext = gestionAuthVueModele;
            MainWindow = new MainWindow();
            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
        }
    }
}
