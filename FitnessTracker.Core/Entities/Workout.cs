using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Core.Entities
{
    public class Workout
    {
        public string id { get; set; }

        public string Name { get; set; }

        public ICollection<Log> Logs { get; set; }
    }
}
