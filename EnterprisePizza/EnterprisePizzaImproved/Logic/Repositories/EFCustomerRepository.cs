using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;
using System.Data.Entity;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private EntityDataModel DatabaseContext;

        public IEnumerable<Customer> CustomerRepository => DatabaseContext.Customers;

        public EFCustomerRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public OperationResult AddNewCustomer(Customer customer)
        {
            if (DatabaseContext.Customers.All(c => c.CustomerId != customer.CustomerId))
            {
                DatabaseContext.Customers.Add(customer);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            else
            {
                return new OperationResult {IsSucceeded = false};
            }
        }
 
        public OperationResult ChangeCustomer(Customer oldCustomer, Customer newCustomer)
        {
            var customer = CustomerRepository.First(c => c.CustomerId == oldCustomer.CustomerId);
            customer.Address = newCustomer.Address;
            customer.Email = newCustomer.Email;
            customer.Name = newCustomer.Name;
            customer.Telephone = newCustomer.Telephone;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveCustomer(Customer customer)
        {
            try
            {
                var first = DatabaseContext.Customers.Single(c => c.CustomerId == customer.CustomerId);
                DatabaseContext.Customers.Remove(first);
                DatabaseContext.SaveChanges();
                return new OperationResult() { IsSucceeded = true };
            }
            catch (InvalidOperationException e)
            {
                return new OperationResult { IsSucceeded = false };
            }
        }
    }
}
