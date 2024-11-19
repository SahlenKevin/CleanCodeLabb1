namespace WebShop;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; }
    public IEnumerable<Product> Products { get; set; }
}