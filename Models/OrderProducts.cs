using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;

public class OrderProducts
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductsId { get; set; }
}