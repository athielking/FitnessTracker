using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using FitnessTracker.Core.Entities;
using System;

namespace FitnessTracker.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly FitnessTrackerContext _db;

        public LogRepository(FitnessTrackerContext db)
        {
            _db = db;
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

        public Log GetLogById(int id)
        {
            return _db.Logs
                .AsNoTracking()
                .Include(user => user.User)
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Where(q => q.LogId == id)
                .SingleOrDefault();
        }

        public Log GetLogByUserId(int id)
        {
            return _db.Logs
                .AsNoTracking()
                .Include(user => user.User)
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .FirstOrDefault(q => q.LogId == id);
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

        public IEnumerable<Log> GetLogsBySet(int id, DateTime date)
        {
            return _db.Logs
                .AsNoTracking()
                .Include(user => user.User)
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Where(obj => obj.User.Id == id && obj.Created.Equals(date))
                .ToList();
        }

        public Log CreateLog(Log log)
        {
            _db.Attach(log).State = EntityState.Added;
            _db.SaveChanges();
            return log;
        }

        public int GetLogCount()
        {
            var logs = _db.Logs.ToList();

            return logs.Count();
        }

        public Log Update(Log log)
        {         
            Log tmp = GetLogById(log.LogId);
            tmp.Comments = log.Comments;
            tmp.Modified = log.Modified;

            var logExecise = log.LogExercises.First();
            foreach (var obj in tmp.LogExercises.Where(w => w.LogId == log.LogId))
            {
                obj.Reps = logExecise.Reps;
                obj.Weight = logExecise.Weight;
                obj.TargetRep = logExecise.TargetRep;
            }

            _db.Update(tmp);
            var z = _db.SaveChanges();

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
