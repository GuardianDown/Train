using UnityEngine;

namespace Train.Breaking
{
    public abstract class AbstractRepairProgressView : MonoBehaviour
    {
        protected IRepair _repair;

        protected abstract void UpdateView(float currentRepairValue, float fullRepairValue);

        public void Construct(IRepair repair)
        {
            _repair = repair;
            Subscribe();
        }

        protected virtual void OnEnable() => Subscribe();

        protected virtual void OnDisable() => Unsubscribe();

        private void Subscribe()
        {
            if (_repair != null)
            {
                _repair.onRepairValueChange += UpdateView;
                UpdateView(_repair.CurrentRepairValue, _repair.FullRepairValue);
            }
        }

        private void Unsubscribe() => _repair.onRepairValueChange -= UpdateView;
    }
}
