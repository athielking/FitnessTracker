using FitnessTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.DTO
{
    public class LogExerciseDTO
    {
        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        public int Reps { get; set; }

        public int Weight { get; set; }

        public int TargetRep { get; set; }

    }
}
