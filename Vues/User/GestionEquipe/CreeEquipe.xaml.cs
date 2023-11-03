﻿using PokeStat.Modeles;
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

namespace PokeStat.Vues.User.GestionEquipe
{
    /// <summary>
    /// Logique d'interaction pour CreeEquipe.xaml
    /// </summary>
    public partial class CreeEquipe : Window
    {
       
        public CreeEquipe(EquipeTreeViewNode equipe)
        {
            DataContext = equipe;
            InitializeComponent();
        }
        private void btnAnnul_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
