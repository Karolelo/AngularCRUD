using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.DbContext;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<DbContext, MyDbContext>();
builder.Services.AddDbContext<MyDbContext>(e=>e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", // Give your policy a name
        builder =>
        {
            builder.WithOrigins("https://127.0.0.1:52826") // Your Angular app's origin
                .AllowAnyMethod()
                .AllowAnyHeader(); 
        });
});

var app = builder.Build();
app.UseCors("AllowAngularApp");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
