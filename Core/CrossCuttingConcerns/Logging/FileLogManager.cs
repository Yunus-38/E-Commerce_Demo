
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
                logPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\Business\\Logs";
            }
            else
            {
                logPath = _logOptions.LogDirectory + "\\Logs";
            }
            CurrentLog.Date = DateTime.Now;
            string log = JsonSerializer.Serialize(CurrentLog);
            bool directoryExists = Directory.Exists(logPath);
            if (!directoryExists)
            {
                Directory.CreateDirectory(logPath);
            }
            File.WriteAllText(logPath + "\\log_" + CurrentLog.Date.ToString().Replace(':','.') + ".json", log);
        }
    }
}
