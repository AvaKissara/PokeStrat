using PokeStat.VuesModeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokeStat.Vues.User.GestionEquipe
{
    /// <summary>
    /// Logique d'interaction pour DetailGestionStat.xaml
    /// </summary>
    public partial class DetailGestionStat : Window
    {
        public DetailGestionStat(EquipierTreeViewNode equipier)
        {
            InitializeComponent();
            DataContext = equipier;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;

            if (DataContext is EquipierTreeViewNode equipier)
            {
                switch (slider.Name)
                {
                    case "pvValue":
                        equipier.BasePVPercentage = slider.Value;
                        double pourcentage = slider.Value;
                        double maxHP = equipier.pokemonRef.BasePV + 252;
                        double calculValue = (pourcentage / 100) * maxHP;
                        if (calculValue > maxHP)
                        {
                            calculValue = maxHP;
                        }
                        equipier.BasePV = (int)calculValue;
                        break;
                    case "attValue":
                        equipier.Equipier.BaseAttaque = (int)slider.Value;
                        break;
                    case "defValue":
                        equipier.Equipier.BaseDefense = (int)slider.Value;
                        break;
                    case "attSpeValue":
                        equipier.Equipier.BaseAttSpe = (int)slider.Value;
                        break;
                    case "defSpeValue":
                        equipier.Equipier.BaseDefSpe = (int)slider.Value;
                        break;
                    case "vitValue":
                        equipier.Equipier.BaseVit = (int)slider.Value;
                        break;
                    default:
                        break;
                }
      
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
