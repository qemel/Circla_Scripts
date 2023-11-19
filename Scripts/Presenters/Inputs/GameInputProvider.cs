using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Presenters.Inputs
{
    /// <summary>
    /// inputのイベントの発行を行う
    /// </summary>
    public class GameInputProvider : MonoBehaviour, IGameInputEventProvider
    {
        [SerializeField] private PlayerInput _input;

        public ISubject<bool> Space { get; } = new Subject<bool>();
        private readonly ISubject<bool> _space = new Subject<bool>();
        public ISubject<bool> Outer1 { get; } = new Subject<bool>();
        private readonly ISubject<bool> _d = new Subject<bool>();
        public ISubject<bool> Inner1 { get; } = new Subject<bool>();
        private readonly ISubject<bool> _f = new Subject<bool>();
        public ISubject<bool> Inner2 { get; } = new Subject<bool>();
        private readonly ISubject<bool> _j = new Subject<bool>();
        public ISubject<bool> Outer2 { get; } = new Subject<bool>();
        private readonly ISubject<bool> _k = new Subject<bool>();
        public ISubject<bool> Retry { get; } = new Subject<bool>();

        private readonly ISubject<bool> _retry = new Subject<bool>();
        public ISubject<bool> Esc { get; } = new Subject<bool>();
        private readonly ISubject<bool> _esc = new Subject<bool>();


        public ISubject<bool> Left { get; } = new Subject<bool>();
        private readonly ISubject<bool> _left = new Subject<bool>();
        public ISubject<bool> Right { get; } = new Subject<bool>();
        private readonly ISubject<bool> _right = new Subject<bool>();
        public ISubject<bool> Up { get; } = new Subject<bool>();
        private readonly ISubject<bool> _up = new Subject<bool>();
        public ISubject<bool> Down { get; } = new Subject<bool>();
        private readonly ISubject<bool> _down = new Subject<bool>();

        public void Initialize()
        {
            _input.currentActionMap["Space"].performed += _ => _space.OnNext(true);
            _input.currentActionMap["Space"].canceled += _ => _space.OnNext(false);
            _input.currentActionMap["Outer1"].performed += _ => _d.OnNext(true);
            _input.currentActionMap["Outer1"].canceled += _ => _d.OnNext(false);
            _input.currentActionMap["Inner1"].performed += _ => _f.OnNext(true);
            _input.currentActionMap["Inner1"].canceled += _ => _f.OnNext(false);
            _input.currentActionMap["Inner2"].performed += _ => _j.OnNext(true);
            _input.currentActionMap["Inner2"].canceled += _ => _j.OnNext(false);
            _input.currentActionMap["Outer2"].performed += _ => _k.OnNext(true);
            _input.currentActionMap["Outer2"].canceled += _ => _k.OnNext(false);
            _input.currentActionMap["Retry"].performed += _ => _retry.OnNext(true);
            _input.currentActionMap["Retry"].canceled += _ => _retry.OnNext(false);
            _input.currentActionMap["Left"].performed += _ => _left.OnNext(true);
            _input.currentActionMap["Left"].canceled += _ => _left.OnNext(false);
            _input.currentActionMap["Right"].performed += _ => _right.OnNext(true);
            _input.currentActionMap["Right"].canceled += _ => _right.OnNext(false);
            _input.currentActionMap["Up"].performed += _ => _up.OnNext(true);
            _input.currentActionMap["Up"].canceled += _ => _up.OnNext(false);
            _input.currentActionMap["Down"].performed += _ => _down.OnNext(true);
            _input.currentActionMap["Down"].canceled += _ => _down.OnNext(false);
            _input.currentActionMap["Esc"].performed += _ => _esc.OnNext(true);
            _input.currentActionMap["Esc"].canceled += _ => _esc.OnNext(false);

            _input.enabled = true;


            _space.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Space.OnNext(true); }).AddTo(this);
            _space.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Space.OnNext(false); }).AddTo(this);
            _d.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Outer1.OnNext(true); }).AddTo(this);
            _d.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Outer1.OnNext(false); }).AddTo(this);
            _f.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Inner1.OnNext(true); }).AddTo(this);
            _f.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Inner1.OnNext(false); }).AddTo(this);
            _j.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Inner2.OnNext(true); }).AddTo(this);
            _j.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Inner2.OnNext(false); }).AddTo(this);
            _k.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Outer2.OnNext(true); }).AddTo(this);
            _k.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Outer2.OnNext(false); }).AddTo(this);
            _retry.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Retry.OnNext(true); }).AddTo(this);
            _retry.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Retry.OnNext(false); }).AddTo(this);
            _esc.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Esc.OnNext(true); }).AddTo(this);
            _esc.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Esc.OnNext(false); }).AddTo(this);
            _left.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Left.OnNext(true); }).AddTo(this);
            _left.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Left.OnNext(false); }).AddTo(this);
            _right.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Right.OnNext(true); }).AddTo(this);
            _right.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Right.OnNext(false); }).AddTo(this);
            _up.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Up.OnNext(true); }).AddTo(this);
            _up.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Up.OnNext(false); }).AddTo(this);
            _down.DistinctUntilChanged().Where(x => x).Subscribe(_ => { Down.OnNext(true); }).AddTo(this);
            _down.DistinctUntilChanged().Where(x => !x).Subscribe(_ => { Down.OnNext(false); }).AddTo(this);
        }

        public void InputEscape()
        {
            _esc.OnNext(true);
            _esc.OnNext(false);
        }

        public void InputRetry()
        {
            _retry.OnNext(true);
            _retry.OnNext(false);
        }
    }
}