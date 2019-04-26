using System.Data.Entity;
using MVCApp.Core.Domain;

namespace MVCApp.Data.EntityFramework
{
    public class MVCAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rotation> Rotations { get; set; }

        public MVCAppContext() : base("MVCApp")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<Rotation>()
                .HasKey(x => x.RotationId);

            modelBuilder.Entity<Rotation>()
                .HasMany<User>(x => x.Members)
                .WithMany(x => x.Rotations)
                .Map(x =>
                {
                    x.MapLeftKey("RotationId");
                    x.MapRightKey("UserId");
                    x.ToTable("RotationMembers");
                });
        }
    }
}