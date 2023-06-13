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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}