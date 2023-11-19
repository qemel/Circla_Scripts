using App.Scripts.Models.Songs.Notes;
using UniRx;
using UnityEngine;

namespace App.Scripts.Views.Entities
{
    public class PlayerBarMover : MonoBehaviour
    {
        private void Start()
        {
            MessageBroker.Default.Receive<IRotInput>()
                .Subscribe(x =>
                {
                    transform.rotation = x switch
                    {
                        RotInputDown => Quaternion.Euler(0, 0, 0),
                        RotInputUp => Quaternion.Euler(0, 0, 180),
                        RotInputRight => Quaternion.Euler(0, 0, 90),
                        RotInputLeft => Quaternion.Euler(0, 0, -90),
                        _ => transform.rotation
                    };
                }).AddTo(this);
        }
    }
}