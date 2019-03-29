using System.Data.Entity;
using MVCApp.Core.Domain;

namespace MVCApp.Data.EntityFramework
{
    public class MVCAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MVCAppContext() : base("MVCApp")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.UserId); 
        }
    }
}