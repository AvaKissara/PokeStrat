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
    public class GestionAuthVueModele : BaseVueModele
    {
        public ICommand ConnexionCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }

        private readonly RepUser repUser;

        private readonly RepAdmin repAdmin;

        private AccueilVueModel accueilVueModel;



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

        public MainWindow MainWindow { get; private set; }

        public GestionAuthVueModele()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            AccueilPageCommand = new RelayCommand(AccueilPage);

            repUser = new RepUser();
            repAdmin = new RepAdmin();
            accueilVueModel = new AccueilVueModel();
        }

        public void Connexion()
        {
            List<MUser> users = repUser.GetAll();
            List<MAdmin> admins = repAdmin.GetAll();
            MUser userAConnecter = users.FirstOrDefault(u => u.MailPersonne == Identifiant);
            MAdmin adminAConnecter = admins.FirstOrDefault(a => a.MailPersonne == Identifiant);

            if (userAConnecter != null && adminAConnecter != null)
            {
                MessageBox.Show("Erreur : Plusieurs utilisateurs avec le même email.");
                return;
            }

            SecureString selHasheUser = userAConnecter != null ? repUser.GetSalt(userAConnecter.IdPersonne) : null;
            SecureString selHasheAdmin = adminAConnecter != null ? repAdmin.GetSalt(adminAConnecter.IdPersonne) : null;

            bool connexionReussieUser = userAConnecter != null && PasswordManager.VerifyPassword(Mdp, userAConnecter.MdpPersonne, selHasheUser);
            bool connexionReussieAdmin = adminAConnecter != null && PasswordManager.VerifyPassword(Mdp, adminAConnecter.MdpPersonne, selHasheAdmin);

            if (connexionReussieUser || connexionReussieAdmin)
            {
                MPersonne personneConnectee = null;

                // Défini le rôle de l'utilisateur
                if (connexionReussieAdmin)
                {
                    personneConnectee = adminAConnecter;
                    Role = UserRole.Administrateur;

                }
                else if(connexionReussieUser)
                {
                    personneConnectee = userAConnecter;
                    Role = UserRole.Utilisateur;
                }
                else
                {
                    Role = UserRole.Profane;
                }
                SessionManager.Instance.Account = personneConnectee;

                MessageBox.Show("Youpii! " + personneConnectee.NomPersonne);
                MainWindow = new MainWindow();

                var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

                activeWindow?.Close();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show(); 
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Raté");
            }
        }

        private void AccueilPage()
        {
            
        }
    }
}
