using FitnessTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Repositories
{
    public interface IWorkoutRepository
    {
        Workout Create(Workout workout);

        Workout Delete(string id);

        IEnumerable<Workout> GetAllWorkouts();

        Workout GetWorkoutById(string id);

        Workout GetWorkoutByName(string name);

        int Count();

        Workout Update(Workout workout);
    }
}
