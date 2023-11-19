namespace App.Scripts.Models.Judgements
{
    public interface IJudgement
    {
        int ScoreBase { get; }
        float AccuracyBase { get; }
    }
}