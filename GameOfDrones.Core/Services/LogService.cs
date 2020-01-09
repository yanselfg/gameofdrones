using GameOfDrones.Core.Entities;
using GameOfDrones.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfDrones.Core.Services
{
    /// <summary>
    /// LogService
    /// </summary>
    public partial class LogService : ILogService
    {
        #region Fields

        private readonly IRepository<Log> _logRepository;

        #endregion

        #region Ctor

        public LogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        #endregion

        #region Methods

        public void InsertLog(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _logRepository.Insert(log);
        }

        public void UpdateLog(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _logRepository.Update(log);
        }

        public void DeleteLog(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _logRepository.Delete(log);
        }

        public Log GetLogById(int logId)
        {
            if (logId == 0)
            {
                return null;
            }

            return _logRepository.GetById(logId);
        }

        public Log[] GetLogsByIds(int[] logIds)
        {
            if (logIds == null)
            {
                throw new ArgumentNullException(nameof(logIds));
            }
            else if (logIds.Count() == 0)
            {
                return null;
            }

            var logs = new List<Log>();

            foreach (var id in logIds)
            {
                var log = _logRepository.GetById(id);

                if (log != null)
                {
                    logs.Add(log);
                }
            }

            return logs.ToArray();
        }

        public IList<Log> GetAllLogs(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _logRepository.Table;
            var logs = query.Skip(pageIndex * pageSize).Take(pageSize).OrderBy(l => l.Date).ToList();

            return logs;
        }

        #endregion
    }
}
