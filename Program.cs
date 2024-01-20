

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoreApi.Domain.Entities;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Repositories;
using StoreApi.Domain.UnitOfWork;
using StoreApi.Security.Token;
using StoreApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITokenHandler, TokenHandler>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerOrderRepository, CustomerOrderRepository>();


builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

builder.Services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtbearerOptions =>
{
    IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
    var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

    jwtbearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime= true,
        ValidateIssuerSigningKey = true,
        ValidIssuer= tokenOptions.Issuer,
        ValidAudience= tokenOptions.Audience,
        IssuerSigningKey=SignHandler.GetSecurityKey(tokenOptions.SecurityKey),
        ClockSkew=TimeSpan.Zero
    };
});

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    options.UseSqlServer(connectionString);

    //options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


