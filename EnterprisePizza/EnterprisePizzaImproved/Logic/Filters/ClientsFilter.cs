using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Abstract.Filters;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.Logic.Filters
{
    public class ClientsFilter : IClientsFilter
    {
        private EntityDataModel DatabaseContext;

        public ClientsFilter(EntityDataModel databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        public IEnumerable<Customer> FindCustomersByName(string name)
        {            
            return from c in DatabaseContext.Customers
                   where c.Name == name
                   select c;
        }

        public IEnumerable<Customer> FindCustomersByNameSubstring(string nameSubstring)
        {
            return DatabaseContext.Customers.Where(c => c.Name.Contains(nameSubstring));
        }

        public IEnumerable<Customer> FindCustomersByTelephone(string telephone)
        {
            return from c in DatabaseContext.Customers
                where c.Telephone == telephone
                select c;
        }

        public IEnumerable<Customer> FindCustomersByEmail(string email)
        {
            return from c in DatabaseContext.Customers
                where c.Email == email
                select c;
        }

        public IEnumerable<Customer> FindCustomersByOrderTime(DateTime dateFrom, DateTime dateTo)
        {
            return from c in DatabaseContext.Customers
                   join o in DatabaseContext.Orders on c.CustomerId equals o.Customer.CustomerId
                   where o.OrderCreated >= dateFrom && o.OrderCreated <= dateTo
                   select c;
        }
    }
}
