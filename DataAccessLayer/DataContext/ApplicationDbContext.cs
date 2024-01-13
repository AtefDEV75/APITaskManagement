using Task = DataAccessLayer.Entities.Task;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataContext
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                modelBuilder.Entity<Task>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("PK__Task__3214EC077BA3F84B");

                    entity.ToTable("Task");

                    entity.Property(e => e.Assignee).HasMaxLength(100);
                    entity.Property(e => e.DueDate).HasColumnType("datetime");
                    entity.Property(e => e.Status)
                        .HasMaxLength(50)
                        .HasDefaultValue("Not Started");
                    entity.Property(e => e.Title).HasMaxLength(255);
                });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}


