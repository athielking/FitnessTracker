using System;
using System.Threading.Tasks;
using FitnessTracker.Api.Models;
using Microsoft.Graph;

namespace FitnessTracker.Api.Services
{
    public interface IMicrosoftGraphService
    {
        Task<GraphServiceClient> GetGraphServiceClient();
        Task<UserAD> GetLoggedInUser();

        Task<UserAD> GetUserById(string id);

        Task<UserAD> Search(string key);

        Task<UserAD> Create(UserAD user);

        Task<bool> UpdatePassword(string password);

        Task<UserAD> Update(UserAD user);

        Task<bool> Delete(string id);
    }
}
