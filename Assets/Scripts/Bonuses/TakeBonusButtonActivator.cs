using System;
using Train.Stations;
using Train.TrainMovement;

namespace Train.Bonuses
{
    public class TakeBonusButtonActivator : IDisposable
    {
        private readonly AbstractTakeBonusView _takeBonusButton;
        private readonly IActiveStationsQueue _activeStationsQueue;
        private readonly IPathFollower _pathFollower;

        public TakeBonusButtonActivator(AbstractTakeBonusView takeBonusView, 
            IActiveStationsQueue activeStationsQueue, IPathFollower pathFollower)
        {
            _takeBonusButton = takeBonusView;
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
                _takeBonusButton.gameObject.SetActive(true);
        }

        private void DisableButton() => _takeBonusButton.gameObject.SetActive(false);
    }
}
