using System;
using System.Collections.Generic;
using System.Text;

namespace PriceBasket
{
    public interface ILogsService
    {
        public void AddToLogs(string msg);

        public IList<string> GetLogs();
    }
}
