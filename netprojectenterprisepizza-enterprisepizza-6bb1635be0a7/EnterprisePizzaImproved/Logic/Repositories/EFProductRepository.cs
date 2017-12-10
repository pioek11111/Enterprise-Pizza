using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private EntityDataModel DatabaseContext;

        public EFProductRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<Product> ProductRepository => DatabaseContext.Products.Include("ProductCategory").Include("Allergens");

        public OperationResult AddNewProduct(Product product)
        {
            if (DatabaseContext.Products.All(p => p.ProductId != product.ProductId))
            {
                DatabaseContext.Products.Add(product);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            else
            {
                return new OperationResult {IsSucceeded = false};
            }
        }

        public OperationResult ChangeProduct(Product oldProduct, Product newProduct)
        {
            var existingProduct = DatabaseContext.Products.First(p => p.ProductId == oldProduct.ProductId);

            var newProductAllergensIds = newProduct.Allergens.Select(al => al.AllergenId);
            var newAllergens = DatabaseContext.Allergens
                .Where(al => newProductAllergensIds.Contains(al.AllergenId)).ToList();
            existingProduct.Allergens.Clear();
            foreach (var al in newAllergens)
            {
                existingProduct.Allergens.Add(al);
            }

            existingProduct.BasePrice = newProduct.BasePrice;
            existingProduct.Description = newProduct.Description;
            existingProduct.PrepareTime = newProduct.PrepareTime;
            existingProduct.ProductCategory = newProduct.ProductCategory;
            existingProduct.Title = newProduct.Title;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveProduct(Product product)
        {
            try
            {
                var first = DatabaseContext.Products.Single(p => p.ProductId == product.ProductId);
                DatabaseContext.Products.Remove(first);
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
