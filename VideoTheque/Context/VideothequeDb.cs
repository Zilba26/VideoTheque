using Microsoft.EntityFrameworkCore;
using VideoTheque.DTOs;

namespace VideoTheque.Context
{
    public class VideothequeDb : DbContext
    {
        public VideothequeDb(DbContextOptions options) : base(options) { }
        public DbSet<AgeRatingDto> AgeRatings { get; set; } = null!;
        public DbSet<BluRayDto> BluRays { get; set; } = null!;
        public DbSet<GenreDto> Genres { get; set; } = null!;
        public DbSet<HostDto> Hosts { get; set; } = null!;
        public DbSet<PersonneDto> Personnes { get; set; } = null!;
        
        public override async ValueTask DisposeAsync() // <-- this method is the fix
        {
            Console.WriteLine("DisposeAsync");
            Dispose();
            await Task.CompletedTask;
        }
    }
}
