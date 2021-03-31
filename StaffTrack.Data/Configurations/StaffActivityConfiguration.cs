using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTrack.Core.Entities;

namespace StaffTrack.Data.Configurations
{
    public class StaffActivityConfiguration:IEntityTypeConfiguration<StaffActivity>
    {
        public void Configure(EntityTypeBuilder<StaffActivity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.ToTable("StaffActivities");
        }
    }
}