namespace Bioworld.Logging
{
    using System.Xml.Linq;

    public class LoggingService : ILoggingService
    {
    }

    public interface ILoggingService
    {
        public void SetLoggingLevel(string logEventLevel) =>
            Extensions.LoggingLevelSwitch.MinimumLevel = Extensions.GetLogEventLevel(logEventLevel);
    }
}