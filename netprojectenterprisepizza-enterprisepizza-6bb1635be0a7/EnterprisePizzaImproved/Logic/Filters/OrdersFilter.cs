using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.Logic.Filters
{
    public class OrdersFilter : IOrdersFilter
    {
        private EntityDataModel DatabaseContext;

        public OrdersFilter(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<Order> FindOrdersByChef(Employee chef)
        {
            var orders = (from o in DatabaseContext.Orders
                          where o.Chef.EmployeeId == chef.EmployeeId
                          select o);
            return orders;
        }

        public IEnumerable<Order> FindOrdersByCustomer(Customer customer)
        {
            var orders = (from o in DatabaseContext.Orders
                          where o.Customer.CustomerId == customer.CustomerId
                          select o);
            return orders;
        }

        public IEnumerable<Order> FindOrdersByCustomerNameSubstring(string customerNameSubstring)
        {
            var orders = DatabaseContext.Orders.Where(o => o.Customer.Name.Contains(customerNameSubstring));
            return orders;
        }

        public IEnumerable<Order> FindOrdersByDeliveryman(Employee employee)
        {
            var orders = (from o in DatabaseContext.Orders
                          where o.Deliveryman.EmployeeId == employee.EmployeeId
                          select o);
            return orders;
        }

        public IEnumerable<Order> FindOrdersByPrice(decimal minPrice, decimal maxPrice)
        {
            var orders = (from o in DatabaseContext.Orders
                          where o.TotalPrice >= minPrice && o.TotalPrice <= maxPrice
                          select o);
            return orders;
        }

        public IEnumerable<Order> FindOrdersByStatus(OrderStatus orderStatus)
        {
            var orders = (from o in DatabaseContext.Orders
                          where o.OrderStatus == orderStatus
                          select o);
            return orders;
        }

        public IEnumerable<Order> FindOrdersByTime(DateTime orderCreatedTimeFrom, DateTime orderCreatedTimeTo)
        {
            var orders = (from o in DatabaseContext.Orders
                          where o.OrderCreated >= orderCreatedTimeFrom && o.OrderCreated <= orderCreatedTimeTo
                          select o);
            return orders;
        }
    }
}
