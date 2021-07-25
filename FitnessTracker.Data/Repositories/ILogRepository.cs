using System;
using System.Collections.Generic;

using FitnessTracker.Core.Entities;

namespace FitnessTracker.Data.Repositories
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetAllLogs();

        IEnumerable<Log> GetAllLogs(string id);

        Log GetLogById(string userId, int id);

        IEnumerable<Log> GetLogsBySet(string id, DateTime date);

        Log CreateLog(Log log);

        Log Update(string userId, Log log);

        Log Delete(string userId, int id);

        int GetLogCount();

        Log GetLastRecord();
    }
}
