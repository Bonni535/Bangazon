using System;
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;

public class Products
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal PriceUnit { get; set; }
    public int CategoryId { get; set; }
    public DateTime TimePosted { get; set; }
    public int SellerId { get; set; }
}