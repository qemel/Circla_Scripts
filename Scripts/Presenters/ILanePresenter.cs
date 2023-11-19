namespace App.Scripts.Presenters
{
    public interface ILanePresenter
    {
        void CheckSpawn();
        
        bool IsEnd { get; }
    }
}