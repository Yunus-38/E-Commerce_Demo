using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Abstractions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogAspect : MethodInterception
    {
        private ILogService _logService;
        public LogAspect()
        {
            _logService = ServiceTool.ServiceProvider.GetService<ILogService>();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            Operation operation = new()
            {
                MethodName = invocation.Method.Name,
                Arguments = invocation.Arguments,
                ReturnValue = invocation.ReturnValue,
                Date = DateTime.Now
            };
            _logService.AddOperation(operation);

        }
    }
}
