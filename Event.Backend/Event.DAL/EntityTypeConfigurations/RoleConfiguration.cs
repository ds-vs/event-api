using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.DAL.EntityTypeConfigurations
{
    /// <summary> Конфигурация для <see cref="RoleEntity"/>. </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable(name: "roles");

            builder.HasKey(keyExpression: e => e.RoleId);

            builder.Property(propertyExpression: e => e.RoleId)
                .HasColumnName(name: "role_id")
                .HasColumnType(typeName: "int")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Name)
                .HasColumnName(name: "name")
                .HasMaxLength(maxLength: 30)
                .HasColumnType(typeName: "text")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Description)
                .HasColumnName(name: "description")
                .HasMaxLength(maxLength: 100)
                .HasColumnType(typeName: "text")
                .IsRequired();

            builder.HasData 
            (
                new RoleEntity 
                { 
                    RoleId = 1, 
                    Name = "Member", 
                    Description = "Member" 
                },
                new RoleEntity
                {
                    RoleId = 2,
                    Name = "Organizer",
                    Description = "Organizer"
                }
            );
        }
    }
}
