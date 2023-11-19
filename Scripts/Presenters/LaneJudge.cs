using App.Scripts.Models.Judgements;
using App.Scripts.Models.Songs.Notes;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public static class LaneJudge
    {
        public const float LagLimit = 0.180f;

        public static IJudgement GetJudge(float songTimeSec, Note note)
        {
            var diffSec = Mathf.Abs(songTimeSec - note.TimeSec);
            return diffSec switch
            {
                < 0.060f => new Perfect(),
                < 0.100f => new Great(),
                < 0.120f => new Good(),
                < 0.150f => new Bad(),
                < LagLimit => new Miss(),
                _ => null
            };
        }
    }
}