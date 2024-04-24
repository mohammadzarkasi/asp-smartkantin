using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using smartkantin.Data;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Repository.Impl;
using smartkantin.Service;
using smartkantin.Service.Impl;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // me-register dbcontext
        builder.Services.AddDbContext<DefaultMysqlDbContext>(options =>
        {
            var connStr = builder.Configuration.GetConnectionString("mysql1") ?? "";
            Console.WriteLine("conn str: " + connStr);
            options.UseMySQL(connStr);
        });

        // config identity / jwt
        builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
        {
            option.Password.RequireDigit = true;
            option.Password.RequireUppercase = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireNonAlphanumeric = true;
            option.Password.RequiredLength = 5;
        })
        .AddEntityFrameworkStores<DefaultMysqlDbContext>();


        // var JWTSigningKey_ = builder.Configuration["JWT:SigningKey"];
        // if (JWTSigningKey_ == null)
        // {
        //     Console.WriteLine("jwt signing key is not found, using default...");
        // }
        // string JWTSigningKey = JWTSigningKey_ ?? "wkwkwkwk";
        var JWTSigningKey = GetStringFromConfig(builder.Configuration, "JWT:SigningKey", "wJMRQjj4wNQM1frktTy4zNnRufmtuuJevxJx6acJ8nLai6m1p0cHAzN0SJJM1tSheWWeqgMeTZzBy3aTwqjchpNWZz7Cru8kETRVUzGHwrqF3ePNzUbTJCyr1NGjqFP3899RnKeLjqyc");

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme =
            option.DefaultChallengeScheme =
            option.DefaultForbidScheme =
            option.DefaultScheme =
            option.DefaultSignInScheme =
            option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(JWTSigningKey)
                )
            };
        });

        // me-register repository
        builder.Services.AddScoped<IVendorRepository, VendorRepository>();
        builder.Services.AddScoped<IFoodRepository, FoodRepository>();
        builder.Services.AddScoped<IVendorOrderRepository, VendorOrderRepository>();
        // builder.Services.AddScoped<IMyUserRepository, MyUserRepository>();


        builder.Services.AddScoped<ITokenService, TokenService>();


        // Add services to the container.



        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.OrderActionsBy(args => args.RelativePath);

            // konfig agar swagger bisa menggunakan jwt
            opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "API Smart Kantin",
                Version = "v1"
            });
            opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
        // swagger dapat dibuka di /swagger


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                // opt.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                opt.EnablePersistAuthorization();
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    private static string GetStringFromConfig(ConfigurationManager configuration, string key, string defaultValue)
    {
        var r = configuration[key];
        if (r != null)
        {
            return r;
        }
        return defaultValue;
    }
}