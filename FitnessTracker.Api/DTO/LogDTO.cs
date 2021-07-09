using System;
using System.Collections.Generic;

namespace FitnessTracker.DTO
{
    public class LogDTO
    {
        public int LogId { get; set; }

        public UserDTO User { get; set; }

        public int Set { get; set; }

        public string SetId { get; set; }

        public string Comments { get; set; }

        public DateTime Created { get; set; }

        public ICollection<LogExerciseDTO> LogExercises { get; set; }
    }
}
