using System.Collections.Generic;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters.Inputs;
using App.Scripts.Views.UIs;
using App.Scripts.Views.UIs.BackGround;
using UnityEngine;

namespace App.Scripts.Presenters.EntryPoints
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private BeatMapPresenter beatMapPresenter;
        [SerializeField] private MenuInputProvider menuInputProvider;
        [SerializeField] private MenuInputService menuInputService;
        [SerializeField] private RecordView recordView;
        [SerializeField] private DifficultTypeView diff;
        [SerializeField] private StarParent star;
        [SerializeField] private CenterSongView centerSongView;
        [SerializeField] private BackgroundGenerator bgGen;


        [SerializeField] private List<BeatMapScriptableObject> beatMapScriptableObjects;


        private void Awake()
        {
            var beatMapRepository = new BeatMapRepository(beatMapScriptableObjects);
            beatMapPresenter.Initialize(beatMapRepository, recordView, star, diff, centerSongView);
            menuInputProvider.Initialize();
            menuInputService.Initialize(beatMapRepository);
            bgGen.ReGenerate();
        }
    }
}