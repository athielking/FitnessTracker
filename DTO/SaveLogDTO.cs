using FitnessTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.DTO
{
    public class SaveLogDTO
    {
        public int? LogId { get; set; }

        public UserDTO User { get; set; }

        public int Set { get; set; }

        public string Comments { get; set; }

        public DateTime Created { get; set; }

        public LogExercise LogExercise { get; set; }
    }
}
