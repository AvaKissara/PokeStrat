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

namespace PokeStat.Vues.CrudVersion
{
    /// <summary>
    /// Logique d'interaction pour ModifieVersion.xaml
    /// </summary>
    public partial class ModifieVersion : Page
    {
        public Frame MainFrame;
        private GestionVersionVueModel gestionVersionVueModel;
        public ModifieVersion()
        {
            InitializeComponent();
            gestionVersionVueModel = new GestionVersionVueModel();
            DataContext = gestionVersionVueModel;

            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
            NavigationServices.NavigateToPage(this);
        }
    }
}
