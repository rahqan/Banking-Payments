
using Banking_Payments.Context;
using Banking_Payments.Custom;
using Banking_Payments.Models;
using Banking_Payments.Repositories;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Banking_Payments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection")));

            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IBankUserRepository, BankUserRepository>();
            builder.Services.AddScoped<IBankUserService, BankUserService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            //builder.Services.AddScoped<IAuthService, AuthService>();
            //builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();


            builder.Services.AddScoped<IBankUserService, BankUserService>();
            builder.Services.AddScoped<IBankUserRepository, BankUserRepository>();


            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IBankUserService, BankUserService>();

            // Cloudinary config
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings")
            );


            //builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Error);
            });
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            // Add Authentication Services.

            builder.Services.AddAuthentication(opt =>
            {

                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(option =>
            //{
            //    option.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["JWT:Issuer"],
            //        ValidAudience = builder.Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
            //    };
            //});

            builder.Services.AddControllers();
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler =
            //        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //}); 


            // Whenever we do pipeline, in that case we have to add this line so that does not get any error.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    builder => builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

             // Important to add cors when we have different url of frontend and backend.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "First Api"
                });
                //Define Security Scheme for JWT BEarer Tokens
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT Bearer Token only",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                //Add Security Requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[]{ } }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "First API");
                    options.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAngular");

            

            app.MapControllers();

            app.Run();
        }
    }
}
