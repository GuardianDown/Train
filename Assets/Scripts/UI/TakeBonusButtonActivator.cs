using System;
using Train.Stations;
using Train.TrainMovement;
using UnityEngine;
using UnityEngine.UI;

namespace Train.UI
{
    public class TakeBonusButtonActivator : IDisposable
    {
        private readonly GameObject _takeBonusButton;
        private readonly IActiveStationsQueue _activeStationsQueue;
        private readonly IPathFollower _pathFollower;

        public TakeBonusButtonActivator(GameObject takeBonusButton, IActiveStationsQueue activeStationsQueue, IPathFollower pathFollower)
        {
            _takeBonusButton = takeBonusButton;
            _activeStationsQueue = activeStationsQueue;
            _pathFollower = pathFollower;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _activeStationsQueue.onAnyStationActive += EnableButton;
            _activeStationsQueue.onAllStationsUnactive += DisableButton;
            _pathFollower.onStopMovement += EnableButton;
            _pathFollower.onStartMovement += DisableButton;
        }

        private void Unsubscribe()
        {
            _activeStationsQueue.onAnyStationActive -= EnableButton;
            _activeStationsQueue.onAllStationsUnactive -= DisableButton;
            _pathFollower.onStopMovement -= EnableButton;
            _pathFollower.onStartMovement -= DisableButton;
        }

        private void EnableButton()
        {
            if(_pathFollower.Speed <= 0f && _activeStationsQueue.ActiveStations.Count > 0)
                _takeBonusButton.SetActive(true);
        }

        private void DisableButton()
        {
            _takeBonusButton.SetActive(false);
        }
    }
}
