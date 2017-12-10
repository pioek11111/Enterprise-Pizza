using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class TimeInterval
    {
        public TimeInterval() { }

        public TimeInterval(TimeInterval other)
        {
            TimeIntervalId = other.TimeIntervalId;
            From = other.From;
            To = other.To;
        }

        public int TimeIntervalId { get; set; }

        [Required(ErrorMessage = "From field can't be empty")]
        public DateTime From { get; set; }
        
        [Required(ErrorMessage = "From field can't be empty")]
        public DateTime To { get; set; }
        
        public int EmployeeId { get; set; }
    }
}
