using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class Allergen
    {
        public Allergen() { }

        public Allergen(Allergen other)
        {
            AllergenId = other.AllergenId;
            Title = other.Title;
            Description = other.Description;
        }

        public int AllergenId { get; set; }

        [Required(ErrorMessage = "Title can't be empty")]
        public string Title { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public ICollection<Product> Products { get; set; }
        public ICollection<Topping> Toppings { get; set; }
    }
}