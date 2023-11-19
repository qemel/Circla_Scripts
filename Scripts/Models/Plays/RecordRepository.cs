using System;
using UnityEngine;

namespace App.Scripts.Models.Songs.Notes.Plays
{
    public static class RecordRepository
    {
        public static Record GetRecord((string, DifficultyType) key)
        {
            var val = PlayerPrefs.GetInt($"{key.Item1}-{key.Item2}", 0);
            var combo = PlayerPrefs.GetInt($"{key.Item1}-{key.Item2}-combo", 0);
            var acc = PlayerPrefs.GetFloat($"{key.Item1}-{key.Item2}-acc", 0f);
            var date = PlayerPrefs.GetString($"{key.Item1}-{key.Item2}-date", "");
            return new Record(val, combo, acc, date);
        }

        private static void SaveRecord((string, DifficultyType) key, Record record)
        {
            PlayerPrefs.SetInt($"{key.Item1}-{key.Item2}", record.Score);
            PlayerPrefs.SetInt($"{key.Item1}-{key.Item2}-combo", record.MaxCombo);
            PlayerPrefs.SetFloat($"{key.Item1}-{key.Item2}-acc", record.Accuracy);
            PlayerPrefs.SetString($"{key.Item1}-{key.Item2}-date", DateTime.Now.ToString("yyyy/MM/dd"));
            PlayerPrefs.Save();
        }


        /// <summary>
        /// ハイスコアをセーブし、ハイスコアを更新したかどうかを返す
        /// </summary>
        /// <param name="songName"></param>
        /// <param name="type"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public static bool SaveHighScore(string songName, DifficultyType type, Record record)
        {
            var key = (songName, type);

            var highScore = GetRecord(key);
            if (highScore.Score >= record.Score) return false;
            SaveRecord(key, record);
            return true;
        }
    }
}