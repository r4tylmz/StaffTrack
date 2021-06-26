using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTrack.Core.Entities;

namespace StaffTrack.Data.Configurations
{
    public class ConstantConfiguration:IEntityTypeConfiguration<Constant>
    {
        public void Configure(EntityTypeBuilder<Constant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.ToTable("Constants");
        }
    }
}