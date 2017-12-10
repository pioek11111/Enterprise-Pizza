using EnterprisePizzaImproved.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract.Filters
{
    public interface IClientsFilter
    {
        IEnumerable<Customer> FindCustomersByName(string name);

        IEnumerable<Customer> FindCustomersByTelephone(string telephone);

        IEnumerable<Customer> FindCustomersByEmail(string email);

        IEnumerable<Customer> FindCustomersByOrderTime(DateTime from, DateTime to);
    }
}
