using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PokemonCategory>().HasKey(x => new { x.PokemonId, x.CategoryId });

            modelBuilder.Entity<PokemonCategory>().HasOne(x => x.Pokemon).
                WithMany(x => x.PokemonCategories).HasForeignKey(x => x.PokemonId);

            modelBuilder.Entity<PokemonCategory>().HasOne(x => x.Category).
                WithMany(x => x.PokemonCategories).HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<PokemonOwner>().HasKey(x => new { x.PokemonId, x.OwnerId });

            modelBuilder.Entity<PokemonOwner>().HasOne(x => x.Pokemon).
                WithMany(x => x.PokemonOwners).HasForeignKey(x => x.PokemonId);

            modelBuilder.Entity<PokemonOwner>().HasOne(x => x.Owner).
                WithMany(x => x.PokemonOwners).HasForeignKey(x => x.OwnerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
