using EnterprisePizzaImproved.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface ITimeIntervalRepository
    {
        IEnumerable<TimeInterval> TimeIntervalRepository { get; }
    }
}
