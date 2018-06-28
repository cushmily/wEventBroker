using System;

namespace wLib
{
    public interface ILogService
    {
        void SwitchLog(bool toggle);

        void Log(object log);

        void LogError(object error);

        void LogWarning(object warning);

        void LogException(Exception exception);
    }
}