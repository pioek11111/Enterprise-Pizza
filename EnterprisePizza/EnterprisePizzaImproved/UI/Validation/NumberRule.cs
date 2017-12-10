using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnterprisePizzaImproved.UI.Validation
{
    public class NumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = (string)value;
            if (!int.TryParse(stringValue, out int _))
                return new ValidationResult(false, "Value must be a number");
            else
                return ValidationResult.ValidResult;
        }
    }
}