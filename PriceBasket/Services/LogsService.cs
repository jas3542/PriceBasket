using System;
using System.Collections.Generic;
using System.Text;

namespace PriceBasket
{
    public class LogsService : ILogsService
    {
        public IList<string> _logs;

        public LogsService() {

            _logs = new List<string>();
        }

        public void AddToLogs(string msg)
        {
            if (msg != null || msg != "")
            {
                _logs.Add(msg);
            }

        }

        public IList<string> GetLogs()
        {
            return _logs;
        }
    }
}
