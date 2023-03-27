using Microsoft.EntityFrameworkCore;
using TaskListAPIProject.Data;

namespace TaskListAPIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //
            // CreateHostBuilder(args).Build().Run();
            //
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("TaskDb");

                if (string.IsNullOrEmpty(connectionString))
                {
                    options.UseInMemoryDatabase("TaskDb");
                }
                else
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            });

            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            EnsureDatabaseCreated(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });
        //
        private static void EnsureDatabaseCreated(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
            dbContext.Database.EnsureCreated();
        }

       
    }
}
