using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Utilitaires
{
    public class SessionManager
    {
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

        public int UserId { get; set; }
        public bool IsLoggedIn => UserId != 0;
        public UserRole Role { get; set; }

        public void SetUser(int userId, UserRole role)
        {
            UserId = userId;
            Role = role;
        }

        public void ClearUser()
        {
            UserId = 0;
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