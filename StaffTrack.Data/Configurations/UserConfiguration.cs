using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTrack.Core.Entities;

namespace StaffTrack.Data.Configurations
{
    public class UserConfiguration:IEntityTypeConfiguration<User>       
    {
        public void Configure(EntityTypeBuilder<User> builder)
        { 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.ToTable("Users");
        }
    }
}