using System;
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int PaymentTypeId { get; set; }
    public bool OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
}