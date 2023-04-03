using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext>options) : base(options)
        {
            
        }

        public DbSet<Song> songs { get; set; }
        public DbSet<Artist> artists { get; set; }
        public DbSet<Album> albums { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
                new Song() { Id = 1, Title = "Test1", Language = "Tamil", Duration = "10", ImageURL = "" }

                );
        }*/
    }
}
