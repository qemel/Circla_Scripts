using System;
using App.Scripts.Presenters;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Models.Mods
{
    [Serializable]
    public class KartMode : IModOfVisual
    {
        public async UniTask Reflect(LaneTapPresenter inner, LaneTapPresenter outer, LaneRotPresenter rot)
        {
            // innerのy座標を反転
            var transform = inner.Mover.transform;
            var position = transform.position;
            position = new UnityEngine.Vector3(position.x, -position.y, position.z);
            transform.position = position;

            await inner.OnSpawnedAllNotesAsync.ToUniTask();

            // z軸方向に180度回転
            inner.transform.Rotate(new UnityEngine.Vector3(0, 0, 180));
        }
    }
}