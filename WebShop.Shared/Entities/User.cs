using System.ComponentModel.DataAnnotations;

namespace WebShop;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
}