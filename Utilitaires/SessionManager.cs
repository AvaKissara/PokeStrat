using PokeStat.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Utilitaires
{
    public class SessionManager
    {
        private MPersonne _account;

        public int UserId { get; set; }
        public UserRole Role { get; set; }

        private static SessionManager _instance;

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionManager();
                }
                return _instance;
            }
        }
        public MPersonne Account
        {
            get => _account;
            set
            {
                _account = value;
                AccountChanged?.Invoke();

                if (_account is MUser)
                {
                    Role = UserRole.Utilisateur;
                }
                else if (_account is MAdmin)
                {
                    Role = UserRole.Administrateur;
                }
                else
                {
                    Role = UserRole.Profane;
                }
            }
        }

        public bool IsLoggedIn => Account != null;

        public event Action AccountChanged;

        public void ClearUser()
        {
            Account = null;
            Role = UserRole.Profane; 
        }
    }

    public enum UserRole
    {
        Profane,    // L'utilisateur non connecté
        Utilisateur, // L'utilisateur connecté
        Administrateur // L'administrateur connecté
    }
}