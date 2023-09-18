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

namespace PokeStat.Vues.User.GestionEquipe
{
    /// <summary>
    /// Logique d'interaction pour GestionEquipe.xaml
    /// </summary>
    public partial class GestionEquipe : Page
    {
        private GestionEquipeVueModele gestionEquipeVueModele;
        public GestionEquipe()
        {
            InitializeComponent();
            gestionEquipeVueModele = new GestionEquipeVueModele();
            DataContext =gestionEquipeVueModele;

            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
            NavigationServices.NavigateToPage(this);
        }
    }
}
