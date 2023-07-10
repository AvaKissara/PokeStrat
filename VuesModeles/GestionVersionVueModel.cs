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
using System.Windows.Controls;
using PokeStat.Vues.CrudVersion;
using System.Data;
using PokeStat.Repositories;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PokeStat.Vues.CrudPokemon;

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
        private readonly RepGeneration repGen;

        //Propriété de type DataTable stockant des objets MVersion
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

        //Propriété de type booléen utilisée pour contrôler la saisie d'une version
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

        public int IdVers;

        private List<MGeneration> cmbGen;
        public List<MGeneration> CmbGen
        {
            get { return cmbGen; }
            set
            {
                if (cmbGen != value)
                {
                    cmbGen = value;
                    OnPropertyChanged(nameof(CmbGen));
                }
            }
        }
        private MGeneration gen;
        public MGeneration Gen
        {
            get { return gen; }
            set
            {
                if (gen != value)
                {
                    gen = value;
                    OnPropertyChanged(nameof(Gen));
                }
            }
        }

        private int IdGen;
        public int idGen 
        { get { return idGen; }
            set
            {
                if (idGen != value)
                {
                    idGen = value;
                    OnPropertyChanged(nameof(IdGen));
                }
            }
        }
        //public string nomGen { get; set; }  

        private List<DataRowView> dataRowViews;
        public List<DataRowView> DataRowViews
        {
            get { return dataRowViews; }
        }

        public DataRowView FindDataRowViewById(int id)
        {
            return dataRowViews.Find(rv => (int)rv["ID"] == id);
        }


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

            //dtData = new DataTable();
            dtData = DataTableTool.ConvertListToDataTable(versions);
            repGen = new RepGeneration();
            List<MGeneration> generations = repGen.GetAll();
            CmbGen = generations;
        }


        public void CreeVersion() 
        {
            NavigationServices.NavigateToPage(new CreerVersion());
        }
        
        public void AjouteVersion() 
        {
            MVersion nouvelleVersion = new MVersion(NomVers, Gen);

            //if (IsSaisieValide)
            //{
                List<MVersion> versions = repVersion.GetAll();
                List<MGeneration> generations = repGen.GetAll();

            // Vérifie si le nom du type existe déjà dans la liste des types
            bool versionExiste = versions.Any(v => v.nomVersion.Equals(nouvelleVersion.nomVersion, StringComparison.OrdinalIgnoreCase));

                if (versionExiste)
                {
                    MessageBox.Show("Cette version existe déjà !");
                }
                else
                {
                    foreach (MGeneration gen in generations)
                    {
                        if (gen.Equals(Gen))
                        {
                            // Ajout d'un nouveau type au repository
                            repVersion.Add(nouvelleVersion);
                        }
                    }          

                    // Actualisation de la liste des types
                    versions = repVersion.GetAll();
                    dtData = DataTableTool.ConvertListToDataTable(versions);

                    MessageBox.Show("La version a bien été ajouté !");
                    NavigationServices.NavigateToPage(new GestionVersion());
                }
            //}
            //else
            //{
            //    ErreurSaisie = "Veuillez corriger les erreurs de saisie.";
            //}
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
