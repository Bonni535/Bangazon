using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool isSeller { get; set; }
}