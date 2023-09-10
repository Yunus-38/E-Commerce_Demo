using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Abstractions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        ILogService _logService;

        public LogController()
        {
            _logService = ServiceTool.ServiceProvider.GetService<ILogService>();
        }

        [HttpPost]
        public IActionResult SaveLog()
        {
            _logService.WriteLog();
            _logService.RefreshLog();
            return Ok();
        }
    }
}
