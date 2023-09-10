
using Core.CrossCuttingConcerns.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class FileLogManager : ILogService
    {
        private Log CurrentLog { get; set; }
        private readonly IConfiguration _configuration;
        private readonly LogOptions _logOptions;
        public FileLogManager(IConfiguration configuration)
        {
            RefreshLog();
            _configuration = configuration;
            _logOptions = _configuration.GetSection("LogOptions").Get<LogOptions>();
        }

        public void RefreshLog()
        {
            CurrentLog = new Log();
        }

        public void AddOperation(Operation operation)
        {
            CurrentLog.Operations.Add(operation);
        }

        public void WriteLog()
        {
            string logPath;
            if (_logOptions.UseDefaultDirectory)
            {
                logPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            }
            else
            {
                logPath = _logOptions.LogDirectory;
            }
            CurrentLog.Date = DateTime.Now;
            string log = JsonSerializer.Serialize(CurrentLog);
            File.WriteAllText(logPath + "\\Logs\\log_" + CurrentLog.Date.DayOfYear + CurrentLog.Date.Hour + CurrentLog.Date.Minute + ".json", log);
        }
    }
}
