using System;

namespace App.Scripts.Models.Judgements
{
    [Serializable]
    public record Perfect : IJudgement
    {
        public int ScoreBase => 1000;
        public float AccuracyBase => 1f;
    }
    
    [Serializable]
    public record Great : IJudgement
    {
        public int ScoreBase => 800;
        public float AccuracyBase => 0.8f;
    }
    
    [Serializable]
    public record Good : IJudgement
    {
        public int ScoreBase => 600;
        public float AccuracyBase => 0.6f;
    }
    
    [Serializable]
    public record Bad : IJudgement
    {
        public int ScoreBase => 400;
        public float AccuracyBase => 0;
    }
    
    [Serializable]
    public record Miss : IJudgement
    {
        public int ScoreBase => 0;
        public float AccuracyBase => 0;
    }
}