using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PokeStat.Utilitaires
{
    public class WindowManager
    {
        public static List<Window> openWindows = new List<Window>();

        public async Task Register(Window window)
        {
            // Vérifiez si une fenêtre du même type existe déjà
            var existingWindow = openWindows.FirstOrDefault(w => w.GetType() == window.GetType());

            if (existingWindow != null)
            {
                await Task.Delay(500);
                // Fermez la fenêtre existante
                existingWindow.Close();
                openWindows.Remove(existingWindow);
            }

            // Ajoutez la nouvelle fenêtre à la liste
            openWindows.Add(window);

            window.Closed += (sender, e) =>
            {
                openWindows.Remove(window);
            };
        }


        public static void Unregister(Window window)
        {
            openWindows.Remove(window);
        }

        public static void CloseAll()
        {
            foreach (var window in openWindows.ToList())
            {
                window.Close();
            }
        }

        public void ShowWindow<T>(string key, T window) where T : Window
        {
            if (!openWindows.Contains(window))
            {
                openWindows.Add(window);

                window.Closed += (sender, e) =>
                {
                    openWindows.Remove(window);
                };
            }
            // Créez une animation de fondu
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3) // Réglez la durée de l'animation selon vos préférences
            };

            // Appliquez l'animation à l'opacité de la fenêtre
            window.BeginAnimation(UIElement.OpacityProperty, animation);

            window.Show();
            window.Activate();
        }

        public void CloseWindow(Window window)
        {
            //var window = openWindows.FirstOrDefault(w => w.Name == key);
            if (window != null)
            {
                openWindows.Remove(window);
                window.Close();
            }
        }
    }

}
