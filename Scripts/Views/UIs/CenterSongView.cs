using App.Scripts.Models.Songs.Notes;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class CenterSongView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI artist;
        [SerializeField] private float disappearDuration;


        public void SetCurrentSong(BeatMapScriptableObject beatMapScriptableObject)
        {
            title.text = beatMapScriptableObject.Title;
            artist.text = beatMapScriptableObject.Artist;
        }
    }
}