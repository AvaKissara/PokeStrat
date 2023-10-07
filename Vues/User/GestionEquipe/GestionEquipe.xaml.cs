using PokeStat.Modeles;
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
        public GestionEquipe()
        {
            InitializeComponent();
            gestionEquipeVueModele = new GestionEquipeVueModele();
            DataContext = gestionEquipeVueModele;

            Frame mainFrame = NavigationServices.GetMainFrame();
            NavigationServices.Initialize(mainFrame);
            NavigationServices.NavigateToPage(this);
        }
        //private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    if (DataContext is GestionEquipeVueModele viewModel)
        //    {
        //        viewModel.SelectedEquipeNode = e.NewValue as EquipeTreeViewNode;
        //    }
        //}

        private void Equipier_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Obtenir l'équipier correspondant aux données de la vue
            var border = sender as Border;
            var equipier = border.DataContext as EquipierTreeViewNode;

            if (equipier != null)
            {
      
                equipier.EquipierSeletionne = equipier.Equipier;
            }

            // Empêcher la propagation de l'événement pour éviter la sélection de l'élément parent de la TreeView.
            e.Handled = true;
        }
        public MainWindow MainWindow { get; set; }
        private WindowManager windowManager = new WindowManager();
        private void EquipierButton_Click(object sender, RoutedEventArgs e)
        {
            MEquipier equipierParDefaut = new MEquipier(
                   IdPokemon: 0,
                   CheminImgPokemon: "0.png",
                   NomFraPokemon: "Nouveau",
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
                   EquipeId: 0
                   );

            var equipeNode = new EquipierTreeViewNode(equipierParDefaut);
            //var activeWindow2 = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

       


            if (equipeNode != null)
            {
                // Appelez une méthode ou effectuez une action avec equipierViewModel
                // par exemple, lancez une action pour sélectionner cet équipier
                equipeNode.EquipierSeletionne = equipeNode.Equipier;
            }

            // Empêcher la propagation de l'événement si nécessaire.
            e.Handled = true;
        }


    }
}
