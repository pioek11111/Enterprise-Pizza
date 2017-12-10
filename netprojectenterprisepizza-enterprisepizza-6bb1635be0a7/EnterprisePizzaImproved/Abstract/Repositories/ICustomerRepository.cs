using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> CustomerRepository { get; }

        OperationResult AddNewCustomer(Customer customer);

        OperationResult ChangeCustomer(Customer oldCustomer, Customer newCustomer);

        OperationResult RemoveCustomer(Customer customer);
    }
}
