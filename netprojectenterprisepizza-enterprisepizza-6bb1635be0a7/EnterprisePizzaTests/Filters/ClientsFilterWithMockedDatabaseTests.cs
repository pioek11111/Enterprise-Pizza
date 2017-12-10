using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Castle.Core.Internal;
using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnterprisePizzaTests.Filters
{
    [TestClass]
    public class ClientsFilterWithMockedDatabaseTests
    {
        private EntityDataModel context;

        [TestInitialize]
        public void Initialize()
        {
            var customer1 = new Customer()
            {
                CustomerId = 0,
                Name = "Jan Kowalski",
                Email = "kowalskij@example.com",
                Address = "Akacjowa 2",
                Telephone = "123456789"
            };
            var customer2 = new Customer()
            {
                CustomerId = 1,
                Name = "Piotr Nowak",
                Email = "piotr.nowak@example.com",
                Address = "Klonowa 42",
                Telephone = "555555555"
            };
            var customer3 = new Customer()
            {
                CustomerId = 2,
                Name = "Katarzyna Malinowska",
                Email = "kmalinowska@example.com",
                Address = "Brzozowa 121",
                Telephone = "987654321"
            };
            var customers = new List<Customer>
            {
                customer1,
                customer2,
                customer3
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    Chef = null,
                    Deliveryman = null,
                    CookingDeadline = DateTime.Parse("15/11/2017 9:00"),
                    CustomerWish = "Nie takie przypieczone",
                    Customer = customer1,
                    DeliveryDeadline = DateTime.Parse("15/11/2017 9:30"),
                    OrderCreated = DateTime.Parse("2/11/2017 19:00"),
                    OrderedProducts = new List<CustomizedProduct>(),
                    TotalPrice = 23 + 16,
                    OrderStatus = OrderStatus.Pending,
                },
                new Order()
                {
                    Chef = null,
                    Deliveryman = null,
                    CookingDeadline = DateTime.Parse("15/11/2017 13:00"),
                    CustomerWish = "Poproszę bez sera",
                    Customer = customer2,
                    DeliveryDeadline = DateTime.Parse("15/11/2017 13:30"),
                    OrderCreated = DateTime.Parse("2/11/2017 14:00"),
                    OrderedProducts = new List<CustomizedProduct>(),
                    TotalPrice = 16,
                    OrderStatus = OrderStatus.Pending,
                }

            };

            var contextMock = new Mock<EntityDataModel>();
            var customersMock = MockedDatabaseUtils.CreateDbSetMock(customers);
            var ordersMock = MockedDatabaseUtils.CreateDbSetMock(orders);
            contextMock.Setup(foo => foo.Customers).Returns(customersMock.Object);
            contextMock.Setup(foo => foo.Orders).Returns(ordersMock.Object);

            context = contextMock.Object;
        }

        [TestMethod]
        public void FindCustomersByNameTest()
        {
            var filter = new ClientsFilter(context);
            var customersByName = filter.FindCustomersByName("Katarzyna Malinowska").ToList();

            Assert.AreEqual(1, customersByName.Count, "Customers count not valid");

            if (!customersByName.IsNullOrEmpty())
            {
                var first = customersByName.First();

                Assert.IsNotNull(first, "Empty customers list");
                Assert.AreEqual("Brzozowa 121", first.Address, "Customer entity not valid");
            }

            var customersByName2 = filter.FindCustomersByName("Marcin Brzozowski").ToList();

            Assert.AreEqual(0, customersByName2.Count, "Customers count not valid");
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
            Assert.IsTrue(customers1.Any(c => c.Name == "Piotr Nowak"), "Customers not valid");

            var customers2 = filter.FindCustomersByOrderTime(dateFrom, dateTo2).ToList();
            Assert.AreEqual(2, customers2.Count, "Customers count not valid");
            Assert.IsTrue(customers2.Any(c => c.Name == "Jan Kowalski"), "Customers not valid");
            Assert.IsTrue(customers2.Any(c => c.Name == "Piotr Nowak"), "Customers not valid");
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }
    }
}
