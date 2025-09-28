using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Configurations;

internal sealed class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.UserId);

        builder.Property(m => m.UserId);

        builder.Property(m => m.Kind)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(m => m.Amount)
            .HasPrecision(18, 2);

        builder.Property(m => m.Description)
            .HasMaxLength(128);

        builder.Property(m => m.OccurredOn);

        builder.Ignore(m => m.DomainEvents);
    }
}

