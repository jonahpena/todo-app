using Microsoft.EntityFrameworkCore;
using TaskListAPIProject.Data;



namespace TaskListAPIProject
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TaskDbContext>(option => option.UseMySql(@"Server=localhost;Database=TaskDb;User Id=jonahpena;Password=mysql2012!;", ServerVersion.AutoDetect(@"Server=localhost;Database=TaskDb;User Id=jonahpena;Password=mysql2012!;")));

            var app = builder.Build();

            EnsureDatabaseCreated(app);

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
        private static void EnsureDatabaseCreated(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}