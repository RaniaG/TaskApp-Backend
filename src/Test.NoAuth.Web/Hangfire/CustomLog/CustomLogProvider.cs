using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Logging;
namespace Test.NoAuth.Web.Hangfire.CustomLog
{
    public class CustomLogProvider : ILogProvider
    {
        public ILog GetLogger(string name)
        {
            return new CustomLogger();
        }
    }
}
