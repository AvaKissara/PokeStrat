using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

namespace PokeStat.Utilitaires
{
    public class LetterValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return new ValidationResult(false, "Le champ Nom est requis.");
                }

                if (!text.All(char.IsLetter))
                {
                    return new ValidationResult(false, "Le champ Nom ne peut contenir que des lettres.");
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}

