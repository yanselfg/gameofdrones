using GameOfDrones.Core.Entities;
using System.Collections.Generic;

namespace GameOfDrones.Core.Interfaces
{
    /// <summary>
    /// ILogService interface
    /// </summary>
    public partial interface ILogService
    {
        /// <summary>
        /// Inserts a Log
        /// </summary>
        /// <param name="log">Log</param>
        void InsertLog(Log log);

        /// <summary>
        /// Updates a Log
        /// </summary>
        /// <param name="log">Log</param>
        void UpdateLog(Log log);

        /// <summary>
        /// Deletes a Log
        /// </summary>
        /// <param name="log">Log</param>
        void DeleteLog(Log log);

        /// <summary>
        /// Gets a Player
        /// </summary>
        /// <param name="logId">Log identifier</param>
        /// <returns>Log</returns>
        Log GetLogById(int logId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logIds"></param>
        /// <returns></returns>
        Log[] GetLogsByIds(int[] logIds);

        /// <summary>
        /// List all Players
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<Log> GetAllLogs(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
