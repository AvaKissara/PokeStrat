using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues.CrudVersion;
using PokeStat.Vues;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Controls;
using System.Windows.Forms;
using PokeStat.Vues.CrudGeneration;

namespace PokeStat.VuesModeles
{
    public class GestionGenVueModele : BaseVueModele, IVueModele<MGeneration, DataTable>
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

        //Propriété de type MGeneration corrrespondant à l'élément actuellement sélectionné dans la liste des types.
        private MGeneration _ligneSelection;
        public MGeneration LigneSelection
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

        private readonly RepGeneration repGen;

 


        public int idGen;

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

        public string nomGen
        {
            get { return nomGen; }
            set
            {
                if (nomGen != value)
                {
                    nomGen = value;
                    OnPropertyChanged(nameof(nomGen));
                }
            }
        }




        public GestionGenVueModele()
        {
            // Initialisation des commandes
            CreeCommand = new RelayCommand(CreeGen);
            AjouteCommand = new RelayCommand(AjouteGen);
            ModifieCommand = new RelayCommand(ModifieGen);
            MajCommand = new RelayCommand(MajGen);
            EffaceCommand = new RelayCommand(EffaceGen);
            GestionCommand = new RelayCommand(GestionGen);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);


            repGen = new RepGeneration();
            List<MGeneration> generations = repGen.GetAll();

            dtData = DataTableTool.ConvertListToDataTable(generations);
        }


        public void CreeGen()
        {
            NavigationServices.NavigateToPage(new CreeGen());
        }

        public void AjouteGen()
        {
            MGeneration nouvelleGen = new MGeneration(nomGen);

            //if (IsSaisieValide)
            //{
            List<MGeneration> generations = repGen.GetAll();

            // Vérifie si le nom du type existe déjà dans la liste des types
            bool genExiste = generations.Any(g => g.nomGen.Equals(nouvelleGen.nomGen, StringComparison.OrdinalIgnoreCase));

            if (genExiste)
            {
                MessageBox.Show("Cette version existe déjà !");
            }
            else
            {        
                // Ajout d'un nouveau type au repository
                repGen.Add(nouvelleGen);                    
             }

                // Actualisation de la liste des types
                generations = repGen.GetAll();
                dtData = DataTableTool.ConvertListToDataTable(generations);

                MessageBox.Show("La version a bien été ajouté !");
                NavigationServices.NavigateToPage(new GestionGen());
            
            //}
            //else
            //{
            //    ErreurSaisie = "Veuillez corriger les erreurs de saisie.";
            //}
            NavigationServices.NavigateToPage(new GestionGen());
        }

        public void ModifieGen() { }

        public void MajGen() { }

        public void EffaceGen()
        {

        }

        public void GestionGen()
        {
            NavigationServices.NavigateToPage(new GestionGen());
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
    }
}
