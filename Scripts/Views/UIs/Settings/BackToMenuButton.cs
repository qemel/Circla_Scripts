using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs.Settings
{
    [RequireComponent(typeof(Button))]
    public class BackToMenuButton : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private AudioClip clip;


        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                LucidAudio.PlaySE(clip);
                SceneManager.UnloadSceneAsync(SceneId.SettingSub.ToString());
            });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}