using Microsoft.EntityFrameworkCore;

namespace Repositories.DbEntities
{
    public class MarvelConventionDbContext:DbContext
    {
        //public DbSet<Participant> Participants { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Convention> Conventions { get; set; }
        public DbSet<ConventionParticipant> ConventionParticipants { get; set; }
        public MarvelConventionDbContext(DbContextOptions<MarvelConventionDbContext> options): base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //}
    }
}
