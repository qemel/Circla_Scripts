using UnityEngine;

namespace App.Scripts.Views.UIs.BackGround
{
    public class LineBackGround : MonoBehaviour
    {
        private PlayerBackGroundMover _playerBackGroundMover;

        private void Awake()
        {
            TryGetComponent(out _playerBackGroundMover);
        }

        public void SetSpeed(float speed)
        {
            _playerBackGroundMover.SetSpeed(speed);
        }
    }
}