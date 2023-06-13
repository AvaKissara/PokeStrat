using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using PokeStat.Utilitaires;
using PokeStat.Vues.CrudPokemon;
using PokeStat.Vues.CrudType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace PokeStat.VuesModeles
{
    public class AccueilVueModel : INotifyPropertyChanged
    {
 
        public ICommand ConnexionCommand { get; set; }
        public ICommand InscriptionCommand { get; set; }
        public ICommand GestionPokemonCommand { get; set; }
        public ICommand GestionTypeCommand { get; set; }
        public ICommand CloseCommand { get; }

        public AccueilVueModel()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            InscriptionCommand = new RelayCommand(Inscription);
            GestionPokemonCommand = new RelayCommand(GestionPokemon);
            GestionTypeCommand = new RelayCommand(GestionType);
            CloseCommand = new RelayCommand(Close);
        }

        private void Connexion()
        {
            
        }

        private void Inscription()
        {
            
        }

        private void GestionPokemon() 
        {
            NavigationServices.NavigateToPage(new GestionPokemon());
         
            MessageBox.Show("Le bouton 'Gestion Pokemon' a été cliqué !");
        }

        private void GestionType()
        {
            NavigationServices.NavigateToPage(new GestionType());
        }

        private void Close()
        {
            Application.Current.Shutdown();
        }

        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
