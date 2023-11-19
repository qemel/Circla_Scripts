using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Presenters.Inputs
{
    public class TitleInputPresenter : MonoBehaviour, IInputEventProvider
    {
        [SerializeField] private PlayerInput input;

        public IObservable<bool> Enter => _enter;
        private readonly ISubject<bool> _enter = new Subject<bool>();

        public IObservable<bool> Esc => _esc;
        private readonly ISubject<bool> _esc = new Subject<bool>();

        public IObservable<bool> Reset => _reset;
        private readonly ISubject<bool> _reset = new Subject<bool>();

        private void Awake()
        {
            input.currentActionMap["Play"].performed += _ => { _enter.OnNext(true); };
            input.currentActionMap["Play"].canceled += _ => _enter.OnNext(false);
            input.currentActionMap["Setting"].performed += _ => { _esc.OnNext(true); };
            input.currentActionMap["Setting"].canceled += _ => _esc.OnNext(false);
            input.currentActionMap["Reset"].performed += _ => { _reset.OnNext(true); };
            input.currentActionMap["Reset"].canceled += _ => _reset.OnNext(false);
        }

        public void InputEscape()
        {
            _esc.OnNext(true);
            _esc.OnNext(false);
        }

        public void InputPlay()
        {
            _enter.OnNext(true);
            _enter.OnNext(false);
        }
    }
}