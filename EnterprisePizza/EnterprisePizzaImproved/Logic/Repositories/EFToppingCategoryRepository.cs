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
    public class EFToppingCategoryRepository : IToppingCategoryRepository
    {
        private EntityDataModel DatabaseContext;

        public EFToppingCategoryRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<ToppingCategory> ToppingCategoryRepository => DatabaseContext.ToppingCategories;

        public OperationResult AddNewToppingCategory(ToppingCategory toppingCategory)
        {
            if (DatabaseContext.ToppingCategories.All(tc => tc.ToppingCategoryId != toppingCategory.ToppingCategoryId))
            {
                DatabaseContext.ToppingCategories.Add(toppingCategory);
                DatabaseContext.SaveChanges();
                return new OperationResult { IsSucceeded = true };
            }
            else
            {
                return new OperationResult { IsSucceeded = false };
            }
        }

        public OperationResult ChangeToppingCategory(ToppingCategory oldToppingCategory, ToppingCategory newToppingCategory)
        {
            var existingToppingCategory = DatabaseContext.ToppingCategories.Single(pc => pc.ToppingCategoryId ==
                                                                                         oldToppingCategory.ToppingCategoryId);
            existingToppingCategory.ProductCategory = newToppingCategory.ProductCategory;
            existingToppingCategory.CategorySelection = newToppingCategory.CategorySelection;
            existingToppingCategory.Obligation = newToppingCategory.Obligation;
            existingToppingCategory.Prompt = newToppingCategory.Prompt;
            existingToppingCategory.Title = newToppingCategory.Title;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveToppingCategory(ToppingCategory toppingCategory)
        {
            try
            {
                var first = DatabaseContext.ToppingCategories.Single(tc => tc.ToppingCategoryId == toppingCategory.ToppingCategoryId);
                DatabaseContext.ToppingCategories.Remove(first);
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
