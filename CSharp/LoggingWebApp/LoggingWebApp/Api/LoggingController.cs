using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using System.IO;
using System.Reflection;
using ILogger = Serilog.ILogger;

namespace LoggingWebApp.Api
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _microsoftLogger;
        private readonly ILogger _serilogLogger;

        public LoggingController(ILogger<LoggingController> microsoftLogger, ILogger serilogLogger)
        {
            _microsoftLogger = microsoftLogger;
            _serilogLogger = serilogLogger.ForContext<LoggingController>();
        }

        [HttpGet("[action]")]
        public IActionResult Microsoft()
        {
            _microsoftLogger.LogTrace("Microsoft Trace");
            _microsoftLogger.LogDebug("Microsoft Debug");
            _microsoftLogger.LogInformation("Microsoft Information");
            _microsoftLogger.LogWarning("Microsoft Warning");
            _microsoftLogger.LogError("Microsoft Error");
            _microsoftLogger.LogCritical("Microsoft Critical");

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Serilog()
        {
            _serilogLogger.Verbose("Serilog Verbose");
            _serilogLogger.Debug("Serilog Debug");
            _serilogLogger.Information("Serilog Information");
            _serilogLogger.Warning("Serilog Warning");
            _serilogLogger.Error("Serilog Error");
            _serilogLogger.Fatal("Serilog Fatal");

            return Ok();
        }

        [HttpPost("{level}")]
        public IActionResult LogLevel(LogEventLevel level)
        {
            var serilogSettingsFile = Path.Combine(Directory.GetParent( Assembly.GetEntryAssembly().Location).FullName, "serilogsettings.json");
            JObject jobject;
            using (var reader = new StreamReader(serilogSettingsFile))
            using (var jsonReader = new JsonTextReader(reader))
            {
                jobject = JObject.Load(jsonReader);
                jobject.SelectToken("Serilog.MinimumLevel.Default").Replace(level.ToString());
            }

            using (var writer = new StreamWriter(serilogSettingsFile))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jobject.WriteTo(jsonWriter);
            }

            return Ok();
        }
    }
}
