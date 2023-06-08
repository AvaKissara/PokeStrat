using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PokeStrat.Utilitaires
{
    public static class NavigationService
    {
        private static NavigationWindow mainWindow;

        public static void Initialize(NavigationWindow window)
        {
            mainWindow = window;
        }

        public static void Navigate(Uri uri)
        {
            mainWindow?.Navigate(uri);
        }
    }
}
