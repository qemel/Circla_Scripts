using System;

namespace App.Scripts.Models.Songs.Notes.Plays
{
    /// <summary>
    /// 最高記録を表すクラス
    /// </summary>
    public class Record
    {
        public int Score { get; }
        public int MaxCombo { get; }
        public float Accuracy { get; }
        public string Date { get; }

        public Record(int score, int maxCombo, float acc, DateTime date)
        {
            Score = score;
            MaxCombo = maxCombo;
            Accuracy = acc;
            Date = date.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public Record(int score, int maxCombo, float acc, string date)
        {
            Score = score;
            MaxCombo = maxCombo;
            Accuracy = acc;
            Date = date;
        }
    }
}