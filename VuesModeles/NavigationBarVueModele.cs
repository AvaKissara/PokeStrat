using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{/// <summary>
/// / gère la vue modèle associée à la barre de navigation pour les utilisateurs non-connecté.
/// </summary>
    public class NavigationBarVueModele : BaseVueModele
    {
        //Propriétés ICommand pour gérer les différentes commandes de la barre de navigation
        public ICommand ConnexionPopupCommand { get; set; }
        public ICommand InscriptionPopupCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }

        // Gestionnaire de session utilisé pour vérifier l'état de la session utilisateur.
        private readonly SessionManager _sessionManager;

        // Propriétés booléennes pour gérer la visibilité des autres navbar
        public bool IsLoggedIn => _sessionManager.IsLoggedIn;

        private bool _isUser;
        public bool IsUser
        {
            get { return _isUser; }
            set
            {
                if (_isUser != value)
                {
                    _isUser = value;
                    OnPropertyChanged(nameof(IsUser));
                }
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        // Constructeur de la classe qui initialise les propriétés et le gestionnaire de session.
        public NavigationBarVueModele(ICommand connexionPopupCommand, ICommand inscriptionPopupCommand, ICommand accueilPageCommand, ICommand closeCommand, SessionManager sessionManager)
        {
            this.ConnexionPopupCommand = connexionPopupCommand;
            this.InscriptionPopupCommand = inscriptionPopupCommand;
            this.AccueilPageCommand = accueilPageCommand;
            this.CloseCommand = closeCommand;
            this._sessionManager = sessionManager;
        }

        // Méthode appelée en cas de modification du compte utilisateur.
        // Elle ajuste les propriétés IsUser et IsAdmin en fonction du type de compte utilisateur.
        private void OnAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            if (_sessionManager.Account is MUser)
            {
                this.IsUser = true;
                this.IsAdmin = false;
            }
            else if (_sessionManager.Account is MAdmin)
            {
                this.IsUser = false;
                this.IsAdmin = true;
            }
            else
            {
                IsUser = false;
                IsAdmin = false;
            }
        }

        // Méthode de nettoyage pour se désabonner de l'événement AccountChanged du gestionnaire de session.
        public override void Dispose()
        {
            _sessionManager.AccountChanged -= OnAccountChanged;
            base.Dispose();
        }
    }
}
