using Microsoft.EntityFrameworkCore;
using WebShop.Notifications;
using WebShop.Repository;
using WebShop.Services;
using WebShop.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Registrera Unit of Work i DI-container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<INotificationObserver, EmailNotification>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = "Server=.;Database=CleanCodeLabbEtt;Trusted_Connection=True;TrustServerCertificate=True;";
// "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CleanCodeLabbEtt;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"

builder.Services.AddDbContext<WebShopDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();


// Apply migrations on startup
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<WebShopDbContext>();
//     dbContext.Database.Migrate();
// }

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
