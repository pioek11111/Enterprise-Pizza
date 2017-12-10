using System;
using System.Collections.Generic;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved.DatabaseFilling
{
    public class DatabaseFiller
    {
        public static void ClearDatabase(EntityDataModel model)
        {
            model.Allergens.RemoveRange(model.Allergens);
            model.Customers.RemoveRange(model.Customers);
            model.CustomizedProducts.RemoveRange(model.CustomizedProducts);
            model.Orders.RemoveRange(model.Orders);
            model.Employees.RemoveRange(model.Employees);
            model.EmployeeCategories.RemoveRange(model.EmployeeCategories);
            model.TimeIntervals.RemoveRange(model.TimeIntervals);
            model.Toppings.RemoveRange(model.Toppings);
            model.ToppingCategories.RemoveRange(model.ToppingCategories);
            model.Products.RemoveRange(model.Products);
            model.ProductCategories.RemoveRange(model.ProductCategories);
            model.SaveChanges();
        }

        public static void FillWithData(EntityDataModel model)
        {
            // allergens
            var meat = new Allergen()
            {
                Title = "Meat",
                Description = "Dangerous to vegans"
            };

            var gluten = new Allergen()
            {
                Title = "Gluten",
                Description = "Dangerous to sensitive people"
            };

            var cheese = new Allergen()
            {
                Title = "Cheese",
                Description = "Dangerous diary product"
            };

            var corn = new Allergen()
            {
                Title = "Corn",
                Description = "Dangerous diary product"
            };

            // product categories
            var pizzaCategory = new ProductCategory()
            {
                Name = "Pizza"
            };
            var burgerCategory = new ProductCategory()
            {
                Name = "Burger"
            };

            // topping categories
            var doughCategory = new ToppingCategory()
            {
                Title = "Dough",
                Prompt = "Please select exactly one dough",
                CategorySelection = CategorySelection.Single,
                Obligation = Obligation.Obligatory,
                ProductCategory = pizzaCategory,
            };

            var extrasPizzaCategory = new ToppingCategory()
            {
                Title = "Extras",
                Prompt = "Please select an extra topping for your pizza",
                CategorySelection = CategorySelection.Multiple,
                Obligation = Obligation.Optional,
                ProductCategory = pizzaCategory,
            };

            var extrasBurgerCategory = new ToppingCategory()
            {
                Title = "Extras",
                Prompt = "Please select an extra topping for your burger",
                CategorySelection = CategorySelection.Multiple,
                Obligation = Obligation.Optional,
                ProductCategory = burgerCategory,
            };

            // toppings
            var salami = new Topping()
            {
                Title = "Salami",
                PrepareTime = TimeSpan.FromSeconds(10),
                ToppingCategory = extrasPizzaCategory,
                Price = 2,
                Allergens = new List<Allergen> { meat, gluten }
            };

            var patty = new Topping()
            {
                Title = "Grilled Patty",
                PrepareTime = TimeSpan.FromSeconds(130),
                ToppingCategory = extrasBurgerCategory,
                Price = 11,
                Allergens = new List<Allergen> { meat }
            };

            var thinDough = new Topping()
            {
                Title = "Thin dough",
                PrepareTime = TimeSpan.FromSeconds(480),
                ToppingCategory = doughCategory,
                Price = 8,
                Allergens = new List<Allergen> { gluten }
            };

            var thickDough = new Topping()
            {
                Title = "Thick dough",
                PrepareTime = TimeSpan.FromSeconds(590),
                ToppingCategory = doughCategory,
                Price = 13,
                Allergens = new List<Allergen> { gluten }
            };

            // products

            var margheritta = new Product()
            {
                Title = "Margheritta Pizza Italiano",
                Allergens = new List<Allergen> { cheese, gluten },
                BasePrice = 10,
                Description = "Delicious and original italian pizza",
                PrepareTime = TimeSpan.FromSeconds(110),
                ProductCategory = pizzaCategory,
            };

            var hawaii = new Product()
            {
                Title = "Hawaii Pizza",
                Allergens = new List<Allergen> { corn },
                BasePrice = 13,
                Description = "Astonishing hawaii pizza with a twist",
                PrepareTime = TimeSpan.FromSeconds(140),
                ProductCategory = pizzaCategory,
            };

            var cheeseBurger = new Product()
            {
                Title = "Cheese Burger",
                Allergens = new List<Allergen> {  },
                BasePrice = 5,
                Description = "Large burger with cheese",
                PrepareTime = TimeSpan.FromSeconds(55),
                ProductCategory = burgerCategory,
            };

            // customers

            var pocztaPolska = new Customer()
            {
                Name = "Poczta Polska",
                Address = "Centrum 123",
                Email = "kontakt@pocztapolska.gov.pl",
                Telephone = "123 456 789"
            };

            var google = new Customer()
            {
                Name = "Google Polska",
                Address = "Koszykowa 55555",
                Email = "spam@google.pl",
                Telephone = "999 888 777"
            };

            var ted = new Customer()
            {
                Name = "TED Polska",
                Address = "Hoża tedowska",
                Email = "food@ted.com",
                Telephone = "+48 999-444-312"
            };

            // employee categories

            var chefCategory = new EmployeeCategory()
            {
                Title = "Chef"
            };

            var deliverymanCategory = new EmployeeCategory()
            {
                Title = "Deliveryman"
            };

            // intervals

            var monday = new TimeInterval()
            {
                From = DateTime.Parse("13/11/2017 8:00"),
                To = DateTime.Parse("13/11/2017 18:00"),
            };

            var wednesday = new TimeInterval()
            {
                From = DateTime.Parse("15/11/2017 10:00"),
                To = DateTime.Parse("15/11/2017 19:00"),
            };

            var thursday = new TimeInterval()
            {
                From = DateTime.Parse("16/11/2017 6:00"),
                To = DateTime.Parse("16/11/2017 12:00"),
            };

            // employees

            var chef1 = new Employee()
            {
                Address = "Mieszkalna 123",
                BirthDate = DateTime.Parse("10/10/1950"),
                Category = chefCategory,
                Email = "john.smith@smiths.com",
                Name = "John Smith",
                Salary = 1234500,
                Telephone = "123-125-865",
                ProductCategoryCompetency = new List<ProductCategory> { pizzaCategory },
                AvailableIntervals = new List<TimeInterval> { monday, wednesday, thursday }
            };

            var chef2 = new Employee()
            {
                Address = "Wypiekalna 123",
                BirthDate = DateTime.Parse("10/10/1980"),
                Category = chefCategory,
                Email = "bezier@adams.com",
                Name = "Adam Bezier",
                Salary = 4624500,
                Telephone = "437-355-124",
                ProductCategoryCompetency = new List<ProductCategory> { pizzaCategory, burgerCategory },
                AvailableIntervals = new List<TimeInterval> { monday, thursday }
            };

            var deliveryman1 = new Employee()
            {
                Address = "Dostarczalna 3",
                BirthDate = DateTime.Parse("10/10/1990"),
                Category = deliverymanCategory,
                Email = "alfred@courier.edu.com",
                Name = "Alfred Courier",
                Salary = 1224500,
                Telephone = "214-514-243",
                AvailableIntervals = new List<TimeInterval> { monday, wednesday, thursday }
            };

            // customized products

            var customHawaii = new CustomizedProduct()
            {
                BaseProduct = hawaii,
                CustomerWish = "mniej ananasów poproszę",
                Toppings = new List<Topping> { thinDough, salami },
            };

            var customBurger1 = new CustomizedProduct()
            {
                BaseProduct = cheeseBurger,
                CustomerWish = "mniej tłuszczu poproszę",
                Toppings = new List<Topping> { patty },
            };

            var customBurger2 = new CustomizedProduct()
            {
                BaseProduct = cheeseBurger,
                CustomerWish = "więcej tłuszczu poproszę",
                Toppings = new List<Topping> { patty },
            };

            // orders

            var order1 = new Order()
            {
                Chef = chef1,
                Deliveryman = deliveryman1,
                CookingDeadline = DateTime.Parse("15/11/2017 9:00"),
                CustomerWish = "Nie takie przypieczone",
                Customer = google,
                DeliveryDeadline = DateTime.Parse("15/11/2017 9:30"),
                OrderCreated = DateTime.Parse("2/11/2017 19:00"),
                OrderedProducts = new List<CustomizedProduct> { customHawaii, customBurger1 },
                TotalPrice = 23 + 16,
                OrderStatus = OrderStatus.Pending,
            };

            var order2 = new Order()
            {
                Chef = chef2,
                Deliveryman = deliveryman1,
                CookingDeadline = DateTime.Parse("15/11/2017 13:00"),
                CustomerWish = "Poproszę bez sera",
                Customer = pocztaPolska,
                DeliveryDeadline = DateTime.Parse("15/11/2017 13:30"),
                OrderCreated = DateTime.Parse("2/11/2017 14:00"),
                OrderedProducts = new List<CustomizedProduct> { customBurger2 },
                TotalPrice = 16,
                OrderStatus = OrderStatus.Pending,
            };

            model.Allergens.AddRange(new[] { meat, gluten, cheese });
            model.ProductCategories.AddRange(new[] { pizzaCategory, burgerCategory });
            model.ToppingCategories.AddRange(new[] { doughCategory, extrasPizzaCategory, extrasBurgerCategory });
            model.Toppings.AddRange(new[] { salami, patty, thinDough, thickDough });
            model.Products.AddRange(new[] { margheritta, hawaii, cheeseBurger });
            model.Customers.AddRange(new[] { pocztaPolska, google, ted });
            model.EmployeeCategories.AddRange(new[] { chefCategory, deliverymanCategory });
            model.Employees.AddRange(new[] { chef1, chef2, deliveryman1 });
            model.CustomizedProducts.AddRange(new[] { customHawaii, customBurger1, customBurger2 });
            model.Orders.AddRange(new[] { order1, order2 });
            model.SaveChanges();
        }
    }
}
