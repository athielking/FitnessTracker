using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Data.Entities
{
    public class Log
    {
        public int LogId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int Set { get; set; }

        public string Comments { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<LogExercise> LogExercises { get; set; }
    }
}
