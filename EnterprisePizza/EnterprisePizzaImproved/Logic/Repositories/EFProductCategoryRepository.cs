using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    public class EFProductCategoryRepository : IProductCategoryRepository
    {
        private EntityDataModel DatabaseContext;

        public EFProductCategoryRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<ProductCategory> ProductCategoryRepository => DatabaseContext.ProductCategories;

        public OperationResult AddNewProductCategory(ProductCategory productCategory)
        {
            if (DatabaseContext.ProductCategories.All(pc => pc.ProductCategoryId == productCategory.ProductCategoryId))
            {
                DatabaseContext.ProductCategories.Add(productCategory);
                DatabaseContext.SaveChanges();
                return new OperationResult { IsSucceeded = true };
            }
            else
            {
                return new OperationResult { IsSucceeded = false };
            }
        }

        public OperationResult ChangeProductCategory(ProductCategory oldProductCategory, ProductCategory newProductCategory)
        {
            var existingProductCategory = DatabaseContext.ProductCategories.Single(pc => pc.ProductCategoryId ==
                                                                                 oldProductCategory.ProductCategoryId);
            existingProductCategory.Name = newProductCategory.Name;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveProductCategory(ProductCategory productCategory)
        {
            try
            {
                var first = DatabaseContext.ProductCategories.Single(pc =>
                    pc.ProductCategoryId == productCategory.ProductCategoryId);
                DatabaseContext.ProductCategories.Remove(first);
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
