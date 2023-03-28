using Microsoft.EntityFrameworkCore;
using TaskListAPIProject.Data;

namespace TaskListAPIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();
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
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            EnsureDatabaseCreated(app);


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseCors("AllowLocalhost3000");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        private static void EnsureDatabaseCreated(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
            dbContext.Database.EnsureCreated();
        }
        
    }
}
