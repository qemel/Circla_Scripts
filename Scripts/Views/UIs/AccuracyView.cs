using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class AccuracyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI acc;

        public void Set(float cur)
        {
            acc.text = $"{cur * 100:F2}%";
        }
    }
}