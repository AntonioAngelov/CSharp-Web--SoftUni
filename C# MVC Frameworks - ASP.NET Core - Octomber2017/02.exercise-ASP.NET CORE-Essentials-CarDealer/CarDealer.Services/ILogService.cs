namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Models.Logs;

    public interface ILogService
    {
        void Log(string userId,
            LogOperation operation,
            string modifiedTable,
            DateTime modificationDate);

        IEnumerable<LogModel> All(int pageNumber, int pageSize, string searchUsername);

        int Total(string userName);

        void Clear();
    }
}
