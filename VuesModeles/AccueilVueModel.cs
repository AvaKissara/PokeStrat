using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using PokeStrat.Utilitaires;

namespace PokeStrat.VuesModeles
{
    public class AccueilVueModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConnexionCommand { get; private set; }
        public ICommand InscriptionCommand { get; private set; }
        public ICommand GestionPokemonCommand { get; private set; }

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
            Utilitaires.NavigationService.Navigate(new Uri("Vues/CrudPokemon/GestionPokemon.xaml", UriKind.Relative));
            MessageBox.Show("Le bouton 'Gestion Pokemon' a été cliqué !");
        }
    }
}
