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
    public class EFCustomizedProductRepository : ICustomizedProductRepository
    {
        private EntityDataModel DatabaseContext = new EntityDataModel();

        public IEnumerable<CustomizedProduct> CustomizedProductRepository => DatabaseContext.CustomizedProducts.Include("BaseProduct").Include("Toppings");

        public EFCustomizedProductRepository(EntityDataModel context)
        {
            DatabaseContext = context;
        }

        public OperationResult AddNewCustomizedProduct(CustomizedProduct customizedProduct)
        {
            if (DatabaseContext.CustomizedProducts.All(cp =>
                cp.CustomizedProductId != customizedProduct.CustomizedProductId))
            {
                DatabaseContext.CustomizedProducts.Add(customizedProduct);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            else
            {
                return new OperationResult {IsSucceeded = false};
            }
        }

        public OperationResult ChangeCustomizedProduct(CustomizedProduct oldCustomizedProduct, CustomizedProduct newCustomizedProduct)
        {
            var cp = CustomizedProductRepository.First(c => c.CustomizedProductId == oldCustomizedProduct.CustomizedProductId);
            cp.BaseProduct = newCustomizedProduct.BaseProduct;
            cp.Toppings = newCustomizedProduct.Toppings;
            cp.CustomerWish = newCustomizedProduct.CustomerWish;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveCustomizedProduct(CustomizedProduct customizedProduct)
        {
            try
            {
                var first = DatabaseContext.CustomizedProducts.Single(cp =>
                    cp.CustomizedProductId == customizedProduct.CustomizedProductId);
                DatabaseContext.CustomizedProducts.Remove(first);
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
