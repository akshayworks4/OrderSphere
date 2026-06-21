using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSphere.Domain.Entities.Vendor;

namespace OrderSphere.Persistence.Configurations.Vendor;

public class VendorApplicationConfiguration
    : IEntityTypeConfiguration<VendorApplication>
{
    public void Configure(EntityTypeBuilder<VendorApplication> builder)
    {
        builder.ToTable("Vendor_Application");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BusinessName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}