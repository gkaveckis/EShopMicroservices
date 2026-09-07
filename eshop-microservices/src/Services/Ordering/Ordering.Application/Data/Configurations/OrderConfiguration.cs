using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o=> o.Id).HasConversion(
                id => id.Value,
                value => OrderId.Of(value)
            );

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(o=> o.OrderName, nameBuilder => 
            {
                nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .IsRequired()
                .HasMaxLength(100);
            });

            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(a => a.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                      .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
                    
            });

            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(a => a.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                      .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();

            });

            builder.ComplexProperty(o => o.Payment, addressBuilder =>
            {
                addressBuilder.Property(p => p.CardName)
                    .HasMaxLength(50);                    

                addressBuilder.Property(a => a.CardNumber)
                      .HasMaxLength(24)
                      .IsRequired();

                addressBuilder.Property(a => a.Expiration)
                      .HasMaxLength(10);

                addressBuilder.Property(a => a.CVV)
                    .HasMaxLength(3);

                addressBuilder.Property(a => a.PaymentMethod);
            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    value => Enum.Parse<OrderStatus>(value)
                )
                .IsRequired();

            builder.Property(o => o.TotalPrice);
        }
    }
}
