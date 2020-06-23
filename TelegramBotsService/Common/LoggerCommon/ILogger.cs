namespace LoggerCommon
{
    public interface ILogger : NLog.ILogger
    {
        void Shutdown();
    }
}