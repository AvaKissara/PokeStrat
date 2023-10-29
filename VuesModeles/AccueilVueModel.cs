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
using PokeStat.Vues.CrudAdmin;

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
        public ICommand GestionAdminCommand { get; set; }
        public ICommand GestionEquipeCommand { get; set; }
        public ICommand GestionProfileCommand { get; set; }
        public ICommand GestionMatchCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }

        public BaseVueModele NavigationBarViewModel
        {
            get
            {
                // Sélection d'un modèle de barre de navigation en fonction du rôle de l'utilisateur
                if (SessionManager.Instance.Role == UserRole.Utilisateur)
                {
                    return new NavigationBarUserVueModele(GestionEquipeCommand, GestionProfileCommand, GestionMatchCommand, AccueilPageCommand, CloseCommand, SessionManager.Instance);
                }
                else if (SessionManager.Instance.Role == UserRole.Administrateur)
                {
                    return new NavigationBarAdminVueModele(GestionPokemonCommand, GestionTypeCommand, GestionVersionCommand, GestionGenerationCommand, GestionUserCommand, GestionAdminCommand, AccueilPageCommand, CloseCommand, SessionManager.Instance);
                }
                else
                {
                    return new NavigationBarVueModele(ConnexionPopupCommand, InscriptionPopupCommand, AccueilPageCommand, CloseCommand, SessionManager.Instance);
                }
            }
        }

        public AccueilVueModel()
        {
            // Initialisation des commandes lors de la création de l'objet AccueilVueModel
            ConnexionPopupCommand = new RelayCommand(ConnexionPopup);
            InscriptionPopupCommand = new RelayCommand(InscriptionPopup);
            GestionPokemonCommand = new RelayCommand(GestionPokemon);
            GestionTypeCommand = new RelayCommand(GestionType);
            GestionVersionCommand = new RelayCommand(GestionVersion);
            GestionGenerationCommand = new RelayCommand(GestionGeneration);
            GestionUserCommand = new RelayCommand(GestionUser);
            GestionAdminCommand = new RelayCommand(GestionAdmin);
            GestionEquipeCommand = new RelayCommand(GestionEquipe);
            GestionProfileCommand = new RelayCommand(GestionProfile);
            GestionMatchCommand = new RelayCommand(GestionMatch);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);
        }

        // Méthodes pour gérer les différentes actions de l'interface

        public void GestionProfile()
        {
            // Code à ajouter pour gérer la page de gestion de profil
        }

        public void GestionMatch()
        {
            // Code à ajouter pour gérer la page de gestion de matchs
        }

        public void GestionEquipe()
        {
            // Naviguer vers la page de gestion d'équipe
            NavigationServices.NavigateToPage(new GestionEquipe());
        }

        public WindowManager windowManager;

        private void ConnexionPopup()
        {
            // Ouvrir une fenêtre de connexion pop-up
            windowManager = new WindowManager();
            var connexionPopup = new Connexion();
            connexionPopup.DataContext = new GestionAuthVueModele();
            windowManager.Register(connexionPopup);
            windowManager.ShowWindow("ConnexionPopup", connexionPopup);
        }

        private void InscriptionPopup()
        {
            // Naviguer vers la page de création d'utilisateur
            NavigationServices.NavigateToPage(new CreeUser());
        }

        private void GestionPokemon()
        {
            // Naviguer vers la page de gestion de Pokémon
            NavigationServices.NavigateToPage(new GestionPokemon());
        }

        private void GestionType()
        {
            // Naviguer vers la page de gestion de types
            NavigationServices.NavigateToPage(new GestionType());
        }

        public void GestionGeneration()
        {
            // Naviguer vers la page de gestion de générations
            NavigationServices.NavigateToPage(new GestionGen());
        }

        public void GestionUser()
        {
            // Naviguer vers la page de gestion d'utilisateurs
            NavigationServices.NavigateToPage(new GestionUser());
        }

        public void GestionAdmin()
        {
            // Ouvrir une fenêtre pop-up pour la création d'administrateur
            windowManager = new WindowManager();
            var creeAdminPopup = new CreerAdmin();
            creeAdminPopup.DataContext = new GestionAdminVueModele();
            windowManager.Register(creeAdminPopup);
            windowManager.ShowWindow("CreerAdminPopup", creeAdminPopup);
        }

        private void AccueilPage()
        {
            // Naviguer vers la page d'accueil
            NavigationServices.NavigateToPage(new AccueilPage());
        }

        public void GestionVersion()
        {
            // Naviguer vers la page de gestion des versions
            NavigationServices.NavigateToPage(new GestionVersion());
        }

        private void Close()
        {
            // Fermer l'application
            Application.Current.Shutdown();
        }
    }

}
