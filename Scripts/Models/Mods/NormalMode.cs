using System;
using App.Scripts.Presenters;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Models.Mods
{
    [Serializable]
    public class NormalMode : IModOfVisual
    {
        public UniTask Reflect(LaneTapPresenter inner, LaneTapPresenter outer, LaneRotPresenter rot)
        {
            // donothing
            return UniTask.CompletedTask;
        }
    }
}