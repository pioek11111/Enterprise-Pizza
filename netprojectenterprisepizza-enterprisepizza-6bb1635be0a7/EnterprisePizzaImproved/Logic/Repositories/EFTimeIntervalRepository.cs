using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    public class EFTimeIntervalRepository : ITimeIntervalRepository
    {
        private EntityDataModel DatabaseContext;

        public EFTimeIntervalRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<TimeInterval> TimeIntervalRepository => DatabaseContext.TimeIntervals;
    }
}
