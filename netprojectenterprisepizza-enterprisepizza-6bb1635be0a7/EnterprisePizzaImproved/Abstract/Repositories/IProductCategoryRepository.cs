using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> ProductCategoryRepository { get; }

        OperationResult AddNewProductCategory(ProductCategory productCategory);

        OperationResult ChangeProductCategory(ProductCategory oldProductCategory, ProductCategory newProductCategory);

        OperationResult RemoveProductCategory(ProductCategory productCategory);
    }
}
