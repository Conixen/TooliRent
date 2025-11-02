using Microsoft.EntityFrameworkCore;
using TooliRent.Infrastructure.Data;
using TooliRent.Core.Interfaces;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Infrastructure.Repositories;
using TooliRent.Core.Interfaces.IService;
using TooliRent.Services.Service;
using TooliRent.Mapping;
using System.Reflection;    // for xml remaning
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using TooliRent.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;

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
            //builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

            // Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthService, UserService>();
            builder.Services.AddScoped<IToolService, ToolService>();
            //builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
            builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

            // AutoMapper

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryMappingProfile>();
                cfg.AddProfile<AuthMappingProfile>();
                cfg.AddProfile<ToolMappingProfile>();
                cfg.AddProfile<OrderMappingProfile>();
                //cfg.AddProfile<ReservationMappingProfile>();
            });

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http, // we dont like .ApiKey
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Write in yopur JWT Token, no bearerer"
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
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });
            // JWT
            var jwt = builder.Configuration.GetSection("Jwt");      // GetSection returnerar allt i "Jwt" i appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])); // Skapar en ny SymmetricSecurityKey med hjälp av "Key" i appsettings.json

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,                  // kollar så att den som skickar token är den vi litar på
                        ValidateAudience = true,                // kollar så att den som tar emot token är den vi litar på
                        ValidateIssuerSigningKey = true,        // kollar så att signeringsnyckeln är korrekt
                        ValidateLifetime = true,                // kollar så att token inte har gått ut
                        ValidIssuer = jwt["Issuer"],            // kollar så att den som skickar token är den vi litar på
                        ValidAudience = jwt["Audience"],        // kollar så att den som tar emot token är den vi litar på
                        IssuerSigningKey = key,                 // kollar så att signeringsnyckeln är korrekt
                        ClockSkew = TimeSpan.FromMinutes(1)     // tiden som token är giltig efter att den har gått ut
                    };
                });

            builder.Services.AddAuthorization();                // Behövs för att kunna använda [Authorize] i controllers

            builder.Services.AddControllers().AddNewtonsoftJson();          // För att hhtp Patch att funka i och använda i Swagger UI

            builder.Services.AddSwaggerGenNewtonsoftSupport();              // För att hhtp Patch att funka i och använda i Swagger UI
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;         // För att hhtp Patch att funka i och använda i Swagger UI
                });
                app.UseSwaggerUI();
            }

            app.UseAuthentication();    // Måste ligga före UseAuthorization
            app.UseAuthorization();     // Måste ligga efter UseAuthentication

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}