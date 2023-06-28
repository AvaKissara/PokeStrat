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
using PokeStat.Vues.CrudVersion;
using System.Data;
using PokeStat.Repositories;

namespace PokeStat.VuesModeles
{
    public class GestionVersionVueModel : IVueModele<MVersion, DataTable>
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


        //Propriété de type MVersion corrrespondant à l'élément actuellement sélectionné dans la liste des types.
        private MVersion _ligneSelection;
        public MVersion LigneSelection
        {
            get { return _ligneSelection; }
            set
            {
                if (_ligneSelection != value)
                {
                    _ligneSelection = value;
                    OnPropertyChanged(nameof(LigneSelection));
                    // indique si un élément est sélectionné ou non
                    OnPropertyChanged(nameof(IsSelectionne));
                }
            }
        }
        public bool IsSelectionne => LigneSelection != null;

        private readonly RepVersion repVersion;

        //Propriété de type DataTable stockant des objets MType
        private DataTable dtData;

        public DataTable DtData
        {
            get { return dtData; }
            set
            {
                if (dtData != value)
                {
                    dtData = value;
                    OnPropertyChanged(nameof(DtData));
                }
            }
        }

        //Propriété de type booléen utilisée pour contrôler la saisie d'un type
        private bool isSaisieValide;
        public bool IsSaisieValide
        {
            get { return isSaisieValide; }
            set
            {
                isSaisieValide = value;
                OnPropertyChanged(nameof(IsSaisieValide));
            }
        }

        //Propriété de type string utilisée pour stocker un message d'erreur relatif à la saisie d'un type
        private string erreurSaisie;
        public string ErreurSaisie
        {
            get { return erreurSaisie; }
            set
            {
                erreurSaisie = value;
                OnPropertyChanged(nameof(ErreurSaisie));
            }
        }

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

        private int idGen;
        public int IdGen
        {
            get { return idGen; }
            set
            {
                if (idGen != value)
                {
                    idGen = value;
                    OnPropertyChanged(nameof(IdGen));
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

            repVersion = new RepVersion();
            List<MVersion> versions = repVersion.GetAll();
            Dictionary<string, string> columnMappings = new Dictionary<string, string>()
            {
                { "ID", "idVersion" },
                { "NOM", "nomVersion" },
                { "GENERATION", "genId" }
            };
            dtData = DataTableTool.ConvertListToDataTable(versions, columnMappings);
        }


        public void CreeVersion() 
        {
            NavigationServices.NavigateToPage(new CreerVersion());
        }
        
        public void AjouteVersion() 
        {
            NavigationServices.NavigateToPage(new GestionVersion());
        }

        public void ModifieVersion() { }

        public void MajVersion() { }

        public void EffaceVersion() 
        { 

        }

        public void GestionVersion() 
        {
            NavigationServices.NavigateToPage(new GestionVersion());
        }

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
