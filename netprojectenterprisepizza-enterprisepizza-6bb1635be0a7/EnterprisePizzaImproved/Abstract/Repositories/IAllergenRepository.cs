using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IAllergenRepository
    {
        IEnumerable<Allergen> AllergenRepository { get; }

        OperationResult AddNewAllergen(Allergen allergen);

        OperationResult ChangeAllergen(Allergen oldAllergen, Allergen newAllergen);

        OperationResult RemoveProductAllergen(Allergen allergen);
    }
}
