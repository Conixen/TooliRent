using Microsoft.EntityFrameworkCore;
using TooliRent.Infrastructure.Data;
using TooliRent.Core.Interfaces;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Infrastructure.Repositories;
using TooliRent.Core.Interfaces.IService;
using TooliRent.Services.Service;
using TooliRent.Mapping;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using TooliRent.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace TooliRent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // DbContexts
            builder.Services.AddDbContext<TooliRentContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IToolRepository, ToolRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

            // Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthService, UserService>();
            builder.Services.AddScoped<IToolService, ToolService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

            // AutoMapper

            //builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));
            //builder.Services.AddAutoMapper(typeof(AuthMappingProfile));
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryMappingProfile>();
                cfg.AddProfile<AuthMappingProfile>();
                cfg.AddProfile<ToolMappingProfile>();
                cfg.AddProfile<ReservationMappingProfile>();
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Write in yopur JWT Token"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            // JWT
            var jwt = builder.Configuration.GetSection("Jwt");      // GetSection returnerar allt i "Jwt" i appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])); // Skapar en ny SymmetricSecurityKey med hj�lp av "Key" i appsettings.json

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,                  // kollar s� att den som skickar token �r den vi litar p�
                        ValidateAudience = true,                // kollar s� att den som tar emot token �r den vi litar p�
                        ValidateIssuerSigningKey = true,        // kollar s� att signeringsnyckeln �r korrekt
                        ValidateLifetime = true,                // kollar s� att token inte har g�tt ut
                        ValidIssuer = jwt["Issuer"],            // kollar s� att den som skickar token �r den vi litar p�
                        ValidAudience = jwt["Audience"],        // kollar s� att den som tar emot token �r den vi litar p�
                        IssuerSigningKey = key,                 // kollar s� att signeringsnyckeln �r korrekt
                        ClockSkew = TimeSpan.FromMinutes(1)     // tiden som token �r giltig efter att den har g�tt ut
                    };
                });

            builder.Services.AddAuthorization();                // Beh�vs f�r att kunna anv�nda [Authorize] i controllers

            builder.Services.AddControllers().AddNewtonsoftJson();          // F�r att hhtp Patch att funka i och anv�nda i Swagger UI

            builder.Services.AddSwaggerGenNewtonsoftSupport();              // F�r att hhtp Patch att funka i och anv�nda i Swagger UI
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;         // F�r att hhtp Patch att funka i och anv�nda i Swagger UI
                });
                app.UseSwaggerUI();
            }

            app.UseAuthentication();    // M�ste ligga f�re UseAuthorization
            app.UseAuthorization();     // M�ste ligga efter UseAuthentication

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
