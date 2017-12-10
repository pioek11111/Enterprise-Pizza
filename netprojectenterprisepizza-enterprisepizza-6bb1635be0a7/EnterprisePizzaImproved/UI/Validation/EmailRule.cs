using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnterprisePizzaImproved.UI.Validation
{
    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = (string)value;
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (string.IsNullOrEmpty(stringValue) || !regex.IsMatch(stringValue))
                return new ValidationResult(false, "Value should be valid email address");
            else
                return ValidationResult.ValidResult;
        }
    }
}
