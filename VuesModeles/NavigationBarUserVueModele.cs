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
    public class NavigationBarUserVueModele : BaseVueModele
    {
        public ICommand GestionEquipeCommand { get; set; }
        public ICommand GestionProfileCommand { get; set; }
        public ICommand GestionMatchCommand { get; set; }
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

        public NavigationBarUserVueModele(ICommand gestionEquipeCommand, ICommand gestionProfileCommand, ICommand gestionMatchCommand, ICommand accueilPageCommand, ICommand closeCommand, SessionManager sessionManager)
        {
        
            this.GestionEquipeCommand = gestionEquipeCommand;
            this.GestionMatchCommand = gestionMatchCommand;
            this.GestionProfileCommand = gestionProfileCommand;
            this.AccueilPageCommand = accueilPageCommand;
            this.CloseCommand = closeCommand;
            this._sessionManager = sessionManager;

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
