using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IToppingCategoryRepository
    {
        IEnumerable<ToppingCategory> ToppingCategoryRepository { get; }

        OperationResult AddNewToppingCategory(ToppingCategory toppingCategory);

        OperationResult ChangeToppingCategory(ToppingCategory oldToppingCategory, ToppingCategory newToppingCategory);

        OperationResult RemoveToppingCategory(ToppingCategory toppingCategory);
    }
}
