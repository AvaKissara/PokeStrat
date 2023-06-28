using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.Vues.CrudType;
using PokeStat.Vues;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Controls;

namespace PokeStat.VuesModeles
{
    public class GestionVersionVueModel : IVueModele
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand CreeCommand { get; set; }
        public ICommand AjouteCommand { get; set; }
        public ICommand ModifieCommand { get; set; }
        public ICommand MajCommand { get; set; }
        public ICommand EffaceCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand GestionCommand { get; set; }
        public ICommand CloseCommand { get; }
        public ICommand OpenFileCommand { get; }

        private string nomVers;
        public string NomVers
        {
            get { return nomVers; }
            set
            {
                if (nomVers != value)
                {
                    nomVers = value;
                    OnPropertyChanged(nameof(NomVers));
                }
            }
        }

        public int IdVers;

        public GestionVersionVueModel()
        {
            // Initialisation des commandes
            CreeCommand = new RelayCommand(CreeVersion);
            AjouteCommand = new RelayCommand(AjouteVersion);
            ModifieCommand = new RelayCommand(ModifieVersion);
            MajCommand = new RelayCommand(MajVersion);
            EffaceCommand = new RelayCommand(EffaceVersion);
            GestionCommand = new RelayCommand(GestionVersion);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);

        }


        public void CreeVersion() { }
        
        public void AjouteVersion() { }

        public void ModifieVersion() { }

        public void MajVersion() { }

        public void EffaceVersion() { }

        public void GestionVersion() { }

        private void AccueilPage()
        {
            // Vers la page d'accueil
            Page accueilPage = new AccueilPage();
            NavigationServices.NavigateToPage(accueilPage);
        }
        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
