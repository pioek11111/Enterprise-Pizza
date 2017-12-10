using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class Employee
    {
        public Employee() { }
        
        public Employee(Employee other)
        {
            EmployeeId = other.EmployeeId;
            Name = other.Name;
            Address = other.Address;
            Email = other.Email;
            Telephone = other.Telephone;
            BirthDate = other.BirthDate;
            Salary = other.Salary;
            AvailableIntervals = other.AvailableIntervals.Select(ti => new TimeInterval(ti)).ToList();
        }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address can't be empty")]
        public string Address { get; set; }

        [EmailAddress(ErrorMessage = "It's not valid email address"),
         Required(ErrorMessage = "Email can't be empty")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "It's not valid phone number"),
         Required(ErrorMessage = "Telephone can't be empty")]
        public string Telephone { get; set; }
        
        public decimal Salary { get; set; }
        public EmployeeCategory Category { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<TimeInterval> AvailableIntervals { get; set; }
        public ICollection<ProductCategory> ProductCategoryCompetency { get; set; }
    }

    public class EmployeeCategory
    {
        public int EmployeeCategoryId { get; set; }
        public string Title { get; set; }
    }

}
