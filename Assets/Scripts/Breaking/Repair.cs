using System;

namespace Train.Breaking
{
    public class Repair : IRepair
    {
        private readonly IBreaking _breaking;
        private readonly float _fullRepairValue;

        private float _currentRepairValue;
        private float _fixDelta;

        public float CurrentRepairValue => _currentRepairValue;
        public float FullRepairValue => _fullRepairValue;

        public event Action<float, float> onRepairValueChange;
        public event Action onFullRepair;

        public Repair(IBreaking breaking, float fullRepairValue)
        {
            _breaking = breaking;
            _fullRepairValue = fullRepairValue;

            Subscribe();
        }

        public void Fix()
        {
            _currentRepairValue += _fixDelta;
            if (_currentRepairValue >= _fullRepairValue)
            {
                _currentRepairValue = _fullRepairValue;
                onFullRepair?.Invoke();
            }
            onRepairValueChange?.Invoke(_currentRepairValue, _fullRepairValue);
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe() => _breaking.onBreaking += StartRepair;

        private void Unsubscribe() => _breaking.onBreaking -= StartRepair;

        private void StartRepair(float fixDelta)
        {
            _currentRepairValue = 0f;
            _fixDelta = fixDelta;
        }

    }
}
