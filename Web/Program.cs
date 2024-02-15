using Microsoft.EntityFrameworkCore;
using Web.Areas.Account.Services;
using Web.Areas.Cart.Services;
using Web.Areas.Product.Services;
using Web.EF;
using Web.EF.Seeders;
using Web.Mapper;

var corsPolicy = "frontend";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddDbContext<BookStoreDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Services.AddControllers().AddNewtonsoftJson();;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProducts();
builder.Services.AddCart();
builder.Services.AddAccount(builder.Configuration.GetSection("Auth"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var bookStoreSeeder = new BookStoreSeeder();
await bookStoreSeeder.SeedAsync(app, builder);

app.Run();