using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class CustomizedProduct
    {
        public CustomizedProduct() { }

        public CustomizedProduct(CustomizedProduct cp)
        {
            CustomizedProductId = cp.CustomizedProductId;
            BaseProduct = cp.BaseProduct;
            Toppings = cp.Toppings;
            CustomerWish = cp.CustomerWish;
        }

        public int CustomizedProductId { get; set; }
        public Product BaseProduct { get; set; }
        public List<Topping> Toppings { get; set; }
        public string CustomerWish { get; set; }
    }
}
