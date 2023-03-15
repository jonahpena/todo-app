using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskAPI.Data;

namespace TaskAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskApi", Version = "v1" });
            });
            services.AddDbContext<ApiDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dbContext, ILogger<Startup> logger)
        {

            dbContext.Database.EnsureCreated();

            if (dbContext.Database.CanConnect())
            {
                logger.LogInformation("Connected to the database successfully.");
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    logger.LogWarning("There are pending migrations. Consider applying the migrations.");
                }
            }
            else
            {
                logger.LogError("Failed to connect to the database.");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

    }
}
