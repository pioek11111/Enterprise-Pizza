using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> ProductRepository { get; }

        OperationResult AddNewProduct(Product product);

        OperationResult ChangeProduct(Product oldProduct, Product newProduct);

        OperationResult RemoveProduct(Product product);
    }
}
