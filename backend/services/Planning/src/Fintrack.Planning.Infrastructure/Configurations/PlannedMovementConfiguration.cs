using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Infrastructure.Configurations;

internal sealed class PlannedMovementConfiguration : IEntityTypeConfiguration<PlannedMovement>
{
    public void Configure(EntityTypeBuilder<PlannedMovement> builder)
    {
        builder.HasKey(plannedMovement => plannedMovement.Id);

        builder.HasIndex(plannedMovement => plannedMovement.UserId);

        builder.Property(plannedMovement => plannedMovement.UserId);

        builder.Property(plannedMovement => plannedMovement.Kind)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(plannedMovement => plannedMovement.Amount)
            .HasPrecision(18, 2);

        builder.Property(plannedMovement => plannedMovement.Description)
            .HasMaxLength(128);

        builder.Property(plannedMovement => plannedMovement.DueOn);

        builder.Property(plannedMovement => plannedMovement.Status)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Ignore(plannedMovement => plannedMovement.DomainEvents);
    }
}
