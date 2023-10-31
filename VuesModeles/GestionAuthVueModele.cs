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

     

        public MainWindow MainWindow { get; set; }
        public WindowManager windowManager;

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
            MUser userAConnecter = new MUser(0, Identifiant);
            List<MUser> users = repUser.GetAll();
            List<MAdmin> admins = repAdmin.GetAll();
            userAConnecter = repUser.GetUser(Identifiant);
            MAdmin adminAConnecter = admins.FirstOrDefault(a => a.MailPersonne == Identifiant);

            if (userAConnecter == null && adminAConnecter == null)
            {
                MessageBox.Show("L'email n'est pas reconnu");

            }
            else
            {
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
                    else if (connexionReussieUser)
                    {
                        personneConnectee = userAConnecter;
                        Role = UserRole.Utilisateur;
                    }
                    else
                    {
                        Role = UserRole.Profane;
                    }
                    SessionManager.Instance.Account = personneConnectee;

                    var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    activeWindow?.Close();
                    activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    activeWindow?.Close();
        
                    MainWindow mainWindow = new MainWindow();
                    using (var manager = new WindowManager())
                    {
                        manager.Register(mainWindow);
                        manager.ShowWindow("MainWindow", mainWindow);
                    }
                  
                }
                else
                {
                    MessageBox.Show("Raté");
                }
            }

          
        }

        private void AccueilPage()
        {
            
        }
    }
}
