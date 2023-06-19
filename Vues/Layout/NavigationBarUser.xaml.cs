﻿using PokeStat.VuesModeles;
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

namespace PokeStat.Vues.Layout
{
    /// <summary>
    /// Logique d'interaction pour NavigationBarUser.xaml
    /// </summary>
    public partial class NavigationBarUser : UserControl
    {
        private AccueilVueModel accueilVueModel;
        public NavigationBarUser()
        {
            InitializeComponent();
            accueilVueModel = new AccueilVueModel();
            DataContext = accueilVueModel;
        }
    }
}