using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.DatabaseFilling;
using EnterprisePizzaImproved.Abstract;
using EnterprisePizzaImproved.Logic.Filters;
using EnterprisePizzaImproved.Logic.Repositories;
using System.Linq;
using Moq;

namespace EnterprisePizzaTests.Filters
{
    /// <summary>
    /// Summary description for OrdersFilterWithMockedDatabaseTests
    /// </summary>
    [TestClass]
    public class OrdersFilterWithMockedDatabaseTests
    {
        private EntityDataModel context;

        private IOrdersFilter filter;

        public object EFEmployeeRepository { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            var customer1 = new Customer()
            {
                CustomerId = 1,
                Name = "Jan Kowalski",
                Email = "kowalskij@example.com",
                Address = "Akacjowa 2",
                Telephone = "123456789"
            };
            var customer2 = new Customer()
            {
                CustomerId = 2,
                Name = "Piotr Nowak",
                Email = "piotr.nowak@example.com",
                Address = "Klonowa 42",
                Telephone = "555555555"
            };
            var customer3 = new Customer()
            {
                CustomerId = 3,
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


            var chef1 = new Employee()
            {
                EmployeeId = 1,
                Address = "Mieszkalna 123",
                BirthDate = DateTime.Parse("10/10/1950"),
                Category = null,
                Email = "john.smith@smiths.com",
                Name = "John Smith",
                Salary = 1234500,
                Telephone = "123-125-865",
                ProductCategoryCompetency = new List<ProductCategory>(),
                AvailableIntervals = new List<TimeInterval>()
            };

            var chef2 = new Employee()
            {
                EmployeeId = 2,
                Address = "Wypiekalna 123",
                BirthDate = DateTime.Parse("10/10/1980"),
                Category = null,
                Email = "bezier@adams.com",
                Name = "Adam Bezier",
                Salary = 4624500,
                Telephone = "437-355-124",
                ProductCategoryCompetency = new List<ProductCategory>(),
                AvailableIntervals = new List<TimeInterval>()
            };

            var deliveryman1 = new Employee()
            {
                EmployeeId = 3,
                Address = "Dostarczalna 3",
                BirthDate = DateTime.Parse("10/10/1990"),
                Category = null,
                Email = "alfred@courier.edu.com",
                Name = "Alfred Courier",
                Salary = 1224500,
                Telephone = "214-514-243",
                AvailableIntervals = new List<TimeInterval>()
            };

            var employees = new List<Employee>()
            {
                chef1,
                chef2,
                deliveryman1
            };

            var customizedProducts = new List<CustomizedProduct>()
            {
                new CustomizedProduct()
                {
                    CustomizedProductId = 1,
                    Toppings = new List<Topping>(),
                    BaseProduct = new Product() {ProductId = 1}
                },
                new CustomizedProduct()
                {
                    CustomizedProductId = 2,
                    Toppings = new List<Topping>(),
                    BaseProduct = new Product() {ProductId = 2}
                }
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    OrderId = 1,
                    Chef = chef1,
                    Deliveryman = deliveryman1,
                    CookingDeadline = DateTime.Parse("15/11/2017 9:00"),
                    CustomerWish = "Nie takie przypieczone",
                    Customer = customer1,
                    DeliveryDeadline = DateTime.Parse("15/11/2017 9:30"),
                    OrderCreated = DateTime.Parse("2/11/2017 19:00"),
                    OrderedProducts = new List<CustomizedProduct>() {customizedProducts[0]},
                    TotalPrice = 23 + 16,
                    OrderStatus = OrderStatus.Pending,
                },
                new Order()
                {
                    OrderId = 2,
                    Chef = chef2,
                    Deliveryman = deliveryman1,
                    CookingDeadline = DateTime.Parse("15/11/2017 13:00"),
                    CustomerWish = "Poproszę bez sera",
                    Customer = customer2,
                    DeliveryDeadline = DateTime.Parse("15/11/2017 13:30"),
                    OrderCreated = DateTime.Parse("2/11/2017 14:00"),
                    OrderedProducts = new List<CustomizedProduct>() {customizedProducts[1]},
                    TotalPrice = 16,
                    OrderStatus = OrderStatus.Pending,
                }

            };


            var contextMock = new Mock<EntityDataModel>();
            var employeesMock = MockedDatabaseUtils.CreateDbSetMock(employees);
            var customersMock = MockedDatabaseUtils.CreateDbSetMock(customers);
            var customizedProductsMock = MockedDatabaseUtils.CreateDbSetMock(customizedProducts);
            var ordersMock = MockedDatabaseUtils.CreateDbSetMock(orders);
            contextMock.Setup(c => c.Employees).Returns(employeesMock.Object);
            contextMock.Setup(c => c.Customers).Returns(customersMock.Object);
            contextMock.Setup(c => c.CustomizedProducts).Returns(customizedProductsMock.Object);
            contextMock.Setup(c => c.Orders).Returns(ordersMock.Object);
            context = contextMock.Object;
            filter = new OrdersFilter(context);
        }

        [TestMethod]
        public void FindOrdersByChefTest()
        {
            EFEmployeeRepository emp = new EFEmployeeRepository(context);
            EFOrderRepository or = new EFOrderRepository(context);

            var chef = (from o in context.Orders
                        join e in context.Employees
                        on o.Chef.EmployeeId equals e.EmployeeId
                        select e).First();
            var newEmployee = new Employee()
            {
                Address = "Mieszkalna 123",
                BirthDate = DateTime.Parse("10/10/1950"),
                Category = null,
                Email = "john.smith@smiths.com",
                Name = "John Smith",
                Salary = 1234500,
                Telephone = "123-125-865",
                ProductCategoryCompetency = null,
                AvailableIntervals = null
            };

            emp.AddNewEmployee(newEmployee);

            var order = filter.FindOrdersByChef(chef);
            var noChef = filter.FindOrdersByChef(newEmployee);

            Assert.IsTrue(order != null, "Didn't find order");
            Assert.IsTrue(noChef.Count() == 0, "Find order with is not conected to chef");
        }

        [TestMethod]
        public void FindOrdersByCustomerTest()
        {
            EFCustomerRepository cr = new EFCustomerRepository(context);
            EFOrderRepository or = new EFOrderRepository(context);

            Customer customer = (from o in context.Orders
                                 join c in context.Customers
                                 on o.Customer.CustomerId equals c.CustomerId
                                 select c).First();

            var order = filter.FindOrdersByCustomer(customer);

            Assert.IsTrue(order != null, "Didn't find order");
        }

        [TestMethod]
        public void FindOrdersByDeliverymanTest()
        {
            EFEmployeeRepository emp = new EFEmployeeRepository(context);
            EFOrderRepository or = new EFOrderRepository(context);

            var deliveryman = (from o in context.Orders
                               join e in context.Employees
                               on o.Deliveryman.EmployeeId equals e.EmployeeId
                               select e).First();

            var order = filter.FindOrdersByDeliveryman(deliveryman);

            Assert.IsTrue(order != null, "Didn't find order");
        }

        [TestMethod]
        public void FindOrdersByPriceTest()
        {
            EFOrderRepository or = new EFOrderRepository(context);
            var orders = context.Orders.ToList();

            decimal maxPrce = orders.Max(o => o.TotalPrice);
            var order = filter.FindOrdersByPrice(maxPrce, maxPrce + 1);

            Assert.IsTrue(order.All(o => o.TotalPrice == maxPrce), "Wrong filter");
        }

        [TestMethod]
        public void FindOrdersByStatusTest()
        {
            EFOrderRepository or = new EFOrderRepository(context);

            var orders = context.Orders.ToList();


            var order = filter.FindOrdersByStatus(OrderStatus.Pending);

            Assert.IsTrue(order.Count() == orders.Where(o => o.OrderStatus == OrderStatus.Pending).Count(), "Wrong filter");
        }

        [TestMethod]
        public void FindOrdersByTime()
        {
            EFOrderRepository or = new EFOrderRepository(context);

            var orders = context.Orders.ToList();

            DateTime maxDate = orders.Max(o => o.OrderCreated);
            var order = filter.FindOrdersByTime(maxDate, DateTime.MaxValue);

            Assert.IsTrue(order.All(o => o.OrderCreated == maxDate), "Wrong filter");
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }
    }
}