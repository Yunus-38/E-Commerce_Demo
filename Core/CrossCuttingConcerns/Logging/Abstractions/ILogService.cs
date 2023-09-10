using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Abstractions
{
    public interface ILogService
    {
        public void RefreshLog();
        public void WriteLog();
        public void AddOperation(Operation operation);
    }
}
