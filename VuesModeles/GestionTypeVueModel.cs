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

namespace PokeStat.VuesModeles
{
    public class GestionTypeVueModel : INotifyPropertyChanged
    {
       
        public ICommand CreeTypeCommand { get; set; }
        public ICommand AjouteTypeCommand { get; set; }
        public ICommand ModifieTypeCommand { get; set; }
        public ICommand EffaceTypeCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }

        public int IdType;
        private string nomType { get; set; }
        public string NomType
        {
            get { return nomType; }
            set
            {
                if (nomType != value)
                {
                    nomType = value;
                    OnPropertyChanged(nameof(NomType));
                }
            }
        }

        private MType _ligneSelection;
        public MType LigneSelection
        {
            get { return _ligneSelection; }
            set
            {
                _ligneSelection = value;
                OnPropertyChanged(nameof(LigneSelection));
            }
        }

        private int _idLigneSelectionnee;
        public int IdLigneSelectionnee
        {
            get { return _idLigneSelectionnee; }
            set
            {
                _idLigneSelectionnee = value;
                OnPropertyChanged(nameof(IdLigneSelectionnee));
            }
        }


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

        public GestionTypeVueModel()
        {
            CreeTypeCommand = new RelayCommand(CreeType);
            AjouteTypeCommand = new RelayCommand(AjouteType);
            ModifieTypeCommand = new RelayCommand(ModifieType);
            EffaceTypeCommand = new RelayCommand(EffaceType);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);


            RepType repType = new RepType();
            List<MType> types = repType.GetTypes();
            DtTypes = ConvertListToDataTable(types);
        }

        private void CreeType()
        {
            NavigationServices.NavigateToPage(new CreeType());

        }
        private void AjouteType()
        {
            NavigationServices.NavigateToPage(new AjouteType());
            MType nouveauType = new MType(NomType);
            RepType repType = new RepType();
            repType.AddType(nouveauType);

            // Actualiser la liste des types
            List<MType> types = repType.GetTypes();
            DtTypes = ConvertListToDataTable(types);

            // Réinitialiser le champ NomType
            NomType = string.Empty;

        }

        private void ModifieType()
        {

        }

        private void EffaceType()
        {
            RepType repType = new RepType();

            List<MType> types = repType.GetTypes();
            if (DtTypes != null && DtTypes.Rows.Count > 0)
            {
                // Récupérer l'identifiant de l'entrée à supprimer à partir de la colonne 0
                int entryId = Convert.ToInt32(DtTypes.Rows[0]);

                // Vérifier si l'identifiant existe dans la liste types
                MType typeToDelete = types.FirstOrDefault(t => t.idType == entryId);
                if (typeToDelete != null)
                {



                    // Utiliser la méthode deleteType du repository pour supprimer l'entrée
                    repType.deleteType(entryId);


                    types = repType.GetTypes();
                    DtTypes = ConvertListToDataTable(types);

                }
                else
                {
                    MessageBox.Show("Echec !");
                }
            }
        }

        private void AccueilPage()
        {
            Page accueilPage = new AccueilPage(); 
            NavigationServices.NavigateToPage(accueilPage);

        }

        // Convertir une liste de MType en DataTable
        private DataTable ConvertListToDataTable(List<MType> types)
        {
            DataTable dtType = new DataTable();

            // Ajouter les colonnes à la DataTable
            dtType.Columns.Add("id", typeof(int));
            dtType.Columns.Add("Nom type", typeof(string));

            // Ajouter les données à la DataTable
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
            Application.Current.Shutdown();
        }


        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
