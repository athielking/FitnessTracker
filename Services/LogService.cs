using System.Collections.Generic;
using System.IO;

using FitnessTracker.Data.Entities;
using FitnessTracker.Data.Repositories;

namespace FitnessTracker.Services
{
    /**
     * Service layer to process and return the results retreived from the repositories
     */
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public LogService(ILogRepository logRepository, IUserRepository userRepository)
        {
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public Log CreateLog(Log log)
        {
            if(log.User == null || log.User.Id <= 0)
                throw new InvalidDataException("To create a Log you need a User");
            
            if(_userRepository.GetById(log.User.Id) == null)
                throw new InvalidDataException("User not found");

            return _logRepository.CreateLog(log);
        }

        public Log New()
        {
            return new Log();
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return _logRepository.GetAllLogs();
        }

        public IEnumerable<Log> GetLogsByUserId(int id)
        {
            return _logRepository.GetLogsByUserId(id);
        }

        public IEnumerable<Log> GetLogsByUserName(string username)
        {
            return _logRepository.GetLogsByUserName(username);
        }

        public Log UpdateLog(Log log)
        {
            return _logRepository.Update(log);
        }

        public Log DeleteLog(int id)
        {
            return _logRepository.Delete(id);
        }
    }
}
