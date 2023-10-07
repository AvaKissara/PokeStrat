using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace PokeStat.Utilitaires
{
    public static class NavigationServices
    {
        private static Frame mainFrame;
        private static SessionManager _sessionManager;
      
        public static void Initialize(Frame Mainframe)
        {
            mainFrame = Mainframe;
        }

        public static Frame GetMainFrame()
        {
            return mainFrame;
        }

        public static void NavigateToPage(Page page)
        {
            _sessionManager = new SessionManager();
            string fullClassName = page.GetType().FullName;
            int lastDotIndex = fullClassName.LastIndexOf('.');
            string className = fullClassName.Substring(lastDotIndex + 1);
            _sessionManager.SetPreviousPage(className);
            mainFrame.Navigate(page);
        }

    }
}
