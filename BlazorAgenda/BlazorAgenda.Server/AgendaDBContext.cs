using System;
using BlazorAgenda.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorAgenda.Server.Models
{
    public partial class AgendaDBContext : DbContext
    {
        public AgendaDBContext()
        {
        }

        public AgendaDBContext(DbContextOptions<AgendaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=AgendaDB;Trusted_Connection=True;User ID=ADMINLOG;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Color)
                    .HasColumnName("COLOR")
                    .HasMaxLength(20);

                entity.Property(e => e.Emailadress)
                    .IsRequired()
                    .HasColumnName("EMAILADRESS")
                    .HasMaxLength(40);

                entity.Property(e => e.End)
                    .HasColumnName("END")
                    .HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(30);

                entity.Property(e => e.Start)
                    .HasColumnName("START")
                    .HasColumnType("datetime");

                entity.Property(e => e.Summary)
                    .HasColumnName("SUMMARY")
                    .HasMaxLength(30);

                entity.HasOne(d => d.EmailadressNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.Emailadress)
                    .HasConstraintName("FK_Event_User_USERID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Emailadress);

                entity.Property(e => e.Emailadress)
                    .HasColumnName("EMAILADRESS")
                    .HasMaxLength(40)
                    .ValueGeneratedNever();

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(40);

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(40);
            });
        }
    }
}
