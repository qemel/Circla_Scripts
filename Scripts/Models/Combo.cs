using System;
using App.Scripts.Views.UIs;
using UniRx;

namespace App.Scripts.Models.Songs.Notes
{
    public class Combo : IDisposable
    {
        public IReadOnlyReactiveProperty<int> MaxCombo => _maxCombo;
        public IReadOnlyReactiveProperty<int> Current => _current;

        private readonly ReactiveProperty<int> _maxCombo = new();
        private readonly ReactiveProperty<int> _current = new();

        private ComboView _comboView;

        public Combo(ComboView comboView)
        {
            _comboView = comboView;
            _maxCombo.Value = 0;
            _current.Value = 0;

            Register();
        }

        private void Register()
        {
            _current.DistinctUntilChanged().Subscribe(_ => { _comboView.Set(_current.Value); });
        }

        public void Add()
        {
            _current.Value++;
            if (_current.Value > _maxCombo.Value)
                _maxCombo.Value = _current.Value;
        }

        public void Reset()
        {
            _current.Value = 0;
        }

        public void Dispose()
        {
            _maxCombo?.Dispose();
            _current?.Dispose();
        }
    }
}