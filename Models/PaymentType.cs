using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;


public class PaymentType
{
    public int Id { get; set; }
    public string? Name { get; set; }
}