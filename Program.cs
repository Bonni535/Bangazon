using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Builder;


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
   
        db.Orders.Add(newOrd);
        db.SaveChanges();
        return Results.Created($"/api/orders/{newOrd.Id}", newOrd);
});

//Get All Orders From a Single User
app.MapGet("/api/order/by-user", (BangazonDbContext db, int userId) =>
{
    var allOrdersForASingleUser = db.Orders.Where(o => o.CustomerId == userId).ToList();

    if (allOrdersForASingleUser.Any())
    {
        return Results.NotFound();
    }
    return Results.Ok(allOrdersForASingleUser);
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

//Update an Order
app.MapPut("/api/orders/{id}", (BangazonDbContext db, Order order, int id) =>
{
    var updateOrder = db.Orders.SingleOrDefault(o => o.Id == id);
    if (updateOrder == null)
    {
        return Results.NotFound();
    }
    updateOrder.PaymentTypeId = order.PaymentTypeId;
    updateOrder.IsCompleted = order.IsCompleted;
    updateOrder.OrderDate = order.OrderDate;
    db.SaveChanges();
    return Results.Ok(updateOrder);

});

//Delete a Product From an Order
app.MapDelete("/api/orderProducts/{id}", (BangazonDbContext db, int id) =>
{
    OrderProducts orderProductToDelete = db.OrderProducts.SingleOrDefault(orderProductToDelete => orderProductToDelete.Id == id);
    if (orderProductToDelete == null)
    {
        return Results.NotFound();
    }
    db.OrderProducts.Remove(orderProductToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

//Create a New User
app.MapPost("/api/users", (BangazonDbContext db, User newUser) =>
{
    db.Users.Add(newUser);
    db.SaveChanges();
    return Results.Created($"/api/users/{newUser.Id}", newUser);
});

//Get All the Categories
app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

//Get All Products For A Single Category
app.MapGet("/api/products/by-category", (BangazonDbContext db, int categoryId) =>
{
    var productsFilteredByCategory = db.Products.Where(p => p.CategoryId == categoryId).ToList();

    //If a Product doesn't match
    if (!productsFilteredByCategory.Any())
    {
        return Results.NotFound("Unfortunately there are no Products available for this Category.");
    }

    return Results.Ok(productsFilteredByCategory);
});
app.Run();

