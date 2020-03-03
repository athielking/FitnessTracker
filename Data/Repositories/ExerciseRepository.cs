
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
            var exerciseSaved = _db.Exercises.Add(exercise).Entity;
            _db.SaveChanges();
            return exerciseSaved;
        }

        public Exercise Delete(int id)
        {
            var exercise = GetExerciseById(id);
            if (exercise == null) return null;
            
            var results = _db.Remove(exercise).Entity;
            _db.SaveChanges();
            return results;
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
            _db.Update(exerciseUpdate);
            _db.SaveChanges();

            return exerciseUpdate;
        }
    }
}
