using System;
using System.Linq;
using Castle.Core.Internal;
using EnterprisePizzaImproved.DatabaseFilling;
using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic.Filters;
using EnterprisePizzaImproved.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterprisePizzaTests.Filters
{
    [TestClass]
    public class ClientsFilterTests
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
        public void FindCustomersByNameTest()
        {
            var filter = new ClientsFilter(context);
            var customersByName = filter.FindCustomersByName("Google Polska").ToList();

            Assert.AreEqual(1, customersByName.Count, "Customers count not valid");

            if (!customersByName.IsNullOrEmpty())
            {
                var first = customersByName.First();

                Assert.IsNotNull(first, "Empty customers list");
                Assert.AreEqual("Koszykowa 55555", first.Address, "Customer entity not valid");
            }
        }

        [TestMethod]
        public void FindCustomersByOrderTime()
        {
            var filter = new ClientsFilter(context);

            var dateFrom = DateTime.Parse("2/11/2017 13:00");
            var dateTo1 = DateTime.Parse("2/11/2017 15:00");
            var dateTo2 = DateTime.Parse("2/11/2017 20:00");

            var customers1 = filter.FindCustomersByOrderTime(dateFrom, dateTo1).ToList();
            Assert.AreEqual(1, customers1.Count, "Customers count not valid");
            Assert.IsTrue(customers1.Any(c => c.Name == "Poczta Polska"), "Customers not valid");

            var customers2 = filter.FindCustomersByOrderTime(dateFrom, dateTo2).ToList();
            Assert.AreEqual(2, customers2.Count, "Customers count not valid");
            Assert.IsTrue(customers2.Any(c => c.Name == "Poczta Polska"), "Customers not valid");
            Assert.IsTrue(customers2.Any(c => c.Name == "Google Polska"), "Customers not valid");
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }
    }
}
