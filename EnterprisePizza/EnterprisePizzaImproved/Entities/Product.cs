using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterprisePizzaImproved.Entities
{
    public class Product
    {
        public Product() { }

        public Product(Product p)
        {
            ProductId = p.ProductId;
            ProductCategory = p.ProductCategory;
            Title = p.Title;
            Allergens = p.Allergens;
            BasePrice = p.BasePrice;
            Description = p.Description;
            PrepareTime = p.PrepareTime;
        }

        public int ProductId { get; set; }

        public ProductCategory ProductCategory { get; set; }
        
        [Required(ErrorMessage = "Title can't be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price can't be empty")]
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be smaller than 0")]
        public decimal BasePrice { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "PrepareTime can't be empty")]
        public TimeSpan PrepareTime { get; set; }

        public ICollection<Allergen> Allergens { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }

    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
    }
}
