﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using FitnessTracker.Core.Entities;

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
                //.AsNoTracking()
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
