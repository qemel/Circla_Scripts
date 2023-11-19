using App.Scripts.Models.Judgements;

namespace App.Scripts.Views.UIs.Judgements
{
    public interface IJudgementView
    {
        IJudgement Type { get; }
        void SetJudge(int count);
        void Reset();
    }
}