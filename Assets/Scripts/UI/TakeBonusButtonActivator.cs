using System;
using Train.Stations;
using UnityEngine.UI;

namespace Train.UI
{
    public class TakeBonusButtonActivator : IDisposable
    {
        private readonly Button _takeBonusButton;
        private readonly IActiveStationsQueue _activeStationsQueue;

        public TakeBonusButtonActivator(Button takeBonusButton, IActiveStationsQueue activeStationsQueue)
        {
            _takeBonusButton = takeBonusButton;
            _activeStationsQueue = activeStationsQueue;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _activeStationsQueue.onAnyStationActive += EnableButton;
            _activeStationsQueue.onAllStationsUnactive += DisableButton;
        }

        private void Unsubscribe()
        {
            _activeStationsQueue.onAnyStationActive -= EnableButton;
            _activeStationsQueue.onAllStationsUnactive -= DisableButton;
        }

        private void EnableButton() => _takeBonusButton.gameObject.SetActive(true);

        private void DisableButton() => _takeBonusButton.gameObject.SetActive(false);
    }
}
