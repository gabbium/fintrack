using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Infrastructure.Configurations;

internal sealed class PlannedMovementConfiguration : IEntityTypeConfiguration<PlannedMovement>
{
    public void Configure(EntityTypeBuilder<PlannedMovement> builder)
    {
        builder.HasKey(pm => pm.Id);

        builder.HasIndex(pm => pm.UserId);

        builder.Property(pm => pm.UserId);

        builder.Property(pm => pm.Kind)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(pm => pm.Amount)
            .HasPrecision(18, 2);

        builder.Property(pm => pm.Description)
            .HasMaxLength(128);

        builder.Property(pm => pm.DueOn);

        builder.Property(pm => pm.Status)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Ignore(pm => pm.DomainEvents);
    }
}
