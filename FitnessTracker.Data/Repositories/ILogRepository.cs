using System;
using System.Collections.Generic;

using FitnessTracker.Core.Entities;

namespace FitnessTracker.Data.Repositories
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetAllLogs();

        Log GetLogById(int id);

        IEnumerable<Log> GetLogsByUserName(string username);

        IEnumerable<Log> GetLogsBySet(int id, DateTime date);

        Log CreateLog(Log log);

        Log Update(Log log);

        Log Delete(int id);

        int GetLogCount();

        Log GetLastRecord();
    }
}
