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
    public class EFOrderRepository : IOrderRepository
    {
        private EntityDataModel DatabaseContext;

        public IEnumerable<Order> OrderRepository => DatabaseContext.Orders.Include(o => o.OrderedProducts)
            .Include(o => o.OrderedProducts.Select(op => op.BaseProduct));

        public EFOrderRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public OperationResult AddNewOrder(Order order)
        {
            if (DatabaseContext.Orders.All(o => o.OrderId != order.OrderId))
            {
                DatabaseContext.Orders.Add(order);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            else
            {
                return new OperationResult {IsSucceeded = false};
            }
        }

        public OperationResult ChangeOrder(Order oldOrder, Order newOrder)
        {
            var order = DatabaseContext.Orders.First(o => o.OrderId == oldOrder.OrderId);
            order.Chef = newOrder.Chef;
            order.CookingDeadline = newOrder.CookingDeadline;
            order.Customer = newOrder.Customer;
            order.CustomerWish = newOrder.CustomerWish;
            order.DeliveryDeadline = newOrder.DeliveryDeadline;
            order.Deliveryman = newOrder.Deliveryman;
            order.OrderCreated = newOrder.OrderCreated;
            order.OrderedProducts = newOrder.OrderedProducts;
            order.OrderStatus = newOrder.OrderStatus;
            order.TotalPrice = newOrder.TotalPrice;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveOrder(Order order)
        {
            try
            {
                var first = DatabaseContext.Orders.Single(o => o.OrderId == order.OrderId);
                DatabaseContext.Orders.Remove(first);
                DatabaseContext.SaveChanges();
                return new OperationResult { IsSucceeded = true };
            }
            catch (InvalidOperationException e)
            {
                return new OperationResult { IsSucceeded = false };
            }
        }
    }
}
