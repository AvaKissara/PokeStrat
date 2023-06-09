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

namespace PokeStat.VuesModeles
{
    public class AccueilVueModel : INotifyPropertyChanged
    {
 
        public ICommand ConnexionCommand { get; set; }
        public ICommand InscriptionCommand { get; set; }
        public ICommand GestionPokemonCommand { get; set; }

        public AccueilVueModel()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            InscriptionCommand = new RelayCommand(Inscription);
            GestionPokemonCommand = new RelayCommand(GestionPokemon);
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

        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
