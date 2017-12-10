using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class Order
    {
        public Order() { }
        public Order(Order o)
        {
            OrderId = o.OrderId;
            Customer = o.Customer;
            OrderedProducts = o.OrderedProducts;
            Chef = o.Chef;
            Deliveryman = o.Deliveryman;
            OrderStatus = o.OrderStatus;
            CustomerWish = o.CustomerWish;
            CookingDeadline = o.CookingDeadline;
            DeliveryDeadline = o.DeliveryDeadline;
            OrderCreated = o.OrderCreated;
            TotalPrice = o.TotalPrice;
        }

        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<CustomizedProduct> OrderedProducts { get; set; }
        public Employee Chef { get; set; }
        public Employee Deliveryman { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string CustomerWish { get; set; }
        public DateTime CookingDeadline { get; set; }
        public DateTime DeliveryDeadline { get; set; }
        public DateTime OrderCreated { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Cooking,
        Cooked,
        Delivering,
        Delivered
    }
}
