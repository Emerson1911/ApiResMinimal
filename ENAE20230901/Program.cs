var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var product = new List<Product>();

app.MapGet("/products", () =>
{
    return product;
});

app.MapGet("/products/{id}", (int id) =>
{
    var client = product.FirstOrDefault(c => c.id == id);
    return client;
});

app.MapPost("/products", (Product product1) =>
{
    product.Add(product1);
    return Results.Ok();
});

app.MapPut("/products/{id}", (int id, Product client) =>
{
    var existingProduct = product.FirstOrDefault(client => client.id == id);
    if (existingProduct != null)
    {
        existingProduct.name = client.name;
        existingProduct.description = client.description;
        existingProduct.price = client.price;
        existingProduct.quantity = client.quantity;
        existingProduct.expirate = client.expirate;
        return Results.Ok();
    }
    else
    {
        return Results.Ok();
    }
});

app.MapDelete("/products/{id}", (int id) =>
{
    var existingProduct = product.FirstOrDefault(product => product.id == id);
    if (existingProduct != null)
    {
        product.Remove(existingProduct);
        return Results.Ok();
    }
    else { return Results.Ok(); }
});
app.Run();

internal class Product
{
    
    public int id {  get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public int quantity { get; set; }
    public DateTime expirate { get; set; }
}