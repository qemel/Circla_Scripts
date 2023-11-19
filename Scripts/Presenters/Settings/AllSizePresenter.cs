using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    public class AllSizePresenter : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Transform allParent;
        

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
            slider.value = PlayerPrefs.GetFloat("AllSize", 1f);
        }
        
        private void OnValueChanged(float value)
        {
            if (value < 0f || value > 1f) throw new System.ArgumentException(nameof(value));
            PlayerPrefs.SetFloat("AllSize", value);
            allParent.localScale = new Vector3(value, value, 1);
        }
        
        private void OnDisable()
        {
            slider.onValueChanged.RemoveAllListeners();
        }
    }
}