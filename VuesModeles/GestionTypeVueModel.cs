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
        public ICommand MajTypeCommand { get; set; }
        public ICommand EffaceTypeCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand CloseCommand { get; }  
       

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



        private List<DataRowView> dataRowViews;

        public GestionTypeVueModel()
        {
            CreeTypeCommand = new RelayCommand(CreeType);
            AjouteTypeCommand = new RelayCommand(AjouteType);
            ModifieTypeCommand = new RelayCommand(ModifieType);
            MajTypeCommand = new RelayCommand(MajType);
            EffaceTypeCommand = new RelayCommand(EffaceType);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);


            RepType repType = new RepType();
            List<MType> types = repType.GetTypes();
            DtTypes = ConvertListToDataTable(types);
            // Initialisez la liste des DataRowView
            dataRowViews = new List<DataRowView>();

        }

        public List<DataRowView> DataRowViews
        {
            get { return dataRowViews; }
        }

        public void AddDataRowView(DataRowView rowView)
        {
            dataRowViews.Add(rowView);
        }

        public DataRowView FindDataRowViewById(int id)
        {
            return dataRowViews.Find(rv => (int)rv["id"] == id);
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
            RepType repType = new RepType();
            List<MType> types = repType.GetTypes();

            if (DtTypes != null && DtTypes.Rows.Count > 0)
            {
                foreach (MType type in types)
                {
                    if (type.Equals(LigneSelection))
                    {
                        LigneSelection = type;
                        break;
                    }
                }
            }
            NavigationServices.NavigateToPage(new ModifieType(LigneSelection));
        }


        public void MajType()
        {
            RepType repType = new RepType();
            LigneSelection.nomType = nomType;
            repType.UpdateType(LigneSelection);
            List<MType> types = repType.GetTypes();
            DtTypes = ConvertListToDataTable(types);
            NavigationServices.NavigateToPage(new MajType());
        }



        private void EffaceType()
        {
            RepType repType = new RepType();

            List<MType> types = repType.GetTypes();
            if (DtTypes != null && DtTypes.Rows.Count > 0)
            {
                int entryId = LigneSelection.idType;

                MType typeToDelete = types.FirstOrDefault(t => t.idType == entryId);
                if (typeToDelete != null)
                {
                    repType.DeleteType(entryId);

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
