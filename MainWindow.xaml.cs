﻿using PokeStat.Utilitaires;
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
        //private WindowManager windowManager = new WindowManager();
        public MainWindow()
        {
            InitializeComponent();
            accueilVueModel = new AccueilVueModel();
            DataContext = accueilVueModel;
            //windowManager.Register(this);
            SetWindowFullScreen();
            NavigationServices.Initialize(MainFrame);

        }

        public void UpdateNavigationBarVisibility()
        {
            // Obtenez le rôle actuel à partir du modèle de vue principal
            UserRole currentRole = SessionManager.Instance.Role;

            // Mettez à jour la visibilité des éléments de la barre de navigation en fonction du rôle actuel
            //NavigationBar.Visibility = currentRole == UserRole.Profane ? Visibility.Visible : Visibility.Collapsed;
            //NavigationBarUser.Visibility = currentRole == UserRole.Utilisateur ? Visibility.Visible : Visibility.Collapsed;
            //NavigationBarAdmin.Visibility = currentRole == UserRole.Administrateur ? Visibility.Visible : Visibility.Collapsed;
        }


        public void SetWindowFullScreen()
        {
            // Obtenez la fenêtre parente du UserControl
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                parentWindow.WindowStyle = WindowStyle.None;
                parentWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
