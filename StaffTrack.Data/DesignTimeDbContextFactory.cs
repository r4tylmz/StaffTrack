using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StaffTrack.Data
{
    public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Server=localhost,1433;Database=StaffTrack;User ID=SA;Password=y1lmaX123;MultipleActiveResultSets=True;";
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}