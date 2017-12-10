using System;
using System.Collections.Generic;
using System.Linq;
using EnterprisePizzaImproved.Abstract;
using EnterprisePizzaImproved.DatabaseFilling;
using EnterprisePizzaImproved.Logic.Repositories;
using EnterprisePizzaImproved.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterprisePizzaTests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private EntityDataModel context;

        [TestInitialize]
        public void Initialize()
        {
            context = new EntityDataModel();
            DatabaseFiller.ClearDatabase(context);
            DatabaseFiller.FillWithData(context);
        }

        [TestMethod]
        public void TestAddingAndRemovingProduct()
        {
            IProductCategoryRepository productCategoryRepository = new EFProductCategoryRepository(context);
            var pizzaCategory = productCategoryRepository.ProductCategoryRepository.First(pc => pc.Name == "Pizza");

            IProductRepository productRepository = new EFProductRepository(context);
            var product = new Product
            {
                Allergens = new List<Allergen>
                {
                    new Allergen {Description = "Opis alergenu", Title = "JakisAlergen"}
                },
                Description = "OpisOpisOpisOpis",
                BasePrice = 29.99m,
                PrepareTime = TimeSpan.FromMinutes(30),
                ProductCategory = pizzaCategory,
                Title = "Wiejska"
            };
            productRepository.AddNewProduct(product);

            Assert.IsTrue(productRepository.ProductRepository.Any(p => p.Title == "Wiejska"), "Added product not in the database");
            Assert.IsTrue(context.Allergens.Any(al => al.Title == "JakisAlergen"), "Added allergen not in the database");

            productRepository.RemoveProduct(product);
            Assert.IsFalse(productRepository.ProductRepository.Any(p => p.Title == "Wiejska"), "Removed product still in the database");
            Assert.IsTrue(context.Allergens.Any(al => al.Title == "JakisAlergen"), "Allergen not in database");
        }

        [TestMethod]
        public void TestChangingProduct()
        {
            IProductRepository repository = new EFProductRepository(context);
            var product = repository.ProductRepository.First(pr => pr.Title == "Cheese Burger");
            product.BasePrice = 50m;
            repository.ChangeProduct(product, product);

            Assert.IsTrue(repository.ProductRepository.Any(pr => pr.Title == "Cheese Burger" && pr.BasePrice == 50m));
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }
    }
}
