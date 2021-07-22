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

        public Workout Delete(string id)
        {
            var workout = GetWorkoutById(id);
            if (workout == null) return null;
            
            var results = _db.Remove(workout).Entity;
            _db.SaveChanges();
            return results;
        }

        public IEnumerable<Workout> GetAllWorkouts()
        {
            return _db.Workouts.AsNoTracking().OrderBy(e => e.Name).ToList();
        }

        public Workout GetWorkoutById(string id)
        {
            return _db.Workouts
                .AsNoTracking()
                .Include(u => u.Logs)
                .FirstOrDefault(ex => ex.id.Equals(id));
        }

        public Workout GetWorkoutByName(string name)
        {
            return _db.Workouts
                .AsNoTracking()
                .Include(u => u.Logs)
                .FirstOrDefault(ex=> ex.Name.Equals(name));
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
