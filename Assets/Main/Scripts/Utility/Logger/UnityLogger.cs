
using System.Text;
using UnityEngine;

namespace LetMeOut.Utility.Logs
{
    public class UnityLogger : ILogService
    {
        private const string DEFAULT_TAG_NAME = "[LMO] ";
        private const string WARN_TAG_NAME = "[LMO-WARN] ";
        private const string ERROR_TAG_NAME = "[LMO-ERR] ";

        private StringBuilder builder = new();

        public void Message(string message)
        {
            builder.Clear()
                .Append(DEFAULT_TAG_NAME)
                .Append(message);

            Debug.Log(builder.ToString());
        }

        public void Warn(string message)
        {
            builder.Clear()
                .Append(WARN_TAG_NAME)
                .Append(message);

            Debug.LogWarning(builder.ToString());
        }

        public void Error(string message)
        {
            builder.Clear()
                .Append(ERROR_TAG_NAME)
                .Append(message);

            Debug.LogError(builder.ToString());
        }
    }
}