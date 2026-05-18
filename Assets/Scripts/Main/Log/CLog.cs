using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Debug = UnityEngine.Debug;

namespace Framework
{
    /// <summary>
    /// Log类型
    /// </summary>
    /// 不需要自定义添加类型
    [Flags]
    public enum ELogType
    {
        Exception = 1 << 0,
        Info = 1 << 1,
        Warning = 1 << 2,
        Error = 1 << 3,
    }

    /// <summary>
    /// Log标签
    /// </summary>
    /// 可以自定义添加标签
    [Flags]
    public enum ELogTag
    {
        Nothing,
        Net = 1 << 0,
        UI = 1 << 1,
        LHW = 1 << 2,
        AppBuild = 1 << 3,
    }

    /// <summary>
    /// Log颜色
    /// </summary>
    public enum ELogColor
    {
        Default,

        Red,
        Green,
        Blue,
        Cyan,
        Yellow,
    }

    public static class CLog
    {
        private static StringBuilder stringBuilder;

        static CLog()
        {
            stringBuilder = new StringBuilder();
        }

        #region Log方法

        [Conditional("LOG_ENABLE")]
        public static void Exception(object message, ELogTag logTag = ELogTag.Nothing, ELogColor logColor = ELogColor.Default)
        {
            if (!IsEnable(ELogType.Exception, logTag))
                return;
            Exception ex = new Exception(GetOutputStr(message, ELogType.Exception, logTag, logColor));
            Debug.LogException(ex);
        }

        [Conditional("LOG_ENABLE")]
        public static void Info(object message, ELogTag logTag = ELogTag.Nothing, ELogColor logColor = ELogColor.Default)
        {
            if (!IsEnable(ELogType.Info, logTag))
                return;
            Debug.Log(GetOutputStr(message, ELogType.Info, logTag, logColor));
        }

        [Conditional("LOG_ENABLE")]
        public static void Warning(object message, ELogTag logTag = ELogTag.Nothing, ELogColor logColor = ELogColor.Default)
        {
            if (!IsEnable(ELogType.Warning, logTag))
                return;
            Debug.LogWarning(GetOutputStr(message, ELogType.Warning, logTag, logColor == ELogColor.Default ? ELogColor.Yellow : logColor));
        }

        [Conditional("LOG_ENABLE")]
        public static void Error(object message, ELogTag logTag = ELogTag.Nothing, ELogColor logColor = ELogColor.Default)
        {
            if (!IsEnable(ELogType.Error, logTag))
                return;
            Debug.LogError(GetOutputStr(message, ELogType.Error, logTag, logColor == ELogColor.Default ? ELogColor.Red : logColor));
        }

        #endregion Log方法

        #region 工具方法

        private static string GetOutputStr(object message, ELogType logType, ELogTag logTag = ELogTag.Nothing, ELogColor logColor = ELogColor.Default)
        {
            // 格式化字符串
            stringBuilder.Clear();
            if (message == null)
            {
                stringBuilder.Append("NULL");
                return stringBuilder.ToString();
            }
            IFormattable formattable = message as IFormattable;
            if (formattable != null)
            {
                stringBuilder.Append(formattable.ToString(null, CultureInfo.InvariantCulture));
                return stringBuilder.ToString();
            }
            stringBuilder.Append(message);

            // 输出内容中添加LogTag
            if (logTag != ELogTag.Nothing)
            {
                stringBuilder.Insert(0, $"<b>[{logTag}]</b> ");
            }

            // 输出内容中添加LogType
            stringBuilder.Insert(0, $"<b>[{logType}] ►</b> ");

            // 修改输出内容的颜色
            if (logColor != ELogColor.Default)
            {
                stringBuilder.Insert(0, $"<color={GetHexColor(logColor)}>");
                stringBuilder.Insert(stringBuilder.Length, "</color>");
            }

            return stringBuilder.ToString();
        }

        private static bool IsEnable(ELogType logType, ELogTag logTag)
        {
            if (!LogService.Enable)
                return false;
            if (!CheckLogType(logType))
                return false;
            if (!CheckLogTag(logTag))
                return false;
            return true;
        }

        private static bool CheckLogType(ELogType logType)
        {
            bool valid = (LogService.LogTypeMask & logType) != 0;
            return valid;
        }

        private static bool CheckLogTag(ELogTag logTag)
        {
            if (logTag == ELogTag.Nothing)
                return true;
            bool valid = (LogService.LogTagMask & logTag) != 0;
            return valid;
        }

        private static string GetHexColor(ELogColor logColor)
        {
            switch (logColor)
            {
                case ELogColor.Red:
                    return "#FF0000";

                case ELogColor.Green:
                    return "#00FF00";

                case ELogColor.Blue:
                    return "#0000FF";

                case ELogColor.Cyan:
                    return "#00FFFF";

                case ELogColor.Yellow:
                    return "#FFFF00";

                default:
                    return "#FFFFFF";
            }

            #endregion 工具方法
        }
    }
}