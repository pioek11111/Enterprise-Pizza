using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Abstract
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> EmployeeRepository { get; }

        OperationResult AddNewEmployee(Employee employee);

        OperationResult ChangeEmployee(Employee oldEmployee, Employee newEmployee);

        OperationResult RemoveEmployee(Employee employee);
    }
}
