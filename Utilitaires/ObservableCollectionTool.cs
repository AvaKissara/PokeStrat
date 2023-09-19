using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace PokeStat.Utilitaires
{
    public static class ObservableCollectionTool
    {
        public static void BindToListBox<T>(ObservableCollection<T> collection, ListBox listBox)
        {
            if (collection == null || listBox == null)
                return;

            listBox.ItemsSource = collection;
            listBox.ItemTemplate = GenerateDataTemplate<T>();
        }

        private static DataTemplate GenerateDataTemplate<T>()
        {
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            dataTemplate.VisualTree = stackPanelFactory;

            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead)
                {
                    FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                    textBlockFactory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding(property.Name));
                    stackPanelFactory.AppendChild(textBlockFactory);
                }
            }

            return dataTemplate;
        }
    }
}
