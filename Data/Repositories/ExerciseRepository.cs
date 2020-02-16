
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using FitnessTracker.Data.Entities;

namespace FitnessTracker.Data.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly FitnessTrackerContext _db;

        public ExerciseRepository(FitnessTrackerContext db)
        {
            _db = db;
        }

        public Exercise Create(Exercise exercise)
        {
            _db.Attach(exercise).State = EntityState.Added;
            _db.SaveChanges();
            return exercise;
        }

        public Exercise Delete(int id)
        {
            if (GetExerciseById(id) == null) return null;
            
            var resdults = _db.Remove(new Exercise { ExerciseId = id }).Entity;
            _db.SaveChanges();
            return resdults;
        }

        public IEnumerable<Exercise> GetAllExercises()
        {
            return _db.Exercises.OrderBy(e => e.Name).ToList();
        }

        public Exercise GetExerciseById(int id)
        {
            return _db.Exercises
                .FirstOrDefault(ex => ex.ExerciseId == id);
        }

        public Exercise GetExerciseByName(string name)
        {
            return _db.Exercises
                .FirstOrDefault(ex=> ex.Name.Equals(name));
        }

        public int Count()
        {
            return _db.Exercises.Count();
        }

        public Exercise Update(Exercise exerciseUpdate)
        {
            var exercises = new List<LogExercise>();
            if (exerciseUpdate.LogExercises != null)
            {
                exercises = new List<LogExercise>(exerciseUpdate.LogExercises);
            }

            _db.Attach(exerciseUpdate).State = EntityState.Modified;

            // replace all exercises with updated exercise information
            _db.LogExercises.RemoveRange(
                _db.LogExercises.Where(ex => ex.ExerciseId == exerciseUpdate.ExerciseId)
            );

            // update exercises with the updated exercise information
            foreach (var exercise in exercises)
            {
                _db.Entry(exercise).State = EntityState.Added;
            }

            _db.SaveChanges();

            return exerciseUpdate;
        }
    }
}
