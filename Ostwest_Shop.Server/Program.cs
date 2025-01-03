using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Ostwest_Shop.Server.DbContext;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;
using Ostwest_Shop.Server.Repository;
using Ostwest_Shop.Server.Services;

var builder = WebApplication.CreateBuilder(args);

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtIssuerOptions:key"])); 

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMagazineRepository, MagazineRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<DbContext, MyDbContext>();
builder.Services.AddDbContext<MyDbContext>(e =>
    e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("https://127.0.0.1:52826")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtIssuerOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtIssuerOptions:Audience"],
        IssuerSigningKey = signingKey
    };
});

builder.Services.Configure<JwtIssuerOptions>(options =>
{
    options.Issuer = builder.Configuration["JwtIssuerOptions:Issuer"];
    options.Audience = builder.Configuration["JwtIssuerOptions:Audience"];
    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularApp");

app.UseStaticFiles();
app.UseDefaultFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();