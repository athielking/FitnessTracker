using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Data.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LogExercise> logExercises { get; set; }
    }
}
