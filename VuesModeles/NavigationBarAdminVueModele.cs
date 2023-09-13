using PokeStat.Modeles;
using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class NavigationBarAdminVueModele : BaseVueModele
    {
        public ICommand GestionPokemonCommand { get; set; }
        public ICommand GestionTypeCommand { get; set; }
        public ICommand GestionVersionCommand { get; set; }
        public ICommand GestionGenerationCommand { get; set; }
        public ICommand GestionUserCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }

        private readonly SessionManager _sessionManager;
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
        public NavigationBarAdminVueModele(ICommand gestionPokemonCommand, ICommand gestionTypeCommand, ICommand gestionVersionCommand, ICommand gestionGenerationCommand, ICommand gestionUserCommand, ICommand accueilPageCommand, ICommand closeCommand, SessionManager sessionManager)
        {
            GestionPokemonCommand = gestionPokemonCommand;
            GestionTypeCommand = gestionTypeCommand;
            GestionVersionCommand = gestionVersionCommand;
            GestionGenerationCommand = gestionGenerationCommand;
            GestionUserCommand = gestionUserCommand;
            AccueilPageCommand = accueilPageCommand;
            CloseCommand = closeCommand;
            _sessionManager = sessionManager;
        }

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

        public override void Dispose()
        {
            _sessionManager.AccountChanged -= OnAccountChanged;
            base.Dispose();
        }
    }
}
