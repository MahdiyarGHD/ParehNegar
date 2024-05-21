using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Mapper.SerializerMapper.Providers;
using EasyMicroservices.Serialization.Interfaces;
using EasyMicroservices.Serialization.Providers;
using EasyMicroservices.Serialization.System.Text.Json.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParehNegar.Database;
using ParehNegar.Database.Contexts;
using ParehNegar.Domain.Contracts;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Helpers;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Logics;
using ParehNegar.WebApi.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ParehNegar.Logics.Managers;

namespace AppTax.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        var app = CreateBuilder(args, configuration);

        WebApplication webApplication = app.Build();
        webApplication.UseMiddleware<AppAuthorizationMiddleware>();

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

        webApplication.UseExceptionHandler();
        
        webApplication.UseMiddleware<TokenFromCookieMiddleware>();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();

        webApplication.MapControllers();

        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();
        
        DbContext context = webApplication.Services.GetService<DbContext>();
        if (context.Database.IsInMemory())
            await context.Database.EnsureCreatedAsync();

        await webApplication.RunAsync();
    }

    static WebApplicationBuilder CreateBuilder(string[] args, IConfiguration configuration)
    {
        var app = WebApplication.CreateBuilder(args);
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

        app.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
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
        app.Services.AddExceptionHandler(delegate (ExceptionHandlerOptions option)
        {
            option.ExceptionHandler = AppAuthorizationMiddleware.ExceptionHandler;
        });
        app.Services.AddHttpContextAccessor();
        app.Services.AddTransient(sp => configuration);
        app.Services.AddTransient(sp => new DatabaseBuilder(sp.GetService<IConfiguration>()));
        app.Services.AddTransient<DbContext>(sp => new ParehNegarContext(sp.GetService<DatabaseBuilder>()));

        app.Services.AddScoped<ITextSerializationProvider, SystemTextJsonProvider>(sp => new SystemTextJsonProvider(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }));
        app.Services.AddScoped<IMapperProvider, SerializerMapperProvider>();
        app.Services.AddScoped<IMapperProvider, SerializerMapperProvider>();

        app.Services.AddScoped<IUnitOfWork>(sp => new UnitOfWork(sp));
        app.Services.AddTransient<IJWTHelper, JWTHelper>();
        app.Services.AddTransient<IdentityHelper>();
        app.Services.AddTransient<ClaimManager>(sp => new ClaimManager(sp.GetService<IHttpContextAccessor>()));

        app.Services.AddScoped<IUnitOfWork>(sp => new UnitOfWork(sp));
            
        app.Services.AddScoped<ContentLanguageHelper>();
        app.Services.AddTransient<ContentHelper>();

        return app;
    }

}