using Microsoft.EntityFrameworkCore;
using smartkantin.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DefaultMysqlDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("mysql1") ?? "";
    Console.WriteLine("conn str: " + connStr);
    options.UseMySQL(connStr);
});



// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OrderActionsBy(args => args.RelativePath);
});



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
