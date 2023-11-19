namespace App.Scripts.Models.Mods
{
    public class AutoArts : IModOfPlayability
    {
        // 全体のScoreを0.6倍にする
        // RotNoteを勝手にすべてPerfect判定にする 
        public float ScoreMultiplier => 0.6f;
    }
}