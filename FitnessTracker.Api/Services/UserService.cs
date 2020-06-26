using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;

using FitnessTracker.Core.Entities;
using FitnessTracker.Data.Repositories;





using System.Threading.Tasks;

namespace FitnessTracker.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Count()
        {
            return _userRepository.Count();
        }

        public User Create(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.PasswordHash == null)
                throw new ArgumentNullException(nameof(user.PasswordHash));

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, user.PasswordHash);

            return _userRepository.Create(user);

        }

        public User Delete(string id)
        {
            return _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public User GetByUsernameWithAllLogs(string username)
        {
            return _userRepository.GetByUsernameWithAllLogs(username);
        }

        public User SignIn(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null) throw new InvalidDataException("User Not Found");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed) throw new AuthenticationException("User failed to log in");

            return user;
        }

        public User SignIn(User user, string password)
        {
            throw new NotImplementedException();
        }

        public User Update(User userUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
