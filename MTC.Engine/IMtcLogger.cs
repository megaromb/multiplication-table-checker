using System;

namespace MTC.Engine
{
    public interface IMtcLogger
    {
        void Info(string message);
        void Info(string format, params object[] args);

        void Debug(string message);
        void Debug(string format, params object[] args);

        void Error(Exception e);
        void Error(Exception e, string message);
        void Error(Exception e, string format, params object[] args);
        void Error(string message);
        void Error(string format, params object[] args);
    }
}
