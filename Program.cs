using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Get All the Users
app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

//Get a Single User
app.MapGet("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    var userId = db.Users.FirstOrDefault(u => u.Id == id);

    if (userId == null)
    {
        return Results.NotFound("User Not Found.");
    }

    return Results.Ok(userId);
});

//Get All Products
app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

//Get a Single Product
app.MapGet("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    var productID = db.Products.FirstOrDefault(p => p.Id == id);

    if (productID == null)
    {
        return Results.NotFound("Product Not Found.");
    }

    return Results.Ok(productID);
});

//Create a New Order
app.MapPost("/api/orders", (BangazonDbContext db, Order newOrd) =>
{
    try
    {
        db.Orders.Add(newOrd);
        db.SaveChanges();
        return Results.Created($"/api/orders/{newOrd.Id}", newOrd);
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});

//Get All Orders
app.MapGet("/api/orders", (BangazonDbContext db) =>
{
    return db.Orders.ToList();
});

//Get a Single Order
app.MapGet("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    var orderID = db.Orders.FirstOrDefault(o => o.Id == id);

    if (orderID == null)
    {
        return Results.NotFound("Order Not Found.");
    }

    return Results.Ok(orderID);
});
app.Run();

