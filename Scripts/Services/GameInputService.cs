using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Mods;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters.Inputs;
using App.Scripts.Views.UIs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Presenters
{
    public class GameInputService : MonoBehaviour
    {
        // Do not use this class directly. Use IGameInputEventProvider instead.
        [SerializeField] private GameInputProvider input;
        private IGameInputEventProvider _input;

        [SerializeField] private AudioClip retrySE;
        [SerializeField] private PausePanel pausePanel;


        private bool IsPausePanelActive => pausePanel.IsActive;
        private IModOfPlayability _modOfPlayability;


        public void Initialize(IModOfPlayability modOfPlayability)
        {
            _input = input;
            _modOfPlayability = modOfPlayability;

            _input.Outer1.Where(x => x).Where(_ => !IsPausePanelActive)
                .Subscribe(_ => MessageBroker.Default.Publish(LaneType.Outer)).AddTo(this);
            _input.Inner1.Where(x => x).Where(_ => !IsPausePanelActive)
                .Subscribe(_ => MessageBroker.Default.Publish(LaneType.Inner)).AddTo(this);
            _input.Inner2.Where(x => x).Where(_ => !IsPausePanelActive)
                .Subscribe(_ => MessageBroker.Default.Publish(LaneType.Inner)).AddTo(this);
            _input.Outer2.Where(x => x).Where(_ => !IsPausePanelActive)
                .Subscribe(_ => MessageBroker.Default.Publish(LaneType.Outer)).AddTo(this);
            _input.Space.Where(x => x).Where(_ => !IsPausePanelActive)
                .Subscribe(_ => MessageBroker.Default.Publish(LaneType.Center)).AddTo(this);

            if (_modOfPlayability is AutoArts)
            {
                MessageBroker.Default.Publish(new RotInputAll() as IRotInput);
            }
            else
            {
                _input.Left.Where(x => x).Where(_ => !IsPausePanelActive)
                    .Subscribe(_ => MessageBroker.Default.Publish(new RotInputLeft() as IRotInput))
                    .AddTo(this);
                _input.Right.Where(x => x).Where(_ => !IsPausePanelActive)
                    .Subscribe(_ => MessageBroker.Default.Publish(new RotInputRight() as IRotInput))
                    .AddTo(this);
                _input.Up.Where(x => x).Where(_ => !IsPausePanelActive)
                    .Subscribe(_ => MessageBroker.Default.Publish(new RotInputUp() as IRotInput))
                    .AddTo(this);
                _input.Down.Where(x => x).Where(_ => !IsPausePanelActive)
                    .Subscribe(_ => MessageBroker.Default.Publish(new RotInputDown() as IRotInput))
                    .AddTo(this);
            }

            _input.Esc.Where(x => x).Subscribe(_ => { OnEscGame(); }).AddTo(this);
            _input.Retry.Where(x => x).Subscribe(_ => { OnRetryGame(); }).AddTo(this);
        }

        private void OnRetryGame()
        {
            LucidAudio.PlaySE(retrySE).SetTimeSamples(4400);
            SceneManager.LoadScene(SceneId.Game.ToString());
            Time.timeScale = 1;
        }


        private void OnEscGame()
        {
            LucidAudio.PlaySE(retrySE).SetTimeSamples(4400);
            pausePanel.ChangeActivate();

            if (IsPausePanelActive) LucidAudio.PauseAllBGM();
            else LucidAudio.UnPauseAllBGM();
        }

        public void OnBackToMenu()
        {
            SceneManager.LoadScene(SceneId.Menu.ToString());
            Time.timeScale = 1;
        }
    }
}