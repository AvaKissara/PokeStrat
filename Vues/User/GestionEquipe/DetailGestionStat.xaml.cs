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
                        equipier.BasePVPourcentage = slider.Value;
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
                        equipier.BaseAttaquePourcentage = slider.Value;
                        double pourcentageAtt = slider.Value;
                        double maxAtt = equipier.pokemonRef.BaseAttaque + 252;
                        double calculValueAtt = (pourcentageAtt / 100) * maxAtt;
                        if (calculValueAtt > maxAtt)
                        {
                            calculValueAtt = maxAtt;
                        }
                        equipier.BaseAttaque = (int)calculValueAtt;
                        break;

                    case "defValue":
                        equipier.BaseDefensePourcentage = slider.Value;
                        double pourcentageDef = slider.Value;
                        double maxDef = equipier.pokemonRef.BaseDefense + 252;
                        double calculValueDef = (pourcentageDef / 100) * maxDef;
                        if (calculValueDef > maxDef)
                        {
                            calculValueDef = maxDef;
                        }
                        equipier.BaseDefense = (int)calculValueDef;
                        break;

                    case "attSpeValue":
                        equipier.BaseAttSpePourcentage = slider.Value;
                        double pourcentageAttSpe = slider.Value;
                        double maxAttSpe = equipier.pokemonRef.BaseAttSpe + 252;
                        double calculValueAttSpe = (pourcentageAttSpe / 100) * maxAttSpe;
                        if (calculValueAttSpe > maxAttSpe)
                        {
                            calculValueAttSpe = maxAttSpe;
                        }
                        equipier.BaseAttSpe = (int)calculValueAttSpe;
                        break;

                    case "defSpeValue":
                        equipier.BaseDefSpePourcentage = slider.Value;
                        double pourcentageDefSpe = slider.Value;
                        double maxDefSpe = equipier.pokemonRef.BaseDefSpe + 252;
                        double calculValueDefSpe = (pourcentageDefSpe / 100) * maxDefSpe;
                        if (calculValueDefSpe > maxDefSpe)
                        {
                            calculValueDefSpe = maxDefSpe;
                        }
                        equipier.BaseDefSpe = (int)calculValueDefSpe;
                        break;

                    case "vitValue":
                        equipier.BaseVitPourcentage = slider.Value;
                        double pourcentageVit = slider.Value;
                        double maxVit = equipier.pokemonRef.BaseVit + 252;
                        double calculValueVit = (pourcentageVit / 100) * maxVit;
                        if (calculValueVit > maxVit)
                        {
                            calculValueVit = maxVit;
                        }
                        equipier.BaseVit = (int)calculValueVit;
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
