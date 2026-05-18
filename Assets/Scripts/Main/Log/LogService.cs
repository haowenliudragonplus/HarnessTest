using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class LogService
    {
        private LogService()
        {
        }

        private static List<ILog> logList = new List<ILog>();

        public static bool Enable { get; private set; } = true;
        public static ELogType LogTypeMask { get; private set; } = (ELogType)~0;
        public static ELogTag LogTagMask { get; private set; } = (ELogTag)~0;

        public static void Init(bool enable, ELogType logTypeMask, ELogTag logTagMask)
        {
            Enable = enable;
            LogTypeMask = logTypeMask;
            LogTagMask = logTagMask;
        }

        public static void SetEnable(bool enable)
        {
            Enable = enable;
        }

        public static void Add(ILog log)
        {
            logList.Add(log);
            log.Init();
        }

        public static void Remove(ILog log)
        {
            logList.Remove(log);
            log.Dispose();
        }

        public static void Dispose()
        {
            foreach (var fileLog in logList)
            {
                fileLog?.Dispose();
            }
        }
    }
}