using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int IdProduct { get; set; }
        [Required(ErrorMessage ="Ime proizvoda je obavezno")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Cena je obavezna")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Broj proizvoda je obavezan")]
        public int Count { get; set; }
        public int IdCategory { get; set; }
    }
}
