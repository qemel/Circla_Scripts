using System;

namespace App.Scripts.Models.Songs.Notes
{
    [Serializable]
    public class PlayHistory
    {
        public DateTime Date => date;
        private DateTime date;

        public int Score { get; private set; }
    }
}