using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface ICustomizedProductRepository
    {
        IEnumerable<CustomizedProduct> CustomizedProductRepository { get; }

        OperationResult AddNewCustomizedProduct(CustomizedProduct cp);

        OperationResult ChangeCustomizedProduct(CustomizedProduct oldCustomizedProduct, CustomizedProduct newCustomizedProduct);

        OperationResult RemoveCustomizedProduct(CustomizedProduct cp);
    }
}
