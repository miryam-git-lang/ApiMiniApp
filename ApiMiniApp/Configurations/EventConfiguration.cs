using ApiMiniApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMiniApp.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(e => e.Description)
            .HasMaxLength(500);
        builder.Property(e => e.Date)
            .HasColumnType("datetime");
        builder.Property(e => e.Location)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(e => e.Organizer)
            .WithMany(o => o.Events)
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Seed data
        var seedDate = new DateTime(2026, 5, 19, 16, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            new Event
            {
                Id = 1,
                Title = "Tech Summit 2025",
                Description = "Крупнейшая технологическая конференция года с участием мировых спикеров.",
                Date = new DateTime(2025, 9, 15, 10, 0, 0),
                Location = "Baku Convention Center, Баку",
                BannerImageUrl = "/uploads/banners/techsummit.jpg",
                OrganizerId = 1,
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Event
            {
                Id = 2,
                Title = "Современное искусство: Осень",
                Description = "Выставка работ молодых художников Азербайджана.",
                Date = new DateTime(2025, 10, 5, 12, 0, 0),
                Location = "ArtSpace Gallery, ул. Низами 42, Баку",
                BannerImageUrl = "/uploads/banners/artfall.jpg",
                OrganizerId = 2,
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Event
            {
                Id = 3,
                Title = "Бакинский марафон 2025",
                Description = "Ежегодный городской марафон по центру Баку.",
                Date = new DateTime(2025, 11, 1, 8, 0, 0),
                Location = "Площадь Азадлыг, Баку",
                BannerImageUrl = null,
                OrganizerId = 3,
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Event
            {
                Id = 4,
                Title = "Jazz Under The Stars",
                Description = "Вечер живой джазовой музыки под открытым небом.",
                Date = new DateTime(2025, 8, 20, 19, 30, 0),
                Location = "Приморский бульвар, Баку",
                BannerImageUrl = "/uploads/banners/jazz.jpg",
                OrganizerId = 4,
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Event
            {
                Id = 5,
                Title = "Python Bootcamp",
                Description = "Интенсивный трёхдневный курс по Python для начинающих и продолжающих.",
                Date = new DateTime(2025, 9, 25, 9, 0, 0),
                Location = "EduWorld Campus, пр. Матбуат 102",
                BannerImageUrl = null,
                OrganizerId = 5,
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Event
            {
                Id = 6,
                Title = "Startup Pitch Night",
                Description = "Вечер презентаций стартапов перед венчурными инвесторами.",
                Date = new DateTime(2025, 10, 18, 17, 0, 0),
                Location = "Baku Tech Hub, ул. Рашида Бейбутова 10",
                BannerImageUrl = "/uploads/banners/pitch.jpg",
                OrganizerId = 1,
                CreatedAt = seedDate,
                IsDeleted = false
            }
        );
    }
}