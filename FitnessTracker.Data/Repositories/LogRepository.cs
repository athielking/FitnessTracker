using System.Collections.Generic;
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
                .Where(q => q.LogId == id).FirstOrDefault();
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
            //_db.UpdateRange(log);
            //_db.Update(log);

            //// update user ref
            //_db.Entry(log).Reference(u => u.User).IsModified = true;
            //_db.Entry(log).Collection<LogExercise>(u => u.LogExercises).IsModified = true;

            Log tmp = GetLogById(log.LogId);
            tmp.Comments = log.Comments;
           // var x = tmp.LogExercises.Where(s => s.LogId.Equals(log.LogId)).FirstOrDefault();
           // x = 
            //x = 
            var logExecise = log.LogExercises.First();
            foreach (var obj in tmp.LogExercises.Where(w => w.LogId == log.LogId))
            {
                obj.Reps = logExecise.Reps;
                obj.Weight = logExecise.Weight;
                obj.TargetRep = logExecise.TargetRep;
            }

            //tmp.LogExercises.Select(c => { c.Weight = 50; return c; }).ToList();

            //var y = log.LogExercises.First();
            //x.Weight = y.Weight;
            //x.Reps = y.Reps;
            //x.TargetRep = y.TargetRep;

            //bool tracking = _db.ChangeTracker.Entries<Log>().Any(x => x.Entity.LogId == log.LogId);
            //bool tracking1 = _db.ChangeTracker.Entries<LogExercise>().Any(x => x.Entity.Weight == log.LogExercises.First().Weight);
            //_db.Update(log);

            //_db.Entry(log).Reference(u => u.User).IsModified = true;

            //_db.Entry(tmp).State = EntityState.Modified;
            //_db.DetachAllEntities();

            //_db.Entry(tmp).CurrentValues.SetValues(log);

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
