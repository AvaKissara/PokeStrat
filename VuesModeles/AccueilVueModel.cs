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
using PokeStat.Vues.User.GestionEquipe;

namespace PokeStat.VuesModeles
{
    public class AccueilVueModel : BaseVueModele
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand ConnexionPopupCommand { get; set; }
        public ICommand InscriptionPopupCommand { get; set; }
        public ICommand GestionPokemonCommand { get; set; }
        public ICommand GestionTypeCommand { get; set; }
        public ICommand GestionVersionCommand { get; set; }
        public ICommand GestionGenerationCommand { get; set; }
        public ICommand GestionUserCommand { get; set; }
        public ICommand GestionEquipeCommand { get; set; }
        public ICommand GestionProfileCommand { get; set; }
        public ICommand GestionMatchCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }



        private BaseVueModele _navigationBarViewModel;
        public BaseVueModele NavigationBarViewModel
        {
            get
            {
                // En fonction du rôle actuel (par exemple, stocké dans SessionManager),
                // retournez le modèle de navigation bar approprié
                if (SessionManager.Instance.Role == UserRole.Utilisateur)
                {
                    // Utilisateur connecté, retournez le modèle de navigation bar utilisateur
                    return new NavigationBarUserVueModele(ConnexionPopupCommand, InscriptionPopupCommand, AccueilPageCommand, CloseCommand, GestionEquipeCommand, GestionProfileCommand, GestionMatchCommand, SessionManager.Instance);
                }
                else if (SessionManager.Instance.Role == UserRole.Administrateur)
                {
                    // Administrateur connecté, retournez le modèle de navigation bar administrateur
                    return new NavigationBarAdminVueModele(GestionPokemonCommand, GestionTypeCommand, GestionVersionCommand, GestionGenerationCommand, GestionUserCommand, AccueilPageCommand, CloseCommand, SessionManager.Instance);
                }
                else
                {
                    // Utilisateur non connecté ou rôle non défini, retournez le modèle de navigation bar par défaut
                    return new NavigationBarVueModele(ConnexionPopupCommand, InscriptionPopupCommand, AccueilPageCommand, CloseCommand, SessionManager.Instance);
                }
            }
        }

        private UserRole role;
        public UserRole Role
        {
            get { return role; }
            set
            {
                if (role != value)
                {
                    role = value;
                    OnPropertyChanged(nameof(Role));
                }
            }
        }

        public AccueilVueModel()
        {
            ConnexionPopupCommand = new RelayCommand(ConnexionPopup);
            InscriptionPopupCommand = new RelayCommand(InscriptionPopup);
            GestionPokemonCommand = new RelayCommand(GestionPokemon);
            GestionTypeCommand = new RelayCommand(GestionType);
            GestionVersionCommand = new RelayCommand(GestionVersion);
            GestionGenerationCommand = new RelayCommand(GestionGeneration);
            GestionUserCommand = new RelayCommand(GestionUser);
            GestionEquipeCommand = new RelayCommand(GestionEquipe);
            GestionProfileCommand = new RelayCommand(GestionProfile);
            GestionMatchCommand = new RelayCommand(GestionMatch);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);
        }


        public void GestionProfile()
        {

        }

        public void GestionMatch()
        {

        }

        public void GestionEquipe()
        {
            NavigationServices.NavigateToPage(new GestionEquipe());
        }

        private void ConnexionPopup()
        {
            var connexionPopup = new Connexion();
            connexionPopup.DataContext = new GestionAuthVueModele();
            connexionPopup.ShowDialog();
        }


        private void InscriptionPopup()
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
    }
}
