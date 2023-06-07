using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PokeStrat.Utilitaires;

namespace PokeStrat.VuesModeles
{
    public class AccueilVueModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConnexionCommand { get; private set; }
        public ICommand InscriptionCommand { get; private set; }

        public AccueilVueModel()
        {
            ConnexionCommand = new RelayCommand(Connexion);
            InscriptionCommand = new RelayCommand(Inscription);
        }

        private void Connexion()
        {
            // Logique de traitement lors du clic sur le bouton de connexion
        }

        private void Inscription()
        {
            // Logique de traitement lors du clic sur le bouton d'inscription
        }
    }
}
