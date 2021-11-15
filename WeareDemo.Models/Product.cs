using System;


using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WeareDemo.Models
{
    public class Product 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

       
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(0.1,10000,ErrorMessage ="Cost can not be more than 10000")]
        public double Cost { get; set; }
        public double Price
        { get { return this.Cost * 1.21; } }

        [Required(ErrorMessage = "Type is required")]
        public ProductType Type { get; set; }

       
    }
}
