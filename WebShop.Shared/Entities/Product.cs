using System.ComponentModel.DataAnnotations;

namespace WebShop
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}