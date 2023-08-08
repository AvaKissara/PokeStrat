﻿using PokeStat.Modeles;
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

namespace PokeStat.Vues.CrudUser
{
    /// <summary>
    /// Logique d'interaction pour ModifieUser.xaml
    /// </summary>
    public partial class ModifieUser : Page
    {
        private GestionUserVueModele gestionUserVueModele;
        public ModifieUser(MUser userAModfier)
        {
            InitializeComponent();
            gestionUserVueModele = new GestionUserVueModele();
            gestionUserVueModele.LigneSelection = userAModfier;
            DataContext = gestionUserVueModele;

            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
            NavigationServices.NavigateToPage(this);


        }
    }
}
