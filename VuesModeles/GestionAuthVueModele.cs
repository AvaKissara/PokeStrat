using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.Authentification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class GestionAuthVueModele : INotifyPropertyChanged
    {
        public ICommand ConnexionCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }

        private readonly RepUser repUser;

        private string identifiant;
        public string Identifiant
        {
            get { return identifiant; }
            set 
            { 
                identifiant = value; 
                OnPropertyChanged(nameof(Identifiant));    
            }
        }

        private SecureString mdp = new SecureString();
        public SecureString Mdp
        {
            get { return mdp; }
            set
            {
                mdp = value;
                OnPropertyChanged(nameof(Mdp));
            }
        }

        public GestionAuthVueModele()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            AccueilPageCommand = new RelayCommand(AccueilPage);
        }
 
        public void Connexion()
        {
            List<MUser> users = repUser.GetAll();
            MUser userAConnecter = users.FirstOrDefault(u => u.mailPersonne == Identifiant);

            SecureString selHashe = repUser.GetSalt(userAConnecter.idPersonne);

            // Vérifier si le mot de passe saisi correspond au hachage stocké
            bool connexionReussie = PasswordManager.VerifyPassword(Mdp, userAConnecter.mdpPersonne, selHashe);
        }

        private void AccueilPage()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
