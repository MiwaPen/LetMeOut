namespace LetMeOut.Utility.Logs
{
    public interface ILogService
    {
        void Message(string message);
        void Warn(string message);
        void Error(string message);
    }
}