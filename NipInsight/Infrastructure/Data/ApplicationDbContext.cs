using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NipInsight.Domain.Entities;
using NipInsight.Domain.Enums;

namespace NipInsight.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Entity> Entities { get; set; }
    public DbSet<EntityPerson> EntityPersons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>(company =>
        {
            company.ToTable("Company");

            company.HasKey(e => e.Nip);

            company.Property(e => e.AccountNumbers)
                .HasConversion(
                    v => string.Join(';', v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (e1, e2) => e1!.SequenceEqual(e2!),
                    c => c.Select((v, i) => HashCode.Combine(i, v.GetHashCode()))
                        .Aggregate(0, HashCode.Combine),
                    e => e.ToList()));

            company.Property(e => e.StatusVat)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusVat)Enum.Parse(typeof(StatusVat), v!));

            modelBuilder.Entity<Entity>()
                .HasMany(e => e.Representatives)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "EntityRepresentative",
                    j => j
                        .HasOne<EntityPerson>()
                        .WithMany()
                        .HasForeignKey("EntityPersonId"),
                    j => j
                        .HasOne<Entity>()
                        .WithMany()
                        .HasForeignKey("EntityId"));

            modelBuilder.Entity<Entity>()
                .HasMany(e => e.AuthorizedClerks)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "EntityAuthorizedClerk",
                    j => j
                        .HasOne<EntityPerson>()
                        .WithMany()
                        .HasForeignKey("EntityPersonId"),
                    j => j
                        .HasOne<Entity>()
                        .WithMany()
                        .HasForeignKey("EntityId"));

            modelBuilder.Entity<Entity>()
                .HasMany(e => e.Partners)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "EntityPartner",
                    j => j
                        .HasOne<EntityPerson>()
                        .WithMany()
                        .HasForeignKey("EntityPersonId"),
                    j => j
                        .HasOne<Entity>()
                        .WithMany()
                        .HasForeignKey("EntityId"));

            company.Ignore(e => e.Pesel);
        });

        modelBuilder.Entity<EntityPerson>(person =>
        {
            person.ToTable("Person");
            person.HasKey(e => e.Id);
            person.Ignore(e => e.Pesel);
        });
    }
}