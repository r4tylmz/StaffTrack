using Microsoft.EntityFrameworkCore;
using StaffTrack.Core.Entities;
using StaffTrack.Data.Configurations;

namespace StaffTrack.Data
{
    public class AppDbContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new StaffActivityConfiguration());
        }
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<StaffActivity> StaffActivities;
        public DbSet<User> Users;
        public DbSet<Staff> Staffs;


    }
}