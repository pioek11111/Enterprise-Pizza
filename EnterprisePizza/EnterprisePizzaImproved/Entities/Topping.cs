using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class Topping
    {
        public Topping() { }

        public Topping(Topping t)
        {
            ToppingId = t.ToppingId;
            ToppingCategory = t.ToppingCategory;
            Title = t.Title;
            Price = t.Price;
            PrepareTime = t.PrepareTime;
            Allergens = t.Allergens;
        }

        public int ToppingId { get; set; }

        public ToppingCategory ToppingCategory { get; set; }
        
        [Required(ErrorMessage = "Title can't be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price can't be empty")]
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be smaller than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Prepare time can't be empty")]
        public TimeSpan PrepareTime { get; set; }

        public ICollection<Allergen> Allergens { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }

    public class ToppingCategory
    {
        public int ToppingCategoryId { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public CategorySelection CategorySelection { get; set; }
        public Obligation Obligation { get; set; }
    }

    public enum CategorySelection
    {
        Multiple,
        Single
    }

    public enum Obligation
    {
        Obligatory,
        Optional
    }
}
