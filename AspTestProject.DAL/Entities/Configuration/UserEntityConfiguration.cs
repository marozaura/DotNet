using AspTestProject.DAL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspTestProject.DAL.Entities.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(DataBaseTableName.User, DataBaseTableScheme.Users);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired();

            builder.Property(x => x.PasswordHash).IsRequired();

            builder.Property(x => x.PasswordSalt).IsRequired();

            builder.Property(x => x.CreatedDateTimeUtc).IsRequired();

            builder.Property(u => u.CreatedDateTimeUtc)
                .IsRequired()
                .HasDefaultValueSql(DatabaseFunctionConstants.GetDate);

            builder.Property(u => u.UpdatedDateTimeUtc)
                .IsRequired()
                .HasDefaultValueSql(DatabaseFunctionConstants.GetDate);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
