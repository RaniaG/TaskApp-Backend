using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Logging;

namespace Test.NoAuth.Web.Hangfire.CustomLog
{
    public class CustomLogger : ILog
    {
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            
        }
    }
}
