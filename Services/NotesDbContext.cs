using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Blazor_Notes.Options;
using Blazor_Notes.Models;
using Microsoft.Extensions.Options;

#nullable disable

namespace Blazor_Notes.Services
{
    public partial class NotesDbContext : DbContext
    {
        private readonly DatabaseOptions _options;

        public NotesDbContext(IOptions<DatabaseOptions> options)
        {
            this._options = options.Value;
        }

        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_options.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("notes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
