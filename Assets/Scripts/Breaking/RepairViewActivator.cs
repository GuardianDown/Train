using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Train.Breaking
{
    public class RepairViewActivator : IDisposable
    {
        private readonly AbstractRepairProgressView _repairProgressViewPrefab;
        private readonly AbstractFixView _fixViewPrefab;
        private readonly IBreaking _breaking;
        private readonly IRepair _repair;
        private readonly Canvas _canvas;

        private AbstractRepairProgressView _repairProgressView;
        private AbstractFixView _fixView;

        public RepairViewActivator(IBreaking breaking, IRepair repair, 
            AbstractRepairProgressView repairProgressViewPrefab, AbstractFixView fixViewPrefab, Canvas canvas)
        {
            _repairProgressViewPrefab = repairProgressViewPrefab;
            _fixViewPrefab = fixViewPrefab;
            _breaking = breaking;
            _repair = repair;
            _canvas = canvas;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _breaking.onBreaking += ActivateView;
            _repair.onFullRepair += DeactivateView;
        }

        private void Unsubscribe()
        {
            _breaking.onBreaking -= ActivateView;
            _repair.onFullRepair -= DeactivateView;
        }

        private void ActivateView(float fixDelta)
        {
            if (_repairProgressView == null)
            {
                _repairProgressView = Object.Instantiate(_repairProgressViewPrefab, _canvas.transform);
                _repairProgressView.Construct(_repair);
            }
            else
            {
                _repairProgressView.gameObject.SetActive(true);
            }

            if (_fixView == null)
            {
                _fixView = Object.Instantiate(_fixViewPrefab, _canvas.transform);
                _fixView.Construct(_repair);
            }
            else
            {
                _fixView.gameObject.SetActive(true);
            }
        }

        private void DeactivateView()
        {
            _repairProgressView.gameObject.SetActive(false);
            _fixView.gameObject.SetActive(false);
        }
    }
}
