using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class NavigationBarVueModele : BaseVueModele
    {
        public ICommand ConnexionPopupCommand { get; set; }
        public ICommand InscriptionPopupCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }

        private readonly SessionManager _sessionManager; 
        public bool IsLoggedIn => _sessionManager.IsLoggedIn;

        public NavigationBarVueModele(ICommand connexionPopupCommand, ICommand inscriptionPopupCommand, ICommand accueilPageCommand, ICommand closeCommand, SessionManager sessionManager)
        {
            ConnexionPopupCommand = connexionPopupCommand;
            InscriptionPopupCommand = inscriptionPopupCommand;
            AccueilPageCommand = accueilPageCommand;
            CloseCommand = closeCommand;
            _sessionManager = sessionManager;
        }

        private void OnAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _sessionManager.AccountChanged -= OnAccountChanged;

            base.Dispose();
        }
    }
}
