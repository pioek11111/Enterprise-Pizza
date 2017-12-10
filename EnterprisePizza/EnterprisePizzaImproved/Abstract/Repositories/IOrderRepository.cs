using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IOrderRepository
    {
        IEnumerable<Order> OrderRepository { get; }

        OperationResult AddNewOrder(Order order);

        OperationResult ChangeOrder(Order oldOrder, Order newOrder);

        OperationResult RemoveOrder(Order order);
    }
}
