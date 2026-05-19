using Computers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Computers.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PC> PCs { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(e =>
        {
            e.ToTable("PCs");
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(50);
            e.Property(p => p.Weight).HasPrecision(5, 2);
            e.Property(p => p.Warranty);
            e.Property(p => p.CreatedAt);
            e.Property(p => p.Stock);
        });
        modelBuilder.Entity<PCComponent>(e =>
        {
            e.ToTable("PCComponents");
            e.HasKey(pc => new { pc.PCId, pc.ComponentCode });
            e.Property(pc => pc.ComponentCode).HasMaxLength(10);
            e.Property(pc => pc.Amount);

            // FK -> PC
            e.HasOne(pc => pc.PC)
                .WithMany(p => p.PCComponents)
                .HasForeignKey(pc => pc.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            // FK -> Component
            e.HasOne(pc => pc.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(pc => pc.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Component>(e =>
        {
            e.ToTable("Components");
            e.HasKey(c => c.Code);
            e.Property(c => c.Code)
                .HasMaxLength(10);

            e.Property(c => c.Name)
                .HasMaxLength(300)
                .IsRequired();

            e.Property(c => c.Description)
                .HasMaxLength(300);

            // FK -> Manufacturer
            e.HasOne(c => c.ComponentManufacturer).WithMany(m => m.Components)
                .HasForeignKey(c => c.ComponentManufacturerId);

            // FK -> ComponentType
            e.HasOne(c => c.ComponentType).WithMany(t => t.Components)
                .HasForeignKey(c => c.ComponentTypeId);
        });
        modelBuilder.Entity<ComponentType>(e =>
        {
            e.ToTable("ComponentTypes");

            e.HasKey(ct => ct.Id);

            e.Property(ct => ct.Abbreviation)
                .HasMaxLength(30);

            e.Property(ct => ct.Name)
                .HasMaxLength(150);
        });
        modelBuilder.Entity<ComponentManufacturer>(e =>
        {
            e.ToTable("ComponentManufacturers");

            e.HasKey(cm => cm.Id);

            e.Property(cm => cm.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            e.Property(cm => cm.FullName)
                .HasMaxLength(300)
                .IsRequired();

            e.Property(cm => cm.FoundationDate)
                .HasColumnType("date");
        }); 
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Name = "Processor",
                Abbreviation = "CPU"
            },
            new ComponentType
            {
                Id = 2,
                Name = "Graphics Card",
                Abbreviation = "GPU"
            },
            new ComponentType
            {
                Id = 3,
                Name = "Memory",
                Abbreviation = "RAM"
            }
        );

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                FullName = "Intel Corporation",
                Abbreviation = "Intel",
                FoundationDate = new DateTime(1968, 7, 18)
            },
            new ComponentManufacturer
            {
                Id = 2,
                FullName = "NVIDIA Corporation",
                Abbreviation = "NVIDIA",
                FoundationDate = new DateTime(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                FullName = "Corsair",
                Abbreviation = "Corsair",
                FoundationDate = new DateTime(1994, 1, 1)
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU001",
                Name = "Intel i7 14700K",
                Description = "14th Gen Intel Processor",
                ComponentManufacturerId = 1,
                ComponentTypeId = 1
            },
            new Component
            {
                Code = "GPU001",
                Name = "RTX 4070",
                Description = "NVIDIA Graphics Card",
                ComponentManufacturerId = 2,
                ComponentTypeId = 2
            },
            new Component
            {
                Code = "RAM001",
                Name = "Corsair Vengeance 32GB",
                Description = "DDR5 RAM Kit",
                ComponentManufacturerId = 3,
                ComponentTypeId = 3
            }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1,
                Name = "Gaming Beast",
                Weight = 12.50m,
                Warranty = 24,
                CreatedAt = new DateTime(2024, 1, 1),
                Stock = 5
            },
            new PC
            {
                Id = 2,
                Name = "Office Pro",
                Weight = 8.20m,
                Warranty = 12,
                CreatedAt = new DateTime(2024, 2, 10),
                Stock = 10
            },
            new PC
            {
                Id = 3,
                Name = "Streaming Setup",
                Weight = 10.75m,
                Warranty = 36,
                CreatedAt = new DateTime(2024, 3, 15),
                Stock = 3
            }
        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "CPU001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 2,
                ComponentCode = "GPU001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 3,
                ComponentCode = "RAM001",
                Amount = 2
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}