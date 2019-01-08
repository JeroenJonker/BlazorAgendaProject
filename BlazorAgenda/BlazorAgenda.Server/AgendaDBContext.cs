using Microsoft.EntityFrameworkCore;

namespace BlazorAgenda.Shared.Models
{
    public partial class AgendaDBContext : DbContext
    {
        public AgendaDBContext()
        {
        }

        public AgendaDBContext(DbContextOptions<AgendaDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=AgendaDB;Trusted_Connection=True;User ID=ADMINLOG;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.Userid);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Emailadress)
                    .HasName("UQ__User__3A372016376838CC")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Emailadress)
                    .IsRequired()
                    .HasColumnName("EMAILADRESS")
                    .HasMaxLength(40);

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(40);

                entity.Property(e => e.Isadmin).HasColumnName("ISADMIN");

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(40);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(65);
            });
        }
    }
}
