using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.DAL.EntityTypeConfigurations
{
    /// <summary> Создание конфигурации для <see cref="AccountEntity"/>. </summary>
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.ToTable(name: "accounts");

            builder.HasKey(keyExpression: e => e.AccountId);

            builder.Property(propertyExpression: e => e.AccountId)
                .HasColumnName(name: "account_id")
                .HasColumnType(typeName: "uuid")
                .HasDefaultValueSql(sql: "uuid_generate_v4()")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Login)
                .HasColumnName(name: "login")
                .HasMaxLength(maxLength: 20)
                .HasColumnType(typeName: "text")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Email)
                .HasColumnName(name: "email")
                .HasMaxLength(maxLength: 50)
                .HasColumnType(typeName: "text")
                .IsRequired();

            builder.Property(propertyExpression: e => e.PasswordHash)
                .HasColumnName(name: "password_hash")
                .HasMaxLength(maxLength: 60)
                .HasColumnType(typeName: "text")
                .IsRequired();

            builder.Property(propertyExpression: e => e.RoleId)
                .HasColumnName(name: "role_id")
                .HasColumnType(typeName: "int")
                .IsRequired();

            builder.HasOne(e => e.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
