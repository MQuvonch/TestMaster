using Microsoft.EntityFrameworkCore;
using TestExecution.Api.Extensions;
using TestExecution.Api.MidlleWares;
using TestExecution.Data.Contexts;

namespace TestExecution.Api
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
            var connectionString = builder.Configuration.GetConnectionString("DefalutConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
            Console.WriteLine("--> CONNECTION String: " + connectionString);
            builder.Services.AddCustomExtension();
            builder.Services.AddSwaggerService();
            builder.Services.AddJwtService(builder.Configuration);


            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()  || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleWare>();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            using(var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            app.MapControllers();

            app.Run();
        }
    }
}
