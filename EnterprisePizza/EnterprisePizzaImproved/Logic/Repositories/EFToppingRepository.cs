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
    public class EFToppingRepository : IToppingRepository
    {
        private EntityDataModel DatabaseContext;

        public EFToppingRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<Topping> ToppingRepository => DatabaseContext.Toppings.Include("ToppingCategory").Include("Allergens");

        public OperationResult AddNewTopping(Topping topping)
        {
            if (DatabaseContext.Toppings.All(t => t.ToppingId != topping.ToppingId))
            {
                DatabaseContext.Toppings.Add(topping);
                DatabaseContext.SaveChanges();
                return new OperationResult { IsSucceeded = true };
            }
            else
            {
                return new OperationResult { IsSucceeded = false };
            }
        }

        public OperationResult ChangeTopping(Topping oldTopping, Topping newTopping)
        {
            var existingTopping = DatabaseContext.Toppings.First(t => t.ToppingId == oldTopping.ToppingId);

            var newToppingAllergensIds = newTopping.Allergens.Select(al => al.AllergenId);
            var newAllergens = DatabaseContext.Allergens
                .Where(al => newToppingAllergensIds.Contains(al.AllergenId)).ToList();
            existingTopping.Allergens.Clear();
            foreach (var al in newAllergens)
            {
                existingTopping.Allergens.Add(al);
            }
            existingTopping.PrepareTime = newTopping.PrepareTime;
            existingTopping.Price = newTopping.Price;
            existingTopping.Title = newTopping.Title;
            existingTopping.ToppingCategory = newTopping.ToppingCategory;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveTopping(Topping topping)
        {
            try
            {
                var first = DatabaseContext.Toppings.Single(t => t.ToppingId == topping.ToppingId);
                DatabaseContext.Toppings.Remove(first);
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
