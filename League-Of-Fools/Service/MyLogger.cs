using NLog;

namespace League_Of_Fools.Services
{
    public class MyLogger
    {
        private static MyLogger instance;
        private static Logger logger;

        public static MyLogger GetInstance()
        {
            if (instance == null)
                instance = new MyLogger();
            return instance;
        }
        private Logger GetLogger()
        {
            if (logger == null)
                MyLogger.logger = NLog.LogManager.GetLogger("League-Of-Fools_Logger");
            return logger;
        }

        public void Error(string className, string message, string error)
        {
            GetLogger().Error("Class: "+ className + ", Message: "+ message + ", Error: " + error );
        }

        public void Info(string className, string message)
        {
            GetLogger().Info("Class: " + className + ", Message: " + message);

        }
    }
}
