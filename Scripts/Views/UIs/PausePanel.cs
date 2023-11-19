using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class PausePanel : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        public void ChangeActivate()
        {
            Time.timeScale = IsActive ? 1f : 0f;
            IsActive = !IsActive;
            gameObject.SetActive(IsActive);
        }
    }
}