using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    class EFEmployeeCategoryRepository : IEmployeeCategoryRepository
    {
        private EntityDataModel DatabaseContext = new EntityDataModel();

        public IEnumerable<EmployeeCategory> EmployeeCategoryRepository => DatabaseContext.EmployeeCategories;
    }
}
