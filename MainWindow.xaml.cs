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

namespace PokeStat
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccueilVueModel accueilVueModel;
        public MainWindow()
        {
            InitializeComponent();
            accueilVueModel = new AccueilVueModel();
            DataContext = accueilVueModel;

            SetWindowFullScreen();

            NavigationServices.Initialize(MainFrame);

        }


        public void SetWindowFullScreen()
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                parentWindow.WindowStyle = WindowStyle.None;
                parentWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
