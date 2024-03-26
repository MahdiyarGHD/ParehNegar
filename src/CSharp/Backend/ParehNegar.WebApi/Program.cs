using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParehNegar.Database;
using ParehNegar.Database.Database.Contexts;
using ParehNegar.WebApi.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Text;

namespace AppTax.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateBuilder(args);
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            string text = app.Configuration["Authorization:HasToUse"];
            if (text.HasValue() && text.Equals("true", StringComparison.OrdinalIgnoreCase))
                app.Services.AddAuthentication("Bearer").AddJwtBearer(delegate (JwtBearerOptions options)
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = app.Configuration["Authorization:JWT:Issuer"],
                        ValidAudience = app.Configuration["Authorization:JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app.Configuration["Authorization:Jwt:Key"]))
                    };
                });

            WebApplication webApplication = app.Build();

            webApplication.UseCors(delegate (CorsPolicyBuilder options)
            {
                List<string> anyCors = configuration.GetSection("Cors:Any")?.Get<List<string>>();
                List<string> list = anyCors;
                if (list != null && list.Count > 0)
                    options.SetIsOriginAllowed((string origin) => anyCors.Any((string x) => new Uri(origin).Host.Equals(x, StringComparison.OrdinalIgnoreCase))).AllowAnyHeader().AllowAnyMethod()
                        .AllowAnyHeader();
                else
                    options.SetIsOriginAllowed((string origin) => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
            });

            webApplication.UseRouting();
            webApplication.UseAuthentication();
            webApplication.UseExceptionHandler();

            webApplication.MapControllers();

            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();

            await webApplication.RunAsync();
        }

        static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = WebApplication.CreateBuilder(args);

            app.Services.AddControllers();
            app.Services.AddEndpointsApiExplorer();
            app.Services.AddSwaggerGen(delegate (SwaggerGenOptions options)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    } 
                });
                options.MapType<decimal>(() => new OpenApiSchema
                {
                    Type = "number",
                    Format = "decimal"
                });
            });
            app.Services.AddHttpContextAccessor();
            app.Services.AddTransient((Func<IServiceProvider, DbContext>)((IServiceProvider serviceProvider) => serviceProvider.GetService<ParehNegarContext>()));
            app.Services.AddExceptionHandler(delegate (ExceptionHandlerOptions option)
            {
                option.ExceptionHandler = AppAuthorizationMiddleware.ExceptionHandler;
            });

            app.Services.AddTransient(serviceProvider => new ParehNegarContext(serviceProvider.GetService<DatabaseBuilder>()));

            return app;
        }

    }
}