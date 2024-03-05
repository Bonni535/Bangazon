using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;

public class Category
{
    public int Id { get; set; }
    public string? Title { get; set; }
}