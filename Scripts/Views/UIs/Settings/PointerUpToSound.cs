using AnnulusGames.LucidTools.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Views.UIs.Settings
{
    public class PointerUpToSound : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private AudioClip upSE;


        public void OnPointerUp(PointerEventData eventData)
        {
            LucidAudio.PlaySE(upSE).SetTimeSamples(4400);
        }
    }
}