
using System;
using System.Collections.Generic;

namespace FitnessTracker.Core.Entities
{
    public class Log
    {
        public int LogId { get; set; }

        public string UserId { get; set; }

        public string WorkoutId { get; set; }

        public User User { get; set; }

        public Workout Workout { get; set; }

        public int Set { get; set; }

        public string SetId { get; set; }

        public string Comments { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<LogExercise> LogExercises { get; set; }
    }
}
