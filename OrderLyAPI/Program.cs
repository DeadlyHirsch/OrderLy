using OrderLy_API.Models;
using OrderLy_API.Services;

namespace OrderLy_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<OrderLyDatabaseSettings>(builder.Configuration.GetSection("OrderLyDatabase"));

            builder.Services.AddSingleton<OrderService>();
            builder.Services.AddSingleton<VendorService>();

            var specificOrigins = "AppOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: specificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:63342");
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(specificOrigins);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
