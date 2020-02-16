﻿using System.Collections.Generic;

using FitnessTracker.Data.Entities;

namespace FitnessTracker.Data.Repositories
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetAllLogs();

        IEnumerable<Log> GetLogsByUserId(int id);

        IEnumerable<Log> GetLogsByUserName(string username);

        Log CreateLog(Log log);

        Log Update(Log log);

        Log Delete(int id);
    }
}
