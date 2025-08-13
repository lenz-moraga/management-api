using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Context
{
    public class BorrowingContext(DbContextOptions<BorrowingContext> options) : DbContext(options)
    {
        // DbSets for each model
        public DbSet<Item> Items { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<MovementDetail> MovementDetails { get; set; }
        public DbSet<MovementType> MovementTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filters
            modelBuilder.Entity<Item>().HasQueryFilter(i => i.DeletedAt == null);
            modelBuilder.Entity<Movement>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<MovementDetail>().HasQueryFilter(md => md.DeletedAt == null);
            modelBuilder.Entity<MovementType>().HasQueryFilter(mt => mt.DeletedAt == null);
            modelBuilder.Entity<Role>().HasQueryFilter(role => role.DeletedAt == null);
            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);

            // Relationships and keys
            // User - Movements
            modelBuilder.Entity<User>()
                .HasMany(u => u.Movements)
                .WithOne(m => m.RequestedBy)
                .HasForeignKey(m => m.RequestedByUserId);

            // User - Roles
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));

            // Item - Movements
            modelBuilder.Entity<Item>()
                .HasMany(i => i.MovementDetails)
                .WithOne(md => md.Item)
                .HasForeignKey(md => md.ItemId);

            // Movement - MovementDetails
            modelBuilder.Entity<Movement>()
                .HasMany(m => m.MovementDetails)
                .WithOne(md => md.Movement)
                .HasForeignKey(md => md.MovementId);

            // MovementDetail - Item
            modelBuilder.Entity<MovementDetail>()
                .HasOne(md => md.Item)
                .WithMany(i => i.MovementDetails)
                .HasForeignKey(md => md.ItemId);

            // MovementDetail - MovementType
            modelBuilder.Entity<MovementDetail>()
                .HasOne(md => md.Movement)
                .WithMany(m => m.MovementDetails)
                .HasForeignKey(md => md.MovementId);

            // Primary keys
            modelBuilder.Entity<Item>().HasKey(i => i.Id);
            modelBuilder.Entity<Movement>().HasKey(m => m.Id);
            modelBuilder.Entity<MovementDetail>().HasKey(m => m.Id);
            modelBuilder.Entity<MovementType>().HasKey(mt => mt.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Database level constraints
            // Constrains : Item
            modelBuilder.Entity<Item>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Item>()
                .Property(i => i.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Item>()
                .Property(i => i.CurrentStock)
                .IsRequired()
                .HasDefaultValue(0);

            // Constraints: Movement
            modelBuilder.Entity<Movement>()
                .Property(m => m.Comment)
                .HasMaxLength(500);

            // Constraints: MovementDetail
            modelBuilder.Entity<MovementDetail>()
                .ToTable(t => t.HasCheckConstraint("CK_MovementDetail_Quantity_Positive", "\"Quantity\" > 0"));

            // Constraints: MovementType
            modelBuilder.Entity<MovementType>()
                .Property(mt => mt.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Constraints: Role
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Constraints: User
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}
