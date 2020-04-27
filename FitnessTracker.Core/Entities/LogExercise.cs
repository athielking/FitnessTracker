
namespace FitnessTracker.Core.Entities
{
    public class LogExercise
    {
        public int LogId { get; set; }

        public int ExerciseId { get; set; }

        public Log Log { get; set; }

        public Exercise Exercise { get; set; }

        public int Reps { get; set; }

        public int Weight { get; set; }

        public int TargetRep { get; set; }
    }
}
