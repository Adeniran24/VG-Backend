using Csatahajok_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Csatahajok_backend.Data;

public class CsatahajokContext : DbContext
{
    public CsatahajokContext(DbContextOptions<CsatahajokContext> options)
        : base(options)
    {
    }

    public DbSet<Hajo> Hajok => Set<Hajo>();
    public DbSet<Csata> Csatak => Set<Csata>();
    public DbSet<Kimenet> Kimenetek => Set<Kimenet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hajo>(entity =>
        {
            entity.ToTable("hajo");
            entity.HasKey(e => e.Nev);
            entity.Property(e => e.Nev).HasColumnName("nev").HasMaxLength(100);
            entity.Property(e => e.Osztaly).HasColumnName("osztaly").HasMaxLength(100);
            entity.Property(e => e.Felavatva).HasColumnName("felavatva");
            entity.Property(e => e.AgyukSzama).HasColumnName("agyukSzama");
            entity.Property(e => e.Kaliber).HasColumnName("kaliber");
            entity.Property(e => e.Vizkiszoritas).HasColumnName("vizkiszoritas");
        });

        modelBuilder.Entity<Csata>(entity =>
        {
            entity.ToTable("csata");
            entity.HasKey(e => e.Nev);
            entity.Property(e => e.Nev).HasColumnName("nev").HasMaxLength(100);
            entity.Property(e => e.Kezdes).HasColumnName("kezdes");
            entity.Property(e => e.Befejezes).HasColumnName("befejezes");
        });

        modelBuilder.Entity<Kimenet>(entity =>
        {
            entity.ToTable("kimenet");
            entity.HasKey(e => new { e.Hajo, e.Csata });
            entity.Property(e => e.Hajo).HasColumnName("hajo").HasMaxLength(100);
            entity.Property(e => e.Csata).HasColumnName("csata").HasMaxLength(100);
            entity.Property(e => e.Eredmeny).HasColumnName("eredmeny").HasMaxLength(50);

            entity.HasOne(d => d.HajoNavigation)
                .WithMany(p => p.Kimenetek)
                .HasForeignKey(d => d.Hajo)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.CsataNavigation)
                .WithMany(p => p.Kimenetek)
                .HasForeignKey(d => d.Csata)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
