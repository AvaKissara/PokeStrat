using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.CrudType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace PokeStat.VuesModeles
{
    public class GestionTypeVueModel : INotifyPropertyChanged
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand CreeTypeCommand { get; set; }
        public ICommand AjouteTypeCommand { get; set; }
        public ICommand ModifieTypeCommand { get; set; }
        public ICommand MajTypeCommand { get; set; }
        public ICommand EffaceTypeCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand GestionTypeCommand { get; set; }
        public ICommand CloseCommand { get; }

        //Propriété de type MType corrrespondant à l'élément actuellement sélectionné dans la liste des types.
        private MType _ligneSelection;
        public MType LigneSelection
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


        RepType repType = new RepType();

        //Propriété de type DataTable stockant des objets MType
        private DataTable dtTypes;

        public DataTable DtTypes
        {
            get { return dtTypes; }
            set
            {
                if (dtTypes != value)
                {
                    dtTypes = value;
                    OnPropertyChanged(nameof(DtTypes));
                }
            }
        }

        //Propriété de type string utilisée pour contrôler la saisie d'un type
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
        
        //Propriété de type string liée au nom de type saisi.
        private string nomType;
        public string NomType
        {
            get { return nomType; }
            set
            {
                if (nomType != value)
                {
                    nomType = value;
                    ValidateNomType(); 
                    OnPropertyChanged(nameof(NomType));
                }
            }
        }

        public int IdType;
     

        private List<DataRowView> dataRowViews;
        public List<DataRowView> DataRowViews
        {
            get { return dataRowViews; }
        }

        public DataRowView FindDataRowViewById(int id)
        {
            return dataRowViews.Find(rv => (int)rv["ID"] == id);
        }

        public GestionTypeVueModel()
        {
            // Initialisation des commandes
            CreeTypeCommand = new RelayCommand(CreeType);
            AjouteTypeCommand = new RelayCommand(AjouteType);
            ModifieTypeCommand = new RelayCommand(ModifieType);
            MajTypeCommand = new RelayCommand(MajType);
            EffaceTypeCommand = new RelayCommand(EffaceType);
            GestionTypeCommand = new RelayCommand(GestionType);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);

            // Récupération des données de types  depuis le repository
            List<MType> types = repType.GetTypes();

            // Conversion de la liste de types en DataTable
            DtTypes = ConvertListToDataTable(types);

            // Initialisation de la liste des DataRowView
            dataRowViews = new List<DataRowView>();
        }

        private void CreeType()
        {
            // Vers la page de création de type
            NavigationServices.NavigateToPage(new CreeType());
        }

        private void AjouteType()
        {
            MType nouveauType = new MType(NomType);
           
            if (IsSaisieValide)
            {
                List<MType> types = repType.GetTypes();

                // Vérifie si le nom du type existe déjà dans la liste des types
                bool typeExiste = types.Any(t => t.nomType.Equals(nouveauType.nomType, StringComparison.OrdinalIgnoreCase));

                if (typeExiste)
                {
                    MessageBox.Show("Ce type existe déjà !");
                }
                else
                {
                    // Ajout d'un nouveau type au repository
                    repType.AddType(nouveauType);

                    // Actualisation de la liste des types
                    types = repType.GetTypes();
                    DtTypes = ConvertListToDataTable(types);

                    MessageBox.Show("Le type a bien été ajouté !");
                    NavigationServices.NavigateToPage(new GestionType());
                }
            }
            else
            {
                ErreurSaisie = "Veuillez corriger les erreurs de saisie.";
            }
        }


        private void ModifieType()
        {
            List<MType> types = repType.GetTypes();
           
            if (DtTypes != null && DtTypes.Rows.Count > 0)
            { 
                //Cherche le type qui correspond à la ligne sélectionnée
                foreach (MType type in types)
                {
                    if (type.Equals(LigneSelection))
                    {
                        // Mise à jour de la ligne sélectionnée avec le type correspondant
                        LigneSelection = type;
                    }
                }
            }

            // Vers la page de modification du type avec la ligne sélectionnée en argument
            NavigationServices.NavigateToPage(new ModifieType(LigneSelection));
        }

        public void MajType()
        {
            if (IsSaisieValide)
            {
                List<MType> types = repType.GetTypes();
                // Mise à jour du nom du type avec la saisie de l'admin
                LigneSelection.nomType = nomType;
                // Vérifie si le nom du type existe déjà dans la liste des types
                bool typeExiste = types.Any(t => t.nomType.Equals(LigneSelection.nomType, StringComparison.OrdinalIgnoreCase));

                if (typeExiste)
                {
                    MessageBox.Show("Ce type existe déjà !");
                }
                else
                {
                    repType.UpdateType(LigneSelection);

                // Actualisation de la liste des types
                types = repType.GetTypes();
                DtTypes = ConvertListToDataTable(types);

                // Message de confirmation
                MessageBox.Show("Le type a bien été modifié !");

                // Vers la page de gestion des types
                NavigationServices.NavigateToPage(new GestionType());
                }
            }
            else
            {
                ErreurSaisie = "Veuillez corriger les erreurs de saisie.";
            }
        }

        private void EffaceType()
        {
            List<MType> types = repType.GetTypes();

            if (DtTypes != null && DtTypes.Rows.Count > 0)
            {
                int idTypeAEfface = LigneSelection.idType;

                // Cherche le type à effacer en fonction de son id
                MType typeAEffacer = types.FirstOrDefault(t => t.idType == idTypeAEfface);
                if (typeAEffacer != null)
                {
                    // Boîte de dialogue de confirmation
                    MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce type ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    // Vérifie la réponse de l'admin
                    if (result == MessageBoxResult.Yes)
                    {
                        // Envoi le type à supprimer vers le Repository
                        repType.DeleteType(idTypeAEfface);

                        // Actualisation de la liste des types
                        types = repType.GetTypes();
                        DtTypes = ConvertListToDataTable(types);
                    }
                }
                else
                {
                    MessageBox.Show("Echec !");
                }
            }
        }

        private void GestionType()
        {
            // Vers la page de gestion des types
            NavigationServices.NavigateToPage(new GestionType());
        }

        private void AccueilPage()
        {
            // Vers la page d'accueil
            Page accueilPage = new AccueilPage();
            NavigationServices.NavigateToPage(accueilPage);
        }

        /// <summary>
        /// Conversion d'une liste de MType en DataTable
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private DataTable ConvertListToDataTable(List<MType> types)
        {
            DataTable dtType = new DataTable();
            dtType.Columns.Add("ID", typeof(int));
            dtType.Columns.Add("NOM", typeof(string));

            foreach (var type in types)
            {
                DataRow row;
                row = dtType.NewRow();
                row[0] = type.idType;
                row[1] = type.nomType;
                dtType.Rows.Add(row);
            }

            return dtType;
        }

        private void Close()
        {
            // Fermer l'application
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Contrôle de la saisie
        /// </summary>
        private void ValidateNomType()
        {
            if (string.IsNullOrEmpty(NomType))
            {
                ErreurSaisie = "Le champ Nom est requis.";
                IsSaisieValide = false;
            }
            else if (!NomType.All(char.IsLetter))
            {
                ErreurSaisie = "Le champ Nom ne peut contenir que des lettres.";
                IsSaisieValide = false;
            }
            else
            {
                ErreurSaisie = string.Empty;
                IsSaisieValide = true;
            }
        }


        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
