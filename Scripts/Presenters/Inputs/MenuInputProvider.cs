using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Presenters.Inputs
{
    public class MenuInputProvider : MonoBehaviour, IMenuInputEventProvider
    {
        [SerializeField] private PlayerInput _input;

        public IObservable<bool> Up => _up;
        private readonly ISubject<bool> _up = new Subject<bool>();
        public IObservable<bool> Down => _down;
        private readonly ISubject<bool> _down = new Subject<bool>();
        public IObservable<ArrowInput> Left => _left;
        private readonly ISubject<ArrowInput> _left = new Subject<ArrowInput>();
        public IObservable<ArrowInput> Right => _right;
        private readonly ISubject<ArrowInput> _right = new Subject<ArrowInput>();
        public IObservable<bool> Enter => _enter;
        private readonly ISubject<bool> _enter = new Subject<bool>();
        public IObservable<bool> Tab => _tab;
        private readonly ISubject<bool> _tab = new Subject<bool>();

        public IObservable<bool> Esc => _esc;
        private readonly ISubject<bool> _esc = new Subject<bool>();
        private IObservable<bool> _right1;
        private IObservable<bool> _left1;

        public void Initialize()
        {
            _input.currentActionMap["Up"].performed += _ => { _up.OnNext(true); };
            _input.currentActionMap["Up"].canceled += _ => _up.OnNext(false);
            _input.currentActionMap["Down"].performed += _ => { _down.OnNext(true); };
            _input.currentActionMap["Down"].canceled += _ => _down.OnNext(false);
            _input.currentActionMap["Left"].performed += _ => { _left.OnNext(ArrowInput.Left); };
            _input.currentActionMap["Left"].canceled += _ => _left.OnNext(ArrowInput.None);
            _input.currentActionMap["Right"].performed += _ => { _right.OnNext(ArrowInput.Right); };
            _input.currentActionMap["Right"].canceled += _ => _right.OnNext(ArrowInput.None);
            _input.currentActionMap["Decision"].performed += _ => { _enter.OnNext(true); };
            _input.currentActionMap["Decision"].canceled += _ => _enter.OnNext(false);
            _input.currentActionMap["Tab"].performed += _ => { _tab.OnNext(true); };
            _input.currentActionMap["Tab"].canceled += _ => _tab.OnNext(false);
            _input.currentActionMap["Esc"].performed += _ => { _esc.OnNext(true); };
            _input.currentActionMap["Esc"].canceled += _ => _esc.OnNext(false);
        }

        public void InputEnter()
        {
            _enter.OnNext(true);
            _enter.OnNext(false);
        }

        public void InputTab()
        {
            _tab.OnNext(true);
            _tab.OnNext(false);
        }

        public void InputEsc()
        {
            _esc.OnNext(true);
            _esc.OnNext(false);
        }

        public void InputLeft()
        {
            _left.OnNext(ArrowInput.Left);
            _left.OnNext(ArrowInput.None);
        }

        public void InputRight()
        {
            _right.OnNext(ArrowInput.Right);
            _right.OnNext(ArrowInput.None);
        }
    }
}