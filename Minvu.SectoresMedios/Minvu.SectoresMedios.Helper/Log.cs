using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Minvu.SectoresMedios.Helper
{
    public class Log
    {
        public static Logger Instance { get; private set; }
        static Log()
        {
            LogManager.ReconfigExistingLoggers();
            Instance = LogManager.GetCurrentClassLogger();
        }

    }
}
