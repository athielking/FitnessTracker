using System.Collections.Generic;
using System.IO;
using System.Linq;

using FitnessTracker.Core.Entities;
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
        private readonly IExerciseRepository _exerciseRepository;

        public LogService(ILogRepository logRepository, IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _logRepository = logRepository;
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public Log CreateLog(Log log)
        {
            if(log.User == null)
                throw new InvalidDataException("To create a Log you need a User");
            
            if(_userRepository.GetById(log.User.Id) == null)
                throw new InvalidDataException("User not found");

            var exercise = _exerciseRepository.GetExerciseById(log.LogExercises.First().ExerciseId);
            log.LogExercises.First().Exercise = exercise;

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

        public Log GetLogById(int id)
        {
            return _logRepository.GetLogById(id);
        }

        public IEnumerable<Log> GetLogsByUserName(string username)
        {
            return _logRepository.GetLogsByUserName(username);
        }

        public Log UpdateLog(Log log)
        {
            var user = _userRepository.GetById(log.User.Id);
            log.User = user;
            return _logRepository.Update(log);
        }

        public Log DeleteLog(int id)
        {
            return _logRepository.Delete(id);
        }

        public int GetLogCount()
        {
            return _logRepository.GetLogCount();
        }

        public Log GetLastRecord()
        {
            return _logRepository.GetLastRecord();
        }
    }
}
