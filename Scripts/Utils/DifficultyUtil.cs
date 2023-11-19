using System;
using App.Scripts.Models.Songs.Notes;

namespace App.Scripts.Utils
{
    public static class DifficultyUtil
    {
        public static DifficultyType GetNextDifficultyType(DifficultyType type)
        {
            return type switch
            {
                DifficultyType.Easy => DifficultyType.Hard,
                DifficultyType.Hard => DifficultyType.Expert,
                DifficultyType.Expert => DifficultyType.Easy,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}