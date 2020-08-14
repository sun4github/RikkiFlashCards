using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnkiFlashCards.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RikkiFlashCards.Models.DomainModels;

namespace AnkiFlashCards.Data
{
    public class RikkiFlashCardsDbContext : IdentityDbContext
    {
        public RikkiFlashCardsDbContext(DbContextOptions<RikkiFlashCardsDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .Property("Front")
                .HasColumnType("TEXT");

            modelBuilder.Entity<Card>()
                .Property("Back")
                .HasColumnType("TEXT");

            modelBuilder.Entity<Revision>()
                    .Property(b => b.CardOrderList)
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<int>>(v));

            //https://docs.microsoft.com/en-us/ef/core/querying/filters
            modelBuilder.Entity<Subject>().HasQueryFilter(s => s.IsDeleted == false);
            modelBuilder.Entity<Resource>().HasQueryFilter(r => r.IsDeleted == false);
            modelBuilder.Entity<Deck>().HasQueryFilter(d => d.IsDeleted == false);
            modelBuilder.Entity<Card>().HasQueryFilter(c => c.IsDeleted == false);


            //https://www.learnentityframeworkcore.com/configuration/fluent-api/ondelete-method
            //this below cascade is not working - leaves orphans
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Resources)
                .WithOne(r=>r.Subject)
                .HasForeignKey(r=>r.SubjectId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Decks)
                .WithOne(d => d.Resource)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Deck>()
                .HasMany(d => d.Cards)
                .WithOne(c => c.Deck)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Deck>()
               .HasMany(d => d.Revisions)
               .WithOne(c => c.Deck)
               .OnDelete(DeleteBehavior.ClientCascade);

            //create needed indexes
            modelBuilder.Entity<Card>()
                   .HasIndex(c => new { c.Front, c.Back })
                   .HasName("IX_SearchCards");

            modelBuilder.Entity<Resource>()
                    .HasIndex(r => new { r.Title })
                    .HasName("IX_SearchResources");

            modelBuilder.Entity<Deck>()
                .HasIndex(d => d.Title)
                .HasName("IX_SearchDeck");
        }

    //    public static readonly ILoggerFactory MyLoggerFactory
    //= LoggerFactory.Create(builder => { builder.AddConsole(); });

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //=> optionsBuilder
    //    .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<FlashCardUser> FlashCardUsers { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }

    }
}
