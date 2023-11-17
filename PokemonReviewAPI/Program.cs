using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
            });

            builder.Services.AddTransient<Seed>();
            builder.Services.AddScoped<IPokemonRepos, PokemonRepos>();
            builder.Services.AddScoped<ICategoryRepos, CategoryRepos>();
            builder.Services.AddScoped<ICountryRepos, CountryRepos>();
            builder.Services.AddScoped<IOwnerRepos, OwnerRepos>();
            builder.Services.AddScoped<IReviewRepos, ReviewRepos>();
            builder.Services.AddScoped<IReviewerRepos, ReviewerRepos>();

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app) {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope()) {
                    var service = scope.ServiceProvider.GetService<Seed>();
                    service.SeedDataContext();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
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