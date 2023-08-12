using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.CrudGeneration;
using PokeStat.Vues.CrudPokemon;
using PokeStat.Vues.CrudType;
using PokeStat.Vues.CrudVersion;
using PokeStat.Vues.CrudUser;
using PokeStat.Vues.Authentification;

namespace PokeStat.VuesModeles
{
    public class AccueilVueModel : INotifyPropertyChanged
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand ConnexionCommand { get; set; }
        public ICommand InscriptionCommand { get; set; }
        public ICommand GestionPokemonCommand { get; set; }
        public ICommand GestionTypeCommand { get; set; }
        public ICommand GestionVersionCommand { get; set; }
        public ICommand GestionGenerationCommand { get; set; }
        public ICommand GestionUserCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }
        public ICommand ClosePopupCommand { get; }



        private static bool isUserLoggedIn;
        public static bool IsUserLoggedIn
        {
            get { return isUserLoggedIn; }
            set
            {
                isUserLoggedIn = value;
                OnPropertyChanged(nameof(IsUserLoggedIn));
            }
        }

        public AccueilVueModel()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            InscriptionCommand = new RelayCommand(Inscription);
            GestionPokemonCommand = new RelayCommand(GestionPokemon);
            GestionTypeCommand = new RelayCommand(GestionType);
            GestionVersionCommand = new RelayCommand(GestionVersion);
            GestionGenerationCommand = new RelayCommand(GestionGeneration);
            GestionUserCommand = new RelayCommand(GestionUser);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);
            ClosePopupCommand = new RelayCommand(ClosePopup);
        }

        private void Connexion()
        {
            var connexionPopup = new Connexion();
            connexionPopup.DataContext = new GestionAuthVueModele();
            connexionPopup.ShowDialog();
        }

        static public void ClosePopup()
        {
            Application.Current.MainWindow.Close();
        }

        private void Inscription()
        {
            NavigationServices.NavigateToPage(new CreeUser());
        }

        private void GestionPokemon() 
        {
            NavigationServices.NavigateToPage(new GestionPokemon());
        }

        private void GestionType()
        {
            NavigationServices.NavigateToPage(new GestionType());
        }


        public void GestionGeneration()
        {
            NavigationServices.NavigateToPage(new GestionGen());
        }

        public void GestionUser()
        {
            NavigationServices.NavigateToPage(new GestionUser());
        }

        private void AccueilPage()
        {
            // Vers la page d'accueil
            Page accueilPage = new AccueilPage();
            NavigationServices.NavigateToPage(accueilPage);
        }

        public void GestionVersion()
        {
            NavigationServices.NavigateToPage(new GestionVersion());
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
