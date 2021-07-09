using System;

namespace FitnessTracker.DTO
{
    public class SaveLogDTO
    {
        public int? LogId { get; set; }

        public UserDTO User { get; set; }

        public int Set { get; set; }

        public string SetId { get; set; }

        public string Comments { get; set; }

        public string Created { get; set; }

        public LogExerciseDTO LogExercise { get; set; }
    }
}
