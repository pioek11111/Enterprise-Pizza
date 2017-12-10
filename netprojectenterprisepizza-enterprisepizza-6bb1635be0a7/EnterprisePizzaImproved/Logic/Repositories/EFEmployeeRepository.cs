using EnterprisePizzaImproved.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterprisePizzaImproved.Entities;
using System.Data.Entity;

namespace EnterprisePizzaImproved.Logic.Repositories
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private EntityDataModel DatabaseContext;

        public EFEmployeeRepository(EntityDataModel databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IEnumerable<Employee> EmployeeRepository
        {
            get
            {
                return DatabaseContext.Employees.Include("AvailableIntervals").ToList();

            }
        }

        public OperationResult AddNewEmployee(Employee employee)
        {
            if (DatabaseContext.Employees.All(e => e.EmployeeId != employee.EmployeeId))
            {
                DatabaseContext.Employees.Add(employee);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            else
            {
                return new OperationResult {IsSucceeded = false};
            }
        }

        public OperationResult ChangeEmployee(Employee oldEmployee, Employee newEmployee)
        {
            var employee = DatabaseContext.Employees.First(e => e.EmployeeId == oldEmployee.EmployeeId);
            employee.Address = newEmployee.Address;
            foreach (var timeInterval in oldEmployee.AvailableIntervals.ToList())
            {
                if (!newEmployee.AvailableIntervals.Any(ti => ti.EmployeeId == timeInterval.EmployeeId))
                {
                    DatabaseContext.TimeIntervals.Remove(timeInterval);
                }
            }

            foreach (var timeInterval in newEmployee.AvailableIntervals.ToList())
            {
                if (!oldEmployee.AvailableIntervals.Any(ti => ti.EmployeeId == timeInterval.EmployeeId))
                {
                    DatabaseContext.TimeIntervals.Add(timeInterval);
                }
            }
            employee.AvailableIntervals = newEmployee.AvailableIntervals;
            employee.BirthDate = newEmployee.BirthDate;
            employee.Category = newEmployee.Category;
            employee.Email = newEmployee.Email;
            employee.Name = newEmployee.Name;
            employee.ProductCategoryCompetency = newEmployee.ProductCategoryCompetency;
            employee.Salary = newEmployee.Salary;
            employee.Telephone = newEmployee.Telephone;
            DatabaseContext.SaveChanges();
            return new OperationResult { IsSucceeded = true };
        }

        public OperationResult RemoveEmployee(Employee employee)
        {
            try
            {
                var first = DatabaseContext.Employees.Single(e => e.EmployeeId == employee.EmployeeId);
                if (employee.AvailableIntervals != null)
                {
                    foreach (var ti in employee.AvailableIntervals.ToList())
                    {
                        DatabaseContext.TimeIntervals.Remove(ti);
                    }
                }
                DatabaseContext.Employees.Remove(first);
                DatabaseContext.SaveChanges();
                return new OperationResult {IsSucceeded = true};
            }
            catch (InvalidOperationException e)
            {
                return new OperationResult {IsSucceeded = false};
            }
        }
    }
}
