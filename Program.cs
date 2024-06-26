using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using smartkantin.Data;
using smartkantin.Middleware;
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
            string connStr = builder.Configuration.GetConnectionString("mysql1") ?? "";
            // Console.WriteLine("conn str: " + connStr);
            // options.UseMySQL(connStr);

            // sqlite
            // var folder = Environment.CurrentDirectory;
            // var path = Environment.GetFolderPath(folder);
            // var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            // var dbPath = System.IO.Path.Join(path, "smart-kantin.db");
            var dbPath = "./smart-kantin2.db";
            Console.WriteLine("sqlite db path: " + dbPath);
            options.UseSqlite("Data Source=" + dbPath);
        });

        // config identity / jwt
        // builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
        // {
        //     // option.Password.RequireDigit = true;
        //     // option.Password.RequireUppercase = true;
        //     // option.Password.RequireLowercase = true;
        //     // option.Password.RequireNonAlphanumeric = true;
        //     option.Password.RequiredLength = 5;
        // })
        // .AddEntityFrameworkStores<DefaultMysqlDbContext>();


        // var JWTSigningKey_ = builder.Configuration["JWT:SigningKey"];
        // if (JWTSigningKey_ == null)
        // {
        //     Console.WriteLine("jwt signing key is not found, using default...");
        // }
        // string JWTSigningKey = JWTSigningKey_ ?? "wkwkwkwk";
        // var JWTSigningKey = GetStringFromConfig(builder.Configuration, "JWT:SigningKey", "wJMRQjj4wNQM1frktTy4zNnRufmtuuJevxJx6acJ8nLai6m1p0cHAzN0SJJM1tSheWWeqgMeTZzBy3aTwqjchpNWZz7Cru8kETRVUzGHwrqF3ePNzUbTJCyr1NGjqFP3899RnKeLjqyc");

        // builder.Services.AddAuthentication(option =>
        // {
        //     option.DefaultAuthenticateScheme =
        //     option.DefaultChallengeScheme =
        //     option.DefaultForbidScheme =
        //     option.DefaultScheme =
        //     option.DefaultSignInScheme =
        //     option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        // })
        // .AddJwtBearer(option =>
        // {
        //     option.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         // ValidateIssuer = true,
        //         // ValidIssuer = builder.Configuration["JWT:Issuer"],
        //         // ValidateAudience = true,
        //         // ValidAudience = builder.Configuration["JWT:Audience"],
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = new SymmetricSecurityKey(
        //             System.Text.Encoding.UTF8.GetBytes(JWTSigningKey)
        //         )
        //     };
        // });

        // me-register repository
        builder.Services.AddScoped<IVendorRepository, VendorRepository>();
        builder.Services.AddScoped<IFoodRepository, FoodRepository>();
        builder.Services.AddScoped<IVendorOrderRepository, VendorOrderRepository>();
        builder.Services.AddScoped<ICustomerCartRepository, CustomerCartRepository>();
        builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
        builder.Services.AddScoped<IMyUserRepository, MyUserRepository>();
        builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();



        // register services
        builder.Services.AddScoped<ITokenService, TokenService>();

        // register middleware
        builder.Services.AddTransient<MyJwtAuthMiddleware>();


        // register policy-role untuk [Authorize]
        // builder.Services.AddAuthorization(opt => {
        //     opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));

        // });
        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = "forbidScheme";
            opt.DefaultForbidScheme = "forbidScheme";
            opt.AddScheme<MyAuthenticationHandler>("forbidScheme", "Handle Forbidden");
        });


        // daftarkan cors
        builder.Services.AddCors(opt => {
            opt.AddDefaultPolicy(b => 
                b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                
            );
        });

        // builder.Services.AddControllers();
        builder.Services.AddControllers().AddNewtonsoftJson(opt =>
        {
            // config agar serialisasi json mengabaikan adanya reference cycle
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
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
                    Array.Empty<string>()
                }
            });
        });
        // swagger dapat dibuka di /swagger


        // json serializer



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

        app.UseMiddleware<MyJwtAuthMiddleware>();

        app.UseCors();

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