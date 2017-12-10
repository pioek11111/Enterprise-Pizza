using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Abstract;
using EnterprisePizzaImproved.Logic.Repositories;
using System.Linq;
using EnterprisePizzaImproved.DatabaseFilling;

namespace EnterprisePizzaTests.EFRepositoriesTests
{
    [TestClass]
    public class AllergenRepositoryTests
    {
        private EntityDataModel context;
        private IAllergenRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EntityDataModel();
            repository = new EFAllergenRepository(context);
            DatabaseFiller.ClearDatabase(context);
            DatabaseFiller.FillWithData(context);
        }

        [TestMethod]
        public void AddNewAllergenTestMethod()
        {
            var allergen = new Allergen { Title = "New allergen", Description = "Allergen description" };
            var firstAllergen = repository.AllergenRepository.First();

            repository.AddNewAllergen(allergen);

            int count = repository.AllergenRepository.Count();
            repository.AddNewAllergen(firstAllergen);
            int count2 = repository.AllergenRepository.Count();

            Assert.IsTrue(repository.AllergenRepository.Any(p => p.Title == "New allergen"), "Allergen not in database");
            Assert.IsTrue(count == count2, "Added existing allergen");
        }

        [TestMethod]
        public void RemoveAllergenTestMethod()
        {
            var allergen = new Allergen { Title = "New allergen", Description = "Allergen description" };
            var firstAllergen = repository.AllergenRepository.First();

            int count = repository.AllergenRepository.Count();
            repository.RemoveProductAllergen(firstAllergen);
            int count2 = repository.AllergenRepository.Count();

            Assert.IsTrue(count == count2 + 1, "Existing allergen not removed");

            count = repository.AllergenRepository.Count();
            repository.RemoveProductAllergen(allergen);
            count2 = repository.AllergenRepository.Count();

            Assert.IsTrue(count == count2, "Removing not existing allergen");
        }

        [TestMethod]
        public void ChangeAllergenTestMethod()
        {
            var newAllergen = new Allergen { Title = "New allergen", Description = "Allergen description" };
            var firstAllergen = repository.AllergenRepository.First();
            int id = firstAllergen.AllergenId;

            int count = repository.AllergenRepository.Count();
            repository.ChangeAllergen(firstAllergen, newAllergen);
            int count2 = repository.AllergenRepository.Count();

            var changedAllergen = repository.AllergenRepository.First(p => p.AllergenId == id);

            Assert.IsTrue(
                count == count2 && 
                changedAllergen.Description.Equals(newAllergen.Description) && 
                changedAllergen.Title.Equals(newAllergen.Title)
                , "Not changed");
        }

        [TestCleanup]
        public void Cleanup()
        {
            //DatabaseFiller.ClearDatabase(context);
            context.Dispose();
        }
    }
}
