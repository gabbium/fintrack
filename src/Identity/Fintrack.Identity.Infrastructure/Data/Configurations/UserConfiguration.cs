using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Infrastructure.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.Property(user => user.Email)
            .HasColumnType("citext")
            .HasMaxLength(256)
            .IsRequired();

        builder.Ignore(user => user.DomainEvents);

    }
}
