using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PokeStat.Utilitaires
{
    public static class DataTableTool
    {
        public static DataTable ConvertListToDataTable<T>(List<T> models)
        {
            if (models == null || models.Count == 0)
                throw new ArgumentException("La liste de modèles ne peut pas être nulle ou vide.");

            DataTable dataTable = new DataTable();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (IsComplexType(property.PropertyType))
                {
                    AddComplexTypeColumns(property.PropertyType, property.Name, dataTable);
                }
                else
                {
                    dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }
            }

            foreach (T model in models)
            {
                DataRow row = dataTable.NewRow();
                FillDataRow(model, row);
                dataTable.Rows.Add(row);
            }
            

            return dataTable;
        }

        private static void AddComplexTypeColumns(Type type, string prefix, DataTable dataTable)
        {
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string columnName = $"{prefix}.{property.Name}";

                if (IsComplexType(property.PropertyType))
                {
                    AddComplexTypeColumns(property.PropertyType, columnName, dataTable);
                }
                else
                {
                    dataTable.Columns.Add(columnName, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }
            }
        }

        private static void FillDataRow<T>(T model, DataRow row, string prefix = "")
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (IsComplexType(property.PropertyType))
                {
                    FillComplexTypeProperties(property.PropertyType, property.GetValue(model), row, $"{property.Name}.");
                }
                else
                {
                    string columnName = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}{property.Name}";
                    object value = property.GetValue(model);
                    row[columnName] = value ?? DBNull.Value;
                }
            }
        }

        private static void FillComplexTypeProperties(Type type, object complexObject, DataRow row, string prefix)
        {
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string columnName = $"{prefix}{property.Name}";

                if (row.Table.Columns.Contains(columnName))
                {
                    object value = property.GetValue(complexObject, null);
                    row[columnName] = value ?? DBNull.Value;
                }
            }
        }



        private static bool IsComplexType(Type type)
        {
            return !type.IsValueType && type != typeof(string);
        }
    }
}
