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

        public int UserId { get; private set; }
        public bool IsLoggedIn => UserId != 0;

        public void SetUser(int userId)
        {
            UserId = userId;
        }

        public void ClearUser()
        {
            UserId = 0;
        }
    }
}
