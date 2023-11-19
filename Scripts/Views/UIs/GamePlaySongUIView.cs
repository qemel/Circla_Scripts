using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class GamePlaySongUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;


        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}