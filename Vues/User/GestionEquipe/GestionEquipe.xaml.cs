using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.VuesModeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PokeStat.Vues.User.GestionEquipe
{
    /// <summary>
    /// Logique d'interaction pour GestionEquipe.xaml
    /// </summary>
    public partial class GestionEquipe : Page
    {
        private GestionEquipeVueModele gestionEquipeVueModele;
        public MainWindow MainWindow { get; set; }
        public GestionEquipe()
        {
            InitializeComponent();
            gestionEquipeVueModele = new GestionEquipeVueModele();
            DataContext = gestionEquipeVueModele;
            this.Focus();
            //Frame mainFrame = NavigationServices.GetMainFrame();
            //NavigationServices.Initialize(mainFrame);
            //NavigationServices.NavigateToPage(this);

        }


        private void Equipier_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var equipier = border.DataContext as EquipierTreeViewNode;
            

            if (equipier != null)
            {
            
                using (var repository = new RepEquipe())
                {
                    equipier.Equipier.equipierOrigine = repository.GetEquipier(equipier.Equipier);
                }
                            
                equipier.Equipier.equipierOrigine.IsSelected = true;

                equipier.EquipierSelectionne = equipier.Equipier;
    
            }
            e.Handled = true;
        }
     

        private void EquipierButton_Click(object sender, RoutedEventArgs e)
        {
            Button equipierButton = sender as Button;
            StackPanel parentStackPanel = equipierButton.Parent as StackPanel;
            EquipeTreeViewNode equipeNode = parentStackPanel.DataContext as EquipeTreeViewNode;

            if(equipeNode.Equipiers.Count()<6) 
            {
                MEquipier equipierOrigineDefaut = null;
                MEquipier equipierParDefaut =
                    new MEquipier(
                       IdPokemon: 0,
                       CheminImgPokemon: "0.png",
                       NomFraPokemon: "",
                       BasePV: 0,
                       BaseAttaque: 0,
                       BaseDefense: 0,
                       BaseAttSpe: 0,
                       BaseDefSpe: 0,
                       BaseVit: 0,
                       Legendaire: false,
                       Shiny: false,
                       Mega: false,
                       Giga: false,
                       Fab: false,
                       SurnomEquipier: "Nouveau",
                       NiveauEquipier: 0,
                       EsquiveEquipier: 0,
                       NiveauBonheur: 0,
                       Ev: 0,
                       Iv: 0,
                       Nature: null,
                       TalentEquipier: null,
                       ObjetEquipier: null,
                       SetCapacites: new ObservableCollection<MCapacite>(),
                       EquipeId: equipeNode.Equipe.IdEquipe,
                       equipierOrigineDefaut
                       ); 

                var equipierNode = new EquipierTreeViewNode(equipierParDefaut);
                //var activeWindow2 = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

                if (equipeNode != null)
                {
                    equipierNode.Equipier.equipierOrigine.IsSelected = false;

                    equipierNode.EquipierSelectionne = equipierNode.Equipier;
                }
            }
            else
            {
                MessageBox.Show("L'équipe est pleine");
            }
           
            // Empêcher la propagation de l'événement si nécessaire.
            e.Handled = true;
        }

        private void TreeView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;

            // Réglage de la position de la barre de défilement verticale
            if (e.Delta > 0)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 1);
            }
            else
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + 1);
            }

            e.Handled = true; // Empêche la propagation de l'événement
        }

    }
}
