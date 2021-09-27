using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;

namespace Airports
{
    public static class Log
    {
        const string LogFilePath = "log.txt";
        public static Logger Logger = new LoggerConfiguration().WriteTo
            .File(LogFilePath, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}").CreateLogger();

    }
}
