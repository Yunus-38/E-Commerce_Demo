using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class Operation
    {
        public string MethodName { get; set; }
        public object[] Arguments { get; set; }
        public object ReturnValue { get; set; }
        public DateTime Date { get; set; }
    }
}
