
using System.Collections.Generic;

namespace FitnessTracker.Data.Entities
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }

        public ICollection<LogExercise> LogExercises { get; set; }
    }
}
