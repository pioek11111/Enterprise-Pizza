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
    public class TelephoneRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = (string)value;
            Regex regex = new Regex(@"^(?:\+\d{1,3}|0\d{1,3}|00\d{1,2})?(?:\s?\(\d+\))?(?:[-\/\s.]|\d)+$");
            if (string.IsNullOrEmpty(stringValue) || !regex.IsMatch(stringValue))
                return new ValidationResult(false, "Value should be valid phone number");
            else
                return ValidationResult.ValidResult;
        }
    }
}
