using Microsoft.EntityFrameworkCore;
using Bangazon.Models;

public class BangazonDbContext : DbContext
{

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<PaymentType>? PaymentTypes { get; set; }
    public DbSet<OrderProducts>? OrderProducts { get; set; }

    public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed data with campsite types
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category {
                Id = 1,
                Title = "Bath"
            },

            new Category {
                Id = 2,
                Title = "Bedroom"
            },
        });

        modelBuilder.Entity<Order>().HasData(new Order[] {
            new Order {
                Id = 1,
                CustomerId = 1,
                PaymentTypeId = 1,
                OrderStatus = true,
                OrderDate = new DateTime(2024, 1, 17)
            },

            new Order {
                Id = 2,
                CustomerId = 1,
                PaymentTypeId = 2,
                OrderStatus = false,
                OrderDate = new DateTime(2024, 2, 8)
            }
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[] {
            new PaymentType {
                Id = 1,
                Name = "Cash"
            },

            new PaymentType {
                Id = 2,
                Name = "Credit Card"
            }
        });

        modelBuilder.Entity<Product>().HasData(new Product[] {

            new Product {
                Id = 1,
                Title = "Bidet",
                Description = "A nice and tiny Bidet",
                Quantity = 50,
                PriceUnit = 200.00M,
                CategoryId = 1,
                TimePosted = new DateTime(2022, 2, 28),
                SellerId = 1
            },

            new Product {
                Id = 2,
                Title = "Bedside Lamp",
                Description = "The Cord, Socket and Plug of Aooshine bedside table lamp are UL listed. By giving products the listed, you don’t have to worry about the material problem. This Nightstand lamp comes with ON/OFF switch control, easy to install and use. Please note not included bulb. Please use the LED bulb only.",
                Quantity = 6,
                PriceUnit = 98.00M,
                CategoryId = 1,
                TimePosted = new DateTime(2022, 8, 26),
                SellerId = 2
            }
        });

        modelBuilder.Entity<User>().HasData(new User[] {

            new User {
                Id = 1,
                Name = "John Carpenter",
                Email = "CarpJohn@gmail.com",
                IsSeller = true
            },

            new User {
                Id = 2,
                Name = "Zoe Sandal",
                Email = "itsnotzoesaldana@gmail.com",
                IsSeller = true
            }
        });

        modelBuilder.Entity<OrderProducts>().HasData(new OrderProducts[] {

            new OrderProducts {
                Id = 1,
                OrderId = 1,
                ProductId = 1
            },

            new OrderProducts {
                Id = 2,
                OrderId = 1,
                ProductId = 2
            }
        });
    }
}