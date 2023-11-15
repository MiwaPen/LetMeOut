namespace LetMeOut.Utility.Logs
{
    public interface ILog
    {
        void Message(string message);
        void Warn(string message);
        void Error(string message);
    }
}