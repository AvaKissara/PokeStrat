using Newtonsoft.Json;
using PokeStat.Modeles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PokeStat.Utilitaires
{
    public class DRVToModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Vérifier si la valeur est nulle ou n'est pas du type attendu
            if (value == null || !(value is DataGrid))
            {
                // Retourner une valeur par défaut appropriée pour votre cas
                return null; // ou une autre valeur par défaut que vous souhaitez
            }

            var dataGrid = (DataGrid)value;

            // Vérifier si un élément est sélectionné dans la DataGrid
            if (dataGrid.SelectedItem == null || !(dataGrid.SelectedItem is DataRowView))
            {
                // Retourner une valeur par défaut appropriée pour votre cas
                return null; // ou une autre valeur par défaut que vous souhaitez
            }

            var selectedRow = (DataRowView)dataGrid.SelectedItem;

            // Convertir la DataRowView en type spécifié
            return selectedRow.Row;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DataRowView dataRowView)
            {
                Type desiredType = targetType;

                if (parameter is Type parameterType)
                {
                    desiredType = parameterType;
                }

                if (desiredType != null)
                {
                    object convertedValue = Activator.CreateInstance(desiredType);

                    PropertyInfo[] properties = desiredType.GetProperties();

                    for (int i = 0; i < properties.Length; i++)
                    {
                        PropertyInfo property = properties[i];

                        if (property.CanWrite && dataRowView.Row.Table.Columns.Contains(property.Name))
                        {
                            object propertyValue = dataRowView.Row[property.Name];

                            if (propertyValue != DBNull.Value)
                            {
                                property.SetValue(convertedValue, propertyValue);
                            }
                        }
                    }

                    return convertedValue;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot convert DataRowView to {desiredType.Name}. Invalid data type.");
                }
            }

            return null;
        }

    }
}
