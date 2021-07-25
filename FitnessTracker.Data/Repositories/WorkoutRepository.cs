using System.Collections.Generic;
using System.Linq;

using FitnessTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FitnessTrackerContext _db;

        public WorkoutRepository(FitnessTrackerContext db)
        {
            _db = db;
        }

        public Workout Create(Workout workout)
        {
            var workoutSaved = _db.Workouts.Add(workout).Entity;
            _db.SaveChanges();
            return workoutSaved;
        }

        public Workout Delete(string userId, string id)
        {
            var workout = GetWorkoutById(userId, id);
            if (workout == null) return null;
            
            var results = _db.Remove(workout).Entity;
            _db.SaveChanges();
            return results;
        }

        public IEnumerable<Workout> GetAllWorkouts(string userId)
        {
            return _db.Workouts.AsNoTracking()
                .Include(x => x.Logs)
                .ThenInclude(x => x.UserId.Equals(userId))
                .ToList();
        }

        public Workout GetWorkoutById(string userId, string id)
        {
            return _db.Workouts
                .AsNoTracking()
                .Where( x => x.id.Equals(id))
                .Include(x => x.Logs)
                .ThenInclude( x => x.UserId.Equals(userId))
                .FirstOrDefault(ex => ex.id.Equals(id));
        }

        public int Count()
        {
            return _db.Workouts.Count();
        }

        public Workout Update(Workout workout)
        {
            _db.Update(workout);
            _db.Entry(workout).Reference(u => u.Logs).IsModified = true;
            _db.SaveChanges();

            return workout;
        }
    }
}
