using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class Log
    {
        public List<Operation> Operations { get; set; }
        public DateTime Date { get; set; }
        public Log()
        {
            Operations = new();
        }
    }
}
