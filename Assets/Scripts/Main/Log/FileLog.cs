using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Framework
{
    public enum ELogPlatform
    {
        WinEditor,
        OSXEditor,
        WinPlayer,
        OSXPlayer,
        Android,
        IOS,
    }

    /// <summary>
    /// 文件写日志
    /// </summary>
    public class FileLog : ILog
    {
        private const string LOG_FILE_DIR = "GameLog";
        private const string LOG_FILE_SUFFIX = "log";
        private const int MAX_LOG_FILE_COUNT = 11;

        private ELogPlatform logPlatform;
        private string logFileName;

        private bool isInit;

        private StreamWriter sw;
        private StringBuilder stringBuilder;

        public FileLog(string logFileName = "")
        {
            stringBuilder = new StringBuilder();

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    logPlatform = ELogPlatform.WinEditor;
                    break;

                case RuntimePlatform.OSXEditor:
                    logPlatform = ELogPlatform.OSXEditor;
                    break;

                case RuntimePlatform.WindowsPlayer:
                    logPlatform = ELogPlatform.WinPlayer;
                    break;

                case RuntimePlatform.OSXPlayer:
                    logPlatform = ELogPlatform.OSXPlayer;
                    break;

                case RuntimePlatform.Android:
                    logPlatform = ELogPlatform.Android;
                    break;

                case RuntimePlatform.IPhonePlayer:
                    logPlatform = ELogPlatform.IOS;
                    break;

                default:
                    break;
            }
            this.logFileName = logFileName;

            Application.logMessageReceived += OnLogMessageReceived;
        }

        public void Init()
        {
            if (isInit)
                return;

            try
            {
                string logFileDir = GetFileDir();
                if (!Directory.Exists(logFileDir))
                {
                    Directory.CreateDirectory(logFileDir);
                }

                // 本地存的log文件数量有限制
                string[] logFilePaths = GetLogFilePaths(logFileDir);
                if (logFilePaths.Length > MAX_LOG_FILE_COUNT)
                {
                    foreach (var filePath in logFilePaths)
                    {
                        File.Delete(filePath);
                    }
                }

                string dateTimeStr = GetDateTimeStr();
                string logFileName = $"{(string.IsNullOrEmpty(this.logFileName) ? "GameLog" : this.logFileName)}" +
                                     $"_{dateTimeStr.Replace(" ", "_").Replace("/", "_").Replace(":", "_")}.{LOG_FILE_SUFFIX}";
                FileStream fs = new FileStream(Path.Combine(logFileDir, logFileName), FileMode.OpenOrCreate);
                sw = new StreamWriter(fs);
                sw.WriteLine($"LogFileName：{(string.IsNullOrEmpty(this.logFileName) ? "GameLog" : this.logFileName)}");
                sw.WriteLine($"DateTime：{dateTimeStr}");
                sw.WriteLine();
                sw.AutoFlush = true;
            }
            catch (Exception e)
            {
            }
            isInit = true;
        }

        private void OnLogMessageReceived(string condition, string stackTrace, LogType logType)
        {
            if (sw == null)
                return;

            try
            {
                string dtStr = GetDateTimeStr();
                string conditionStr = TrimRichText(condition);
                if (logType == LogType.Error || logType == LogType.Exception)
                {
                    sw.WriteLine($"{dtStr} {conditionStr}\n{stackTrace}");
                }
                else
                {
                    sw.WriteLine($"{dtStr} {conditionStr}");
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 去除富文本
        /// </summary>
        private string TrimRichText(string message)
        {
            string pattern = "<[^>]*>";
            message = Regex.Replace(message, pattern, "");

            stringBuilder.Clear();
            stringBuilder.Append(message);
            return stringBuilder.ToString();
        }

        private string GetFileDir()
        {
            switch (logPlatform)
            {
                case ELogPlatform.WinEditor:
                case ELogPlatform.OSXEditor:
                    return Application.dataPath + "/../" + LOG_FILE_DIR;

                case ELogPlatform.Android:
                case ELogPlatform.IOS:
                case ELogPlatform.WinPlayer:
                case ELogPlatform.OSXPlayer:
                    return Path.Combine(Application.persistentDataPath, LOG_FILE_DIR);

                default:
                    return Application.dataPath + "/../" + LOG_FILE_DIR;
            }
        }

        private string[] GetLogFilePaths(string logFileDir)
        {
            string[] logFilePaths = Directory.GetFiles(logFileDir, $"*.{LOG_FILE_SUFFIX}", SearchOption.TopDirectoryOnly);
            return logFilePaths;
        }

        private string GetDateTimeStr()
        {
            var dateTimeStr = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            return dateTimeStr;
        }

        public void Dispose()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
            sw.Close();
        }
    }
}