
using BusinessLayer.Abstract;
using BusinessLayer.Impl;
using DAL.Abstract;
using DAL.Impl;
using Entities.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
            });

            var jwt = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

            builder.Services.AddSingleton(jwt);


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
            builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
            builder.Services.AddScoped<ICategoryBusiness, CategoryBusiness>();




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
        }
    }
}
