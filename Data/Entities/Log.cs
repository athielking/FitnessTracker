using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Data.Entities
{
    public class Log
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int Set { get; set; }

        public string Comments { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<LogExercise> logExercises { get; set; }
    }
}
