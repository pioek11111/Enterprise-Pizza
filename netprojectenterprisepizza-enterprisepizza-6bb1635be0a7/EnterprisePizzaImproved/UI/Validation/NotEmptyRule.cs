using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnterprisePizzaImproved.UI.Validation
{
    public class NotEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = (string)value;
            if (string.IsNullOrEmpty(stringValue))
                return new ValidationResult(false, "Value cannot be empty");
            else
                return ValidationResult.ValidResult;
        }
    }
}