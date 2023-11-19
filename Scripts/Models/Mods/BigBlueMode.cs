using System;
using App.Scripts.Presenters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Models.Mods
{
    [Serializable]
    public class BigBlueMode : IModOfVisual
    {
        public async UniTask Reflect(LaneTapPresenter inner, LaneTapPresenter outer, LaneRotPresenter rot)
        {
            // innerのy座標を反転し2倍に
            var transform = inner.Mover.transform;
            inner.Mover.ChangeToBigBlueSprite();
            var position = transform.position;
            position = new Vector3(position.x * 2, -position.y * 2, position.z);
            transform.position = position;

            await inner.OnSpawnedAllNotesAsync.ToUniTask();

            Transform innerTransform;
            (innerTransform = inner.transform).Rotate(new Vector3(0, 0, 180));
            innerTransform.localScale = new Vector3(2, 2, 2);
        }
    }
}