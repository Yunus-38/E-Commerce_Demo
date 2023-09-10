using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogOptions
    {
        public string LogDirectory { get; set; }
        public bool UseDefaultDirectory { get; set; }
    }
}
