namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Models.Logs;

    public class LogService : ILogService
    {
        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Log(string userId, LogOperation operation, string modifiedTable, DateTime modificationDate)
        {
            var log=  new Log
            {
                UserId = userId,
                Operation = operation,
                ModifiedTable = modifiedTable,
                ModificationDate = modificationDate
            };

            this.db.Add(log);
            this.db.SaveChanges();
        }

        public IEnumerable<LogModel> All(int pageNumber, int pageSize, string searchUsername)
        {
            var logs = this.db
                .Logs
                .Select(l => new LogModel
                {
                    Id = l.Id,
                    ModificationDate = l.ModificationDate,
                    ModifiedTable = l.ModifiedTable,
                    Operation = l.Operation,
                    User = l.User.UserName
                })
                .ToList();

            if (!string.IsNullOrEmpty(searchUsername))
            {
                logs = logs
                    .Where(l => l.User.ToLower() == searchUsername.ToLower())
                    .ToList();
            }

            logs = logs
                .OrderBy(l => l.ModificationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return logs;
        }

        public int Total(string userName)
        {
            var logs = this.db
                .Logs
                .AsQueryable();

            if (!string.IsNullOrEmpty(userName))
            {
                logs = logs
                    .Where(l => l.User.UserName == userName);
            }

            return logs.Count();
        }

        public void Clear()
        {
            this.db.Logs.RemoveRange(this.db.Logs);

            this.db.SaveChanges();
        }
    }
}
