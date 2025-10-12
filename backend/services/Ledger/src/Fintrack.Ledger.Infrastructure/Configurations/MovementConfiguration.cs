using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Configurations;

internal sealed class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.HasKey(movement => movement.Id);

        builder.HasIndex(movement => movement.UserId);

        builder.Property(movement => movement.UserId);

        builder.Property(movement => movement.Kind)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(movement => movement.Amount)
            .HasPrecision(18, 2);

        builder.Property(movement => movement.Description)
            .HasMaxLength(128);

        builder.Property(movement => movement.OccurredOn);
    }
}
