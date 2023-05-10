using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.DAL.EntityTypeConfigurations
{
    /// <summary> Конфигурация для <see cref="AccountEntity"/>. </summary>
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

            builder.Property(propertyExpression: e => e.RefreshToken)
                .HasColumnName(name: "refresh_token")
                .HasMaxLength(maxLength: 64)
                .HasColumnType(typeName: "text");

            builder.Property(propertyExpression: e => e.TokenCreated)
                .HasColumnName(name: "token_created")
                .HasColumnType(typeName: "timestamp without time zone");

            builder.Property(propertyExpression: e => e.TokenExpires)
                .HasColumnName(name: "token_expires")
                .HasColumnType(typeName: "timestamp without time zone");

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
