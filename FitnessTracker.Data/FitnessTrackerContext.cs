using Microsoft.EntityFrameworkCore;

using FitnessTracker.Core.Entities;
using System.Linq;

namespace FitnessTracker.Data
{
    public class FitnessTrackerContext : DbContext
    {
        public FitnessTrackerContext(DbContextOptions<FitnessTrackerContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<LogExercise> LogExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
           modelBuilder.Entity<LogExercise>()
                .HasKey(bc => new { bc.LogId, bc.ExerciseId });
           modelBuilder.Entity<LogExercise>()
                .HasOne(bc => bc.Log)
                .WithMany(b => b.LogExercises)
                .HasForeignKey(bc => bc.LogId);
            modelBuilder.Entity<LogExercise>()
                .HasOne(bc => bc.Exercise)
                .WithMany(c => c.LogExercises)
                .HasForeignKey(bc => bc.ExerciseId);
            modelBuilder.Entity<Log>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.Logs)
                .HasForeignKey(bc => bc.UserId);
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
