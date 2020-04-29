using System.Collections.Generic;

using FitnessTracker.Core.Entities;

namespace FitnessTracker.Services
{
    public interface ILogService
    {
        Log CreateLog(Log log);

        Log New();

        IEnumerable<Log> GetAllLogs();

        IEnumerable<Log> GetLogsByUserId(int id);

        IEnumerable<Log> GetLogsByUserName(string username);

        Log UpdateLog(Log log);

        Log DeleteLog(int id);

        int GetLogCount();

        Log GetLastRecord();
    }
}
