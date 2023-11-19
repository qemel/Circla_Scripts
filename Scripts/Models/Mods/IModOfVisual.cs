using App.Scripts.Presenters;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Models.Mods
{
    public interface IModOfVisual : IMod
    {
        /// <summary>
        /// modの内容を反映する
        /// </summary>
        UniTask Reflect(LaneTapPresenter inner, LaneTapPresenter outer, LaneRotPresenter rot);        
    }
}