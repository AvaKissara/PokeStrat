using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Utilitaires
{
    public static class CollectionTool
    {
        public static IEnumerable<IEnumerable<string>> ConvertListToEnumerable<T>(List<T> models)
        {
            if (models == null || models.Count == 0)
                throw new ArgumentException("La liste de modèles ne peut pas être nulle ou vide.");

            List<string> propertyNames = GetPropertyNames<T>().ToList();
            List<List<string>> resultList = new List<List<string>>();

            foreach (T model in models)
            {
                List<string> itemValues = new List<string>();

                foreach (string propertyName in propertyNames)
                {
                    var propertyValue = GetPropertyValue(model, propertyName);
                    itemValues.Add(propertyValue != null ? propertyValue.ToString() : string.Empty);
                }

                resultList.Add(itemValues);
            }

            return resultList;
        }

        private static IEnumerable<string> GetPropertyNames<T>()
        {
            return typeof(T).GetProperties().Select(property => property.Name);
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }
    }
}
