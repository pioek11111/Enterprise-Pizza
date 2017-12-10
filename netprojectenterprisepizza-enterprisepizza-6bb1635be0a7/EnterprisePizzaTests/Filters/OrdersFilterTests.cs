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

namespace EnterprisePizzaTests.Filters
{
    /// <summary>
    /// Summary description for OrdersFilterTests
    /// </summary>
    [TestClass]
    public class OrdersFilterTests
    {
        private EntityDataModel context;

        private IOrdersFilter filter;

        public object EFEmployeeRepository { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            context = new EntityDataModel();
            filter = new OrdersFilter(context);
            DatabaseFiller.ClearDatabase(context);
            DatabaseFiller.FillWithData(context);
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

            var orders = or.OrderRepository.ToList(); 

            decimal maxPrce = orders.Max(o => o.TotalPrice);
            var order = filter.FindOrdersByPrice(maxPrce, maxPrce + 1);

            Assert.IsTrue(order.All(o => o.TotalPrice == maxPrce), "Wrong filter");
        }

        [TestMethod]
        public void FindOrdersByStatusTest()
        {
            EFOrderRepository or = new EFOrderRepository(context);

            var orders = or.OrderRepository.ToList();

            
            var order = filter.FindOrdersByStatus(OrderStatus.Pending);

            Assert.IsTrue(order.Count() == orders.Where(o => o.OrderStatus == OrderStatus.Pending).Count(), "Wrong filter");
        }

        [TestMethod]
        public void FindOrdersByTime()
        {
            EFOrderRepository or = new EFOrderRepository(context);

            var orders = or.OrderRepository.ToList();

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