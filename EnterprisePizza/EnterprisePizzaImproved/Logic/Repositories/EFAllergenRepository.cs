using EnterprisePizzaImproved.Abstract;
using EnterprisePizzaImproved.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    public class EFAllergenRepository : IAllergenRepository
    {
        private EntityDataModel DatabaseContext;

        public IEnumerable<Allergen> AllergenRepository => DatabaseContext.Allergens;

        public EFAllergenRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public OperationResult AddNewAllergen(Allergen allergen)
        {
            if (DatabaseContext.Allergens.All(al => al.AllergenId != allergen.AllergenId))
            {
                DatabaseContext.Allergens.Add(allergen);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            else
            {
                return new OperationResult {IsSucceeded = false};
            }
        }

        public OperationResult ChangeAllergen(Allergen oldAllergen, Allergen newAllergen)
        {
            var allergen = DatabaseContext.Allergens.First(p => p.AllergenId == oldAllergen.AllergenId);
            allergen.Description = newAllergen.Description;
            allergen.Title = newAllergen.Title;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveProductAllergen(Allergen allergen)
        {
            try
            {
                var first = DatabaseContext.Allergens.Single(a => a.AllergenId == allergen.AllergenId);
                DatabaseContext.Allergens.Remove(first);
                DatabaseContext.SaveChanges();
                return new OperationResult() {IsSucceeded = true};
            }
            catch (InvalidOperationException e)
            {
                return new OperationResult { IsSucceeded = false };
            }
        }
    }
}
