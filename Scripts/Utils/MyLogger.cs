using System;
using UnityEngine;

namespace App.Scripts.Utils
{
    public static class MyLogger
    {
        public static void LogMenuUi(string message, LogType type = 0)
        {
            LogGeneral("Menu", message, type);
        }

        public static void LogSong(string message, LogType type = 0)
        {
            LogGeneral("Song", message, type);
        }

        public static void LogInput(string message, LogType type = 0)
        {
            LogGeneral("Input", message, type);
        }

        public static void LogLoad(string message, LogType type = 0)
        {
            LogGeneral("Load", message, type);
        }

        public static void LogNoteGen(string message, LogType type = 0)
        {
            LogGeneral("NoteGen", message, type);
        }

        public static void LogGame(string message, LogType type = 0)
        {
            LogGeneral("NoteGame", message, type);
        }

        public static void LogJudge(string message, LogType type = 0)
        {
            LogGeneral("GetJudge", message, type);
        }


        /// <param name="domain">スクリプトの書かれている大まかな領域</param>
        /// <param name="message">ログの内容</param>
        /// <param name="type">Logの種類</param>
        private static void LogGeneral(string domain, string message, LogType type)
        {
            var context = $"[{domain}] {message}";
            switch (type)
            {
                case LogType.Debug:
                    Debug.Log(context);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(context);
                    break;
                case LogType.Error:
                    Debug.LogError(context);
                    break;
                case LogType.Tips:
                    Debug.Log($"<color=aqua>{context}</color>");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum LogType
    {
        Debug = 0,
        Warning,
        Error,
        Tips,
    }
}