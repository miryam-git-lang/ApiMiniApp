using ApiMiniApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMiniApp.Configurations;

public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.ToTable("Organizers");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        // Seed data
        var seedDate = new DateTime(2026, 5, 19, 16, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            new Organizer 
            { 
                Id = 1,
                Name = "TechConf Inc.",
                Email = "contact@techconf.az",
                Phone = "+994501234567",
                LogoUrl = "/uploads/logos/techconf.png",
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Organizer 
            { 
                Id = 2,
                Name = "ArtSpace Baku",
                Email = "info@artspace.az",
                Phone = "+994552345678",
                LogoUrl = "/uploads/logos/artspace.png",
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Organizer 
            { 
                Id = 3,
                Name = "SportLife Agency",
                Email = "hello@sportlife.az",
                Phone = "+994703456789",
                LogoUrl = "/uploads/logos/sportlife.png",
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Organizer 
            { 
                Id = 4,
                Name = "MusicFest Org",
                Email = "press@musicfest.az",
                Phone = null,
                LogoUrl = "/uploads/logos/musicfest.png",
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new Organizer 
            { 
                Id = 5,
                Name = "EduWorld Azerbaijan",
                Email = "edu@eduworld.az",
                Phone = "+994514567890",
                LogoUrl = null,
                CreatedAt = seedDate,
                IsDeleted = false
            }
        );
    }
}