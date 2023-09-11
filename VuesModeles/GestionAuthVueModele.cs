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
using System.Windows;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class GestionAuthVueModele : INotifyPropertyChanged
    {
        public ICommand ConnexionCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }

        private readonly RepUser repUser;

        private readonly RepAdmin repAdmin;

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

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                if (isAdmin != value)
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                    OnPropertyChanged(nameof(IsUser));
                }
            }
        }

        public bool IsUser => !IsAdmin;

  

        public event Action ConnexionReussie;

        public GestionAuthVueModele()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            AccueilPageCommand = new RelayCommand(AccueilPage);

            repUser = new RepUser();
            repAdmin = new RepAdmin();
        }

        public void Connexion()
        {
            List<MUser> users = repUser.GetAll();
            List<MAdmin> admins = repAdmin.GetAll();
            MUser userAConnecter = users.FirstOrDefault(u => u.mailPersonne == Identifiant);
            MAdmin adminAConnecter = admins.FirstOrDefault(a => a.mailPersonne == Identifiant);

            if (userAConnecter != null && adminAConnecter != null)
            {
                MessageBox.Show("Erreur : Plusieurs utilisateurs avec le même email.");
                return;
            }

            SecureString selHasheUser = userAConnecter != null ? repUser.GetSalt(userAConnecter.idPersonne) : null;
            SecureString selHasheAdmin = adminAConnecter != null ? repAdmin.GetSalt(adminAConnecter.idPersonne) : null;

            // Vérifier si le mot de passe saisi correspond au hachage stocké
            bool connexionReussieUser = userAConnecter != null && PasswordManager.VerifyPassword(Mdp, userAConnecter.mdpPersonne, selHasheUser);
            bool connexionReussieAdmin = adminAConnecter != null && PasswordManager.VerifyPassword(Mdp, adminAConnecter.mdpPersonne, selHasheAdmin);

            if (connexionReussieUser || connexionReussieAdmin)
            {
                // Définir le rôle de l'utilisateur
                if (connexionReussieAdmin)
                {
                    IsAdmin = true;    
                }
                else
                {
                    IsAdmin = false;
                }

                ConnexionReussie?.Invoke();
                // Définir l'ID de l'utilisateur dans la session
                SessionManager.Instance.SetUser(userAConnecter?.idPersonne ?? adminAConnecter?.idPersonne ?? 0);

                MessageBox.Show("Youpii! " + SessionManager.Instance.UserId);

                var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

                if (activeWindow != null)
                {
                    activeWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Raté");
            }
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
