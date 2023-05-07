namespace Exam.Infrastructure.Logger
{
    public static class Logger
    {
        public static NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}