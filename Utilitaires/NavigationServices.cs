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
            mainFrame.Navigate(page);
        }
    }
}
