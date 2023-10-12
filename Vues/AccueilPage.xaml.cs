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

namespace PokeStat.Vues
{
    /// <summary>
    /// Logique d'interaction pour AccueilPage.xaml
    /// </summary>
    public partial class AccueilPage : Page
    {
        private AccueilVueModel accueilVueModel;
        private readonly Frame MainFrame;
        public AccueilPage()
        {
            MainFrame = NavigationServices.GetMainFrame();

            InitializeComponent();
            accueilVueModel = new AccueilVueModel();
            DataContext = accueilVueModel;

        }

    }
}
