using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class Customer
    {
        public Customer() { }

        public Customer(Customer c)
        {
            CustomerId = c.CustomerId;
            Name = c.Name;
            Address = c.Address;
            Email = c.Email;
            Telephone = c.Telephone;
        }

        public int CustomerId { get; set; }

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
    }
}