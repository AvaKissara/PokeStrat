using PokeStat.Modeles;
using PokeStat.Repositories;
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
using System.Windows.Forms;
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
        public WindowManager windowManager;

        public DetailEquipe()
        {
            InitializeComponent();
            MainWindow = new MainWindow();
            windowManager = new WindowManager();
            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);

            double detailGestionCapaciteHeight = 620; 

            double newTop = Screen.PrimaryScreen.WorkingArea.Height - detailGestionCapaciteHeight + 470; 

            this.Height = newTop;

            this.Top = newTop;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           EquipierTreeViewNode equipier = (DataContext as EquipierTreeViewNode);

            if (equipier != null)
            {
                equipier.EquipierSaisiEquipeId = equipier.Equipier.EquipeId;
                equipier.EquipierAModId = equipier.Equipier.equipierOrigine;
                DetailGestionPokemon detailsPokemon = new DetailGestionPokemon(equipier);

                double detailEquipeLeft = Left;
                double detailEquipeTop = Top;

                detailsPokemon.Left = detailEquipeLeft - detailsPokemon.Width;
                detailsPokemon.Top = detailEquipeTop;

                windowManager.ShowWindow("DetailEquipe", detailsPokemon);
                //detailsPokemon.ShowDialog();
            }
        }

        private void Equipier_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var equipier = border.DataContext as EquipierTreeViewNode;

            if (equipier != null)
            {
                equipier.EquipierSelectionne = equipier.Equipier;
       
            }

            e.Handled = true;
        }

        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {         
            EquipierTreeViewNode equipier = (DataContext as EquipierTreeViewNode);
            var detailGestionCapacite = new DetailGestionCapacite(equipier);
            detailGestionCapacite.Owner = this;
            equipier.EquipierAModId = equipier.Equipier.equipierOrigine;
          

            double detailCapaciteLeft = Left;
            double detailCapaciteTop = Top;
            double detailGestionCapaciteHeight = 620;
            detailGestionCapacite.Left = detailCapaciteLeft -166;
            detailGestionCapacite.Top = detailCapaciteTop + detailGestionCapaciteHeight + 46;
            windowManager.ShowWindow("DetailEquipe", detailGestionCapacite);
            //detailGestionCapacite.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var activeWindow2 = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
    }
}
