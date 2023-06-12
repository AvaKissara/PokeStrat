using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues.CrudType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class GestionTypeVueModel : INotifyPropertyChanged
    {
       
        public ICommand CreeTypeCommand { get; set; }
        public ICommand AjouteTypeCommand { get; set; }
        public ICommand ModifieTypeCommand { get; set; }
        public ICommand EffaceTypeCommand { get; set; }

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



        // EVENT HANDLER
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
