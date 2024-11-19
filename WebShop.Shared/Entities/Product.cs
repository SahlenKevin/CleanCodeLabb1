using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product
    {
        [Key]
        public int Id { get; set; } // Unikt ID för produkten
        [JsonPropertyName("name")]
        public string Name { get; set; } // Namn på produkten
    }
}