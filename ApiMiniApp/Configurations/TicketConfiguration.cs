using ApiMiniApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMiniApp.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket> 
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Type)
            .IsRequired()   
            .HasMaxLength(100);
        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");
        builder.Property(e => e.QuantityAvailable)
            .IsRequired();
        
        builder.HasOne(e => e.Event)
            .WithMany(e => e.Tickets)
            .HasForeignKey(e => e.EventId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Seed data
        var seedDate = new DateTime(2026, 5, 19, 16, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            // Tech Summit 2025 (EventId = 1)
            new Ticket { Id = 1, EventId = 1, Type = "VIP", Price = 299.99m, QuantityAvailable = 50, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 2, EventId = 1, Type = "Regular", Price = 99.99m, QuantityAvailable = 300, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 3, EventId = 1, Type = "Student", Price = 49.99m, QuantityAvailable = 100, CreatedAt = seedDate, IsDeleted = false },

            // Современное искусство (EventId = 2)
            new Ticket { Id = 4, EventId = 2, Type = "Regular", Price = 15.00m, QuantityAvailable = 200, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 5, EventId = 2, Type = "VIP", Price = 35.00m, QuantityAvailable = 30, CreatedAt = seedDate, IsDeleted = false },

            // Бакинский марафон (EventId = 3)
            new Ticket { Id = 6, EventId = 3, Type = "Participant", Price = 25.00m, QuantityAvailable = 500, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 7, EventId = 3, Type = "VIP", Price = 75.00m, QuantityAvailable = 20, CreatedAt = seedDate, IsDeleted = false },

            // Jazz Under The Stars (EventId = 4)
            new Ticket { Id = 8, EventId = 4, Type = "Regular", Price = 20.00m, QuantityAvailable = 150, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 9, EventId = 4, Type = "VIP", Price = 60.00m, QuantityAvailable = 40, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 10, EventId = 4, Type = "Table", Price = 200.00m, QuantityAvailable = 10, CreatedAt = seedDate, IsDeleted = false },

            // Python Bootcamp (EventId = 5)
            new Ticket { Id = 11, EventId = 5, Type = "Regular", Price = 120.00m, QuantityAvailable = 80, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 12, EventId = 5, Type = "VIP", Price = 199.99m, QuantityAvailable = 15, CreatedAt = seedDate, IsDeleted = false },

            // Startup Pitch Night (EventId = 6)
            new Ticket { Id = 13, EventId = 6, Type = "General", Price = 0.00m, QuantityAvailable = 100, CreatedAt = seedDate, IsDeleted = false },
            new Ticket { Id = 14, EventId = 6, Type = "Investor", Price = 50.00m, QuantityAvailable = 25, CreatedAt = seedDate, IsDeleted = false }
        );
    }
}