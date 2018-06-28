using System;
using UnityEngine;

namespace wLib
{
    public class UnityLogService : ILogService
    {
        private bool _doLog = true;

        public void SwitchLog(bool toggle)
        {
            _doLog = toggle;
        }

        public void Log(object log)
        {
            if (_doLog) { Debug.Log(log); }
        }

        public void LogError(object error)
        {
            if (_doLog) { Debug.LogError(error); }
        }

        public void LogWarning(object warning)
        {
            if (_doLog) { Debug.LogWarning(warning); }
        }

        public void LogException(Exception exception)
        {
            if (_doLog) { Debug.LogException(exception); }
        }
    }
}