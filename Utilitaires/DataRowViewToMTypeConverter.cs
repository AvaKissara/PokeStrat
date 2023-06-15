﻿using System;
using System.Data;
using System.Globalization;
using System.Windows.Data;
using PokeStat.Modeles;

namespace PokeStat.Utilitaires
{
    public class DataRowViewToMTypeConverter : IValueConverter
    {      
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DataRowView rowView)
            {
                // Convertir la DataRowView en instance de MType
                return new MType
                {
                    idType = (int)rowView["ID"],
                    nomType = (string)rowView["NOM"]
                };
            }

            return null;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
