
using Cart.Application;
using Cart.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CartService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDbCart"), ServiceLifetime.Transient);
            builder.Services.AddScoped<ICartRepository, CartRepository>();

            // Add services to the container.
            builder.Services.AddScoped<ICartUsageService, CartUsageService>();

            builder.Services.AddControllers();
            builder.Services.AddHttpClient<IEShopConnectService, EShopConnectService>(client =>
            {
                client.BaseAddress = new Uri("http://eshopservice:8080/");
                client.DefaultRequestHeaders.ConnectionClose = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
