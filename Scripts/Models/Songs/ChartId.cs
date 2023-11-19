using System;

namespace App.Scripts.Models.Songs.Notes
{
    public struct ChartId : IEquatable<ChartId>
    {
        private readonly int _songId;
        private readonly DifficultyType _difficultyType;
        
        public ChartId(int songId, DifficultyType difficultyType)
        {
            _songId = songId;
            _difficultyType = difficultyType;
        }

        public bool Equals(ChartId other)
        {
            return _songId == other._songId && _difficultyType == other._difficultyType;
        }

        public override bool Equals(object obj)
        {
            return obj is ChartId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_songId, (int)_difficultyType);
        }
    }
}