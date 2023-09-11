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
        private GestionAuthVueModele gestionAuthVueModele;

        public MainWindow()
        {
            InitializeComponent();
            gestionAuthVueModele = new GestionAuthVueModele();
            DataContext = gestionAuthVueModele;
            SetWindowFullScreen();
            NavigationServices.Initialize(MainFrame);
          
        }

        //private void UpdateNavigationBarStyles()
        //{
        //    if (DataContext is GestionAuthVueModele gestionAuthVueModele)
        //    {
        //        if (gestionAuthVueModele.IsAdmin)
        //        {
        //            NavigationBarUser.Style = (Style)FindResource("NavigationBarUserHide");
        //            NavigationBarAdmin.Style = (Style)FindResource("NavigationBarAdminShow");
        //            NavigationBar.Style = (Style)FindResource("NavigationBarHide");
        //        }
        //        else if (gestionAuthVueModele.IsUser)
        //        {
        //            NavigationBarUser.Style = (Style)FindResource("NavigationBarUserShow");
        //            NavigationBarAdmin.Style = (Style)FindResource("NavigationBarAdminHide");
        //            NavigationBar.Style = (Style)FindResource("NavigationBarHide");
        //        }
        //        else
        //        {
        //            NavigationBarUser.Style = (Style)FindResource("NavigationBarHide");
        //            NavigationBarAdmin.Style = (Style)FindResource("NavigationBarHide");
        //            NavigationBar.Style = (Style)FindResource("NavigationBarShow");
        //        }
        //    }
        //}


        private void SetWindowFullScreen()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }
    }
}
