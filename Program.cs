using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Repository;
using smartkantin.Repository.Impl;

var builder = WebApplication.CreateBuilder(args);

// me-register dbcontext
builder.Services.AddDbContext<DefaultMysqlDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("mysql1") ?? "";
    Console.WriteLine("conn str: " + connStr);
    options.UseMySQL(connStr);
});

// me-register repository
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IMyUserRepository, MyUserRepository>();



// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OrderActionsBy(args => args.RelativePath);
});
// swagger dapat dibuka di /swagger


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
