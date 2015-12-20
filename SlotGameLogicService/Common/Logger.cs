using log4net;

namespace Log
{
    public static class Logger
    {
        static ILog logger
        {
            get; set;
        }

        static Logger()
        {
            logger = LogManager.GetLogger("logger");
        }

        public static void Fatal(object message)
        {
            logger.Fatal(message);
        }

        public static void Error(object message)
        {
            logger.Fatal(message);
        }

        public static void Warn(object message)
        {
            logger.Fatal(message);
        }

        public static void Info(object message)
        {
            logger.Fatal(message);
        }

        public static void Debug(object message)
        {
            logger.Fatal(message);
        }
    }
}
