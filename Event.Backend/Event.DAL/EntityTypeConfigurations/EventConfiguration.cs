﻿using Event.Domain.Entities;
using Event.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.DAL.EntityTypeConfigurations
{
    /// <summary> Создание конфигурации для <see cref="EventEntity"/>. </summary>
    public class EventConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable(name: "events");

            builder.HasKey(keyExpression: e => e.EventId);

            builder.Property(propertyExpression: e => e.EventId)
                .HasColumnName(name: "event_id")
                .HasColumnType(typeName: "uuid")
                .HasDefaultValueSql(sql: "uuid_generate_v4()")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Title)
                .HasColumnName(name: "title")
                .HasMaxLength(maxLength: 60)
                .HasColumnType(typeName: "text")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Description)
                .HasColumnName(name: "description")
                .HasMaxLength(maxLength: 200)
                .HasColumnType(typeName: "text");

            builder.Property(propertyExpression: e => e.Responses)
                .HasColumnName(name: "responses")
                .HasColumnType(typeName: "bigint")
                .HasDefaultValue(value: 0)
                .IsRequired();

            builder.Property(propertyExpression: e => e.EventDate)
                .HasColumnName(name: "event_date")
                .HasColumnType(typeName: "timestamp without time zone")
                .IsRequired();

            builder.Property(propertyExpression: e => e.Status)
                .HasColumnName(name: "status")
                .HasColumnType(typeName: "int")
                .HasDefaultValue(value: StatusType.Actual)
                .HasConversion<int>();
        }
    }
}