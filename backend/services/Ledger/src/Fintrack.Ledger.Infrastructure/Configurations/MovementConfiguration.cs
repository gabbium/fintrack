using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Configurations;

internal sealed class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.HasKey(mov => mov.Id);

        builder.HasIndex(mov => mov.UserId);

        builder.Property(mov => mov.UserId);

        builder.Property(mov => mov.Kind)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(mov => mov.Amount)
            .HasPrecision(18, 2);

        builder.Property(mov => mov.Description)
            .HasMaxLength(128);

        builder.Property(mov => mov.OccurredOn);
    }
}
