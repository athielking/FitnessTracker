using FitnessTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Repositories
{
    public interface IWorkoutRepository
    {
        Workout Create(Workout workout);

        Workout Delete(string userId, string id);

        IEnumerable<Workout> GetAllWorkouts(string userId);

        Workout GetWorkoutById(string userId, string id);

        int Count();

        Workout Update(Workout workout);
    }
}
