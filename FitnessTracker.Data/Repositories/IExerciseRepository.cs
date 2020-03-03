using System.Collections.Generic;

using FitnessTracker.Core.Entities;

namespace FitnessTracker.Data.Repositories
{
    public interface IExerciseRepository
    {
        Exercise Create(Exercise exercise);        
        
        IEnumerable<Exercise> GetAllExercises();

        Exercise GetExerciseById(int id);

        Exercise GetExerciseByName(string name);

        int Count();

        Exercise Update(Exercise exercise);

        Exercise Delete(int id);
    }
}
