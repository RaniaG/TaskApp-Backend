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
            if (messageFunc == null)
            {
                // Before calling a method with an actual message, LogLib first probes
                // whether the corresponding log level is enabled by passing a `null`
                // messageFunc instance.
                return logLevel > LogLevel.Info;
            }

            // Writing a message somewhere, make sure you also include the exception parameter,
            // because it usually contain valuable information, but it can be `null` for regular
            // messages.
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"./HangfireLog.txt", true))
            {
                file.WriteLine(String.Format("**At {0}: {1} {2} {3}",DateTime.Now.ToString(), logLevel, messageFunc(), exception));
            }

            // Telling LibLog the message was successfully logged.
            return true;
        }
    }
}
