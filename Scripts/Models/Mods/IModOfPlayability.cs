namespace App.Scripts.Models.Mods
{
    public interface IModOfPlayability : IMod
    {
        float ScoreMultiplier { get; }
    }
}