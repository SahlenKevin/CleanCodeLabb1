using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product
    {
        [Key]
        public int Id { get; set; } // Unikt ID f�r produkten
        [JsonPropertyName("name")]
        public string Name { get; set; } // Namn p� produkten
    }
}