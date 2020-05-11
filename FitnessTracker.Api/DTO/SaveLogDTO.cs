using System;

namespace FitnessTracker.DTO
{
    public class SaveLogDTO
    {
        public int? LogId { get; set; }

        public UserDTO User { get; set; }

        public string Comments { get; set; }

        public DateTime Created { get; set; }

        public LogExerciseDTO LogExercise { get; set; }
    }
}
