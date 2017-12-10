using EnterprisePizzaImproved.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IOrdersFilter
    {
        IEnumerable<Order> FindOrdersByCustomer(Customer customer);

        IEnumerable<Order> FindOrdersByDeliveryman(Employee employee);

        IEnumerable<Order> FindOrdersByTime(DateTime orderCreatedTimeFrom, DateTime orderCreatedTimeTo);

        IEnumerable<Order> FindOrdersByStatus(OrderStatus orderStatus);

        IEnumerable<Order> FindOrdersByChef(Employee chef);

        IEnumerable<Order> FindOrdersByPrice(decimal minPrice, decimal maxPrice);
    }
}
