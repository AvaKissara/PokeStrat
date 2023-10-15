using PokeStat.Utilitaires;
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

namespace PokeStat.Vues.CrudAdmin
{
    /// <summary>
    /// Logique d'interaction pour CreerAdmin.xaml
    /// </summary>
    public partial class CreerAdmin : Window
    {
        private MainWindow MainWindow;
        public CreerAdmin()
        {
            InitializeComponent();
            MainWindow = new MainWindow();
            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
        }
    }
}
