using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IToppingRepository
    {
        IEnumerable<Topping> ToppingRepository { get; }

        OperationResult AddNewTopping(Topping topping);

        OperationResult ChangeTopping(Topping oldTopping, Topping newTopping);

        OperationResult RemoveTopping(Topping topping);
    }
}
