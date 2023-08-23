using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.BusinessWork
{
    public class BusinessRuleAttribute : Attribute
    {
        public string[] Methods { get; set; }

        public BusinessRuleAttribute(params string[] methods)
        {
            Methods = methods;
        }
    }
}
