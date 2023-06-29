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
            if (value is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem is DataRowView selectedRow)
                {
                    // Convertir la DataRowView en type spécifié
                    return selectedRow.Row;
                }
            }

            return null;
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
                    object[] values = dataRowView.Row.ItemArray;

                    object convertedValue = Activator.CreateInstance(desiredType);

                    PropertyInfo[] properties = desiredType.GetProperties();

                    for (int i = 0; i < values.Length && i < properties.Length; i++)
                    {
                        PropertyInfo property = properties[i];

                        if (property.CanWrite)
                        {
                            object propertyValue = values[i];
                            Type propertyType = property.PropertyType;

                            // Vérifier si la valeur est null
                            if (propertyValue == DBNull.Value)
                            {
                                property.SetValue(convertedValue, null);
                                continue;
                            }

                            // Vérifier si le type de propriété est nullable
                            if (Nullable.GetUnderlyingType(propertyType) != null)
                            {
                                propertyType = Nullable.GetUnderlyingType(propertyType);
                            }

                            // Convertir la valeur en fonction du type de propriété
                            try
                            {
                                object convertedPropertyValue = Convert.ChangeType(propertyValue, propertyType);
                                property.SetValue(convertedValue, convertedPropertyValue);
                            }
                            catch (InvalidCastException)
                            {
                                // Gérer les conversions de types spécifiques ici
                                // Exemple : si la propriété est de type decimal et la valeur est de type string
                                if (propertyType == typeof(decimal) && propertyValue is string)
                                {
                                    decimal decimalValue;
                                    if (decimal.TryParse((string)propertyValue, out decimalValue))
                                    {
                                        property.SetValue(convertedValue, decimalValue);
                                    }
                                    else
                                    {
                                        // Gérer l'erreur de conversion
                                    }
                                }
                                else
                                {
                                    // Gérer les autres types de conversion ou lancer une exception
                                    throw;
                                }
                            }
                        }
                    }

                    string json = JsonConvert.SerializeObject(convertedValue);

                    // ...

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
