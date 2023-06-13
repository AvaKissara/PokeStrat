using PokeStat.Modeles;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Data;



namespace PokeStat.Utilitaires
{
    public class DataRowViewToMTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataRowView rowView = value as DataRowView;
            if (rowView != null)
            {
                int idType = (int)rowView["id"];
                string nomType = (string)rowView["Nom type"];
                return new MType(idType, nomType);
            }
            return null;
        }

        private DataRowView FindDataRowViewById(DataView dataView, int id)
        {
            foreach (DataRowView rowView in dataView)
            {
                int rowId = (int)rowView["id"];
                if (rowId == id)
                {
                    return rowView;
                }
            }
            return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MType mType = value as MType;
            if (mType != null)
            {
                //// Recherche de la DataRowView correspondante
                //DataRowView rowView = FindDataRowViewById(dataGridView, mType.idType);

                //if (rowView != null)
                //{
                //    return rowView;
                //}
            }

            // Si la conversion n'est pas possible ou si aucune correspondance n'a été trouvée, vous pouvez retourner Binding.DoNothing ou la valeur cible par défaut.
            return Binding.DoNothing;
        }


    }
}