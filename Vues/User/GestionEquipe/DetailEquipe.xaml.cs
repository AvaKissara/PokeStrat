using PokeStat.Modeles;
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
using System.Windows.Shapes;

namespace PokeStat.Vues.User.GestionEquipe
{
    /// <summary>
    /// Logique d'interaction pour DetailEquipe.xaml
    /// </summary>
    public partial class DetailEquipe : Window
    {
        private MainWindow MainWindow;

        public DetailEquipe()
        {
            InitializeComponent();
            MainWindow = new MainWindow();
   
            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Obtenez l'équipier correspondant au DataContext de l'image
            MEquipier equipier = (DataContext as EquipierTreeViewNode)?.Equipier;

            if (equipier != null)
            {
                DetailGestionPokemon detailsPokemon = new DetailGestionPokemon(equipier);

                double detailEquipeLeft = Left;
                double detailEquipeTop = Top;

                // Positionnez la nouvelle fenêtre à gauche de DetailEquipe
                detailsPokemon.Left = detailEquipeLeft - detailsPokemon.Width;
                detailsPokemon.Top = detailEquipeTop;

                detailsPokemon.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
