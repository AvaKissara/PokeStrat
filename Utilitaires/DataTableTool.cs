using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PokeStat.Utilitaires
{
    public static class DataTableTool
    {
        public static DataTable ConvertListToDataTable<T>(List<T> modeles, Dictionary<string, string> columnMappings)
        {
            DataTable dtData = new DataTable();

            // Ajoute les colonnes au DataTable en utilisant les mappings spécifiés
            foreach (var columnMapping in columnMappings)
            {
                dtData.Columns.Add(columnMapping.Key, typeof(T).GetProperty(columnMapping.Value).PropertyType);
            }

            // Ajoute les éléments à chaque ligne du DataTable
            foreach (var modele in modeles)
            {
                DataRow row = dtData.NewRow();

                foreach (var columnMapping in columnMappings)
                {
                    row[columnMapping.Key] = typeof(T).GetProperty(columnMapping.Value).GetValue(modele);
                }

                dtData.Rows.Add(row);
            }

            return dtData;
        }
    }
}

