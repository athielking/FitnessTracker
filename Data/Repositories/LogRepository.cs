
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTracker.Data.Entities;

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly FitnessTrackerContext _db;
        private IMapper _mapper;

        public LogRepository(FitnessTrackerContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return _db.Logs
                .AsNoTracking()
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Include(user => user.User)
                .OrderBy( q => q.UserId)
                .ToList();
        }

        public Log GetLastRecord()
        {
            var x = _db.Logs.OrderByDescending(q => q.LogId).Take(1);

            return x.FirstOrDefault();
        }

        public IEnumerable<Log> GetLogsByUserId(int id)
        {
            return _db.Logs
                .AsNoTracking()
                .Include(user => user.User)
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Where(q => q.User.Id == id)
                .ToList();
        }

        public Log GetLogByUserId(int id)
        {
            return _db.Logs
                .AsNoTracking()
                .Include(user => user.User)
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Where(q => q.User.Id == id).FirstOrDefault();
        }

        public IEnumerable<Log> GetLogsByUserName(string username)
        {
            return _db.Logs
                .AsNoTracking()
                .Include(user => user.User)
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Where( q => q.User.Username.Equals(username))
                .ToList();
        }

        public Log CreateLog(Log log)
        {
            var logSaved = _db.Logs.Add(log).Entity;
            _db.SaveChanges();
            return logSaved;
        }

        public int GetLogCount()
        {
            var logs = _db.Logs.ToList();

            return logs.Count();
        }

        public Log Update(Log log)
        {
            _db.Update(log);

            //// update user ref
            _db.Entry(log).Reference(u => u.User).IsModified = true;
            var x = _db.SaveChanges();

            return log;
        }

        public Log Delete(int id)
        {
            var log = GetLogByUserId(id);
            if ( log == null) return null;

            var results = _db.Remove(log).Entity;
            _db.SaveChanges();
            return results;
        }

    }
}
